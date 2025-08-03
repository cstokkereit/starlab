using log4net;
using ScottPlot;
using ScottPlot.Plottables;
using StarLab.Data;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;
using System.Text.RegularExpressions;

namespace StarLab.UI.Workspace.Documents.Charts
{
    //https://scottplot.net/cookbook/5.0/
    // https://astronomy.stackexchange.com/questions/39610/is-there-a-formula-for-absolute-magnitude-that-does-not-contain-an-apparent-magn
    // https://github.com/casaluca/bolometric-corrections

    /// <summary>
    /// A <see cref="UserControl"/> that implements the <see cref="IChartView"/> interface used to control the behaviour that is specific to a chart document.
    /// </summary>
    public partial class ChartView : UserControl, IChartView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartView)); // The logger that will be used for writing log messages.

        private readonly IChartViewPresenter presenter; // The presenter that controls the view.

        private readonly SplitViewPanels panel; // The panel that will contain the view.

        readonly ScottPlot.Plottables.Rectangle RectanglePlot; //

        private Scatter scatter; //

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartView"> class.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information required to construct this view.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        public ChartView(IViewDefinition definition, IViewFactory factory)
        {
            ArgumentNullException.ThrowIfNull(definition, nameof(definition));
            ArgumentNullException.ThrowIfNull(factory, nameof(factory));

            // Scale points with zoom
            // Dragable axis lines
            // scale points according to number of stars
            // Colour points - spectrum
            // Colour back ground - spectrum
            // Tick mark density

            InitializeComponent();

            Name = Views.Chart;

            panel = (SplitViewPanels)definition.Panel;

            presenter = (IChartViewPresenter)factory.CreatePresenter(definition, this);





            // TODO - This is all temporary - calculations etc need to happen in a worker thread

            // add a rectangle we can use as a selection indicator
            RectanglePlot = formsPlot.Plot.Add.Rectangle(0, 0, 0, 0);
            RectanglePlot.FillStyle.Color = Colors.Red.WithAlpha(.2);

            // add events to trigger in response to mouse actions
            formsPlot.MouseMove += FormsPlot1_MouseMove;
            formsPlot.MouseDown += FormsPlot1_MouseDown;
            formsPlot.MouseUp += FormsPlot1_MouseUp;
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"> that controls this view.
        /// </summary>
        public IChildViewController Controller => (IChildViewController)presenter;

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        public SplitViewPanels Panel => panel;

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            // This is all temporary

            // Split data into vertical series and colour according to spectral class

            Tuple<double[], double[]> data = GetData();

            scatter = formsPlot.Plot.Add.ScatterPoints(data.Item1, data.Item2);

            scatter.MarkerColor = Colors.Green;
            scatter.MarkerSize = 2;
            formsPlot.Refresh();

            formsPlot.Plot.Axes.Bottom.Label.Text = "Spectral Class";

            double[] tickPositions = new double[70];
            string[] tickLabels = new string[70]; // = { "O", "B", "A", "F", "G", "K", "M" };

            string c = "O";
            int s = 2;

            for (int n = 2; n < 70; n++)
            {
                if (s > 9)
                {
                    s = 0;
                }

                if (n > 9)
                {
                    c = "B";
                }
                if (n > 19)
                {
                    c = "A";
                }
                if (n > 29)
                {
                    c = "F";
                }
                if (n > 39)
                {
                    c = "G";
                }
                if (n > 49)
                {
                    c = "K";
                }
                if (n > 59)
                {
                    c = "M";
                }

                tickPositions[n] = n;
                tickLabels[n] = c + s.ToString();
                s++;
            }

            //ScottPlot.TickGenerators.NumericAutomatic tickGenX = new();
            //tickGenX.TargetTickCount = 10;
            //formsPlot.Plot.Axes.Bottom.TickGenerator = tickGenX;

            //ScottPlot.TickGenerators.NumericAutomatic tickGenX = new();
            //tickGenX.MinimumTickSpacing = 5;
            //formsPlot.Plot.Axes.Bottom.TickGenerator = tickGenX;

            formsPlot.Plot.Axes.SetLimits(2, 69, 20, -10);

            formsPlot.Plot.Axes.Bottom.SetTicks(tickPositions, tickLabels);

            formsPlot.Plot.Axes.Left.Label.Text = "Luminosity";

            formsPlot.Plot.Axes.Title.Label.Text = "Plot Title";

            formsPlot.Plot.Grid.XAxisStyle.IsVisible = false;
            formsPlot.Plot.Grid.YAxisStyle.IsVisible = false;

            // some items must be styled directly
            formsPlot.Plot.Grid.MajorLineColor = Colors.Green;
            formsPlot.Plot.FigureBackground.Color = Colors.Black;
            formsPlot.Plot.DataBackground.Color = Colors.Black;

            // the Style object contains helper methods to style many items at once
            formsPlot.Plot.Axes.Color(Colors.Green);

            // Lock the X axis min and max
            formsPlot.Plot.Axes.Rules.Clear();
            formsPlot.Plot.Axes.Rules.Add(new LockAxisRule());

        }

        

        readonly Coordinates[] DataPoints;
        Coordinates MouseDownCoordinates;
        Coordinates MouseNowCoordinates;
        CoordinateRect MouseSlectionRect => new(MouseDownCoordinates, MouseNowCoordinates);
        bool MouseIsDown = true;

        bool selectPoints = false;

        private Tuple<double[], double[]> GetData()
        {
            var stars = new StarsRepository(); // TODO - This should be done through the presenter, this should not know about the Domain model

            stars.Populate();

            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            var errors = 0;

            foreach (var star in stars)
            {
                try
                {
                    if (!string.IsNullOrEmpty(star.SpectralType.SpectralClass))
                    {
                        xValues.Add(Parse(star.SpectralType.SpectralClass));
                        yValues.Add(star.AbsoluteMagnitude);
                    }
                }
                catch (Exception e)
                {
                    errors++;
                }
            }

            return new Tuple<double[], double[]>(xValues.ToArray(), yValues.ToArray());
        }

        private double Parse(string spectralType)
        {
            double retval = 0;

            if (!spectralType.Contains('/') && !spectralType.Contains('-'))
            {
                if (spectralType.StartsWith("B"))
                {
                    retval = 10;
                }
                else if (spectralType.StartsWith("A"))
                {
                    retval = 20;
                }
                else if (spectralType.StartsWith("F"))
                {
                    retval = 30;
                }
                else if (spectralType.StartsWith("G"))
                {
                    retval = 40;
                }
                else if (spectralType.StartsWith("K"))
                {
                    retval = 50;
                }
                else if (spectralType.StartsWith("M"))
                {
                    retval = 60;
                }
                else
                {
                    throw new InvalidOperationException();
                }

                retval = retval + double.Parse(spectralType.Substring(1));
            }

            return retval;
        }

        private void FormsPlot1_MouseDown(object? sender, MouseEventArgs e)
        {
            if (!selectPoints)
                return;

            MouseIsDown = true;
            RectanglePlot.IsVisible = true;
            MouseDownCoordinates = formsPlot.Plot.GetCoordinates(e.X, e.Y);
            //formsPlot.Interaction.Disable(); TODO - disable the default click-drag-pan behavior 
        }

        private void FormsPlot1_MouseUp(object? sender, MouseEventArgs e)
        {
            if (!selectPoints)
                return;

            MouseIsDown = false;
            RectanglePlot.IsVisible = false;

            // clear old markers
            formsPlot.Plot.Remove<ScottPlot.Plottables.Marker>();

            // identify selectedPoints
            var selectedPoints = scatter.Data.GetScatterPoints().Where(x => MouseSlectionRect.Contains(x));

            // add markers to outline selected points
            foreach (Coordinates selectedPoint in selectedPoints)
            {
                var newMarker = formsPlot.Plot.Add.Marker(selectedPoint);
                newMarker.MarkerStyle.Shape = MarkerShape.OpenCircle;
                newMarker.MarkerStyle.Size = 10;
                newMarker.MarkerStyle.FillColor = Colors.Red.WithAlpha(.2);
                newMarker.MarkerStyle.LineColor = Colors.Red;
                newMarker.MarkerStyle.LineWidth = 1;
            }

            // reset the mouse positions
            MouseDownCoordinates = Coordinates.NaN;
            MouseNowCoordinates = Coordinates.NaN;

            // update the plot
            formsPlot.Refresh();
            //formsPlot.Interaction.Enable(); // re-enable the default click-drag-pan behavior
        }

        private void FormsPlot1_MouseMove(object? sender, MouseEventArgs e)
        {
            if (!MouseIsDown || !selectPoints)
                return;

            MouseNowCoordinates = formsPlot.Plot.GetCoordinates(e.X, e.Y);
            RectanglePlot.CoordinateRect = MouseSlectionRect;
            formsPlot.Refresh();
        }
    }

    public class LockAxisRule : IAxisRule
    {
        public void Apply(RenderPack rp, bool beforeLayout)
        {
            IXAxis bottom = rp.Plot.Axes.Bottom;
            if (bottom.Min < 2) bottom.Min = 2;
            if (bottom.Max > 69) bottom.Max = 69;

            IYAxis left = rp.Plot.Axes.Left;
            if (left.Min > 20) left.Min = 20;
            if (left.Max < -10) left.Max = -10;

        }
    }
}
