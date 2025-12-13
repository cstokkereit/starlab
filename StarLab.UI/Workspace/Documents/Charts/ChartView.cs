using log4net;
using ScottPlot;
using ScottPlot.Plottables;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared.Properties;

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

        readonly ScottPlot.Plottables.Rectangle RectanglePlot; //

        private IChartViewPresenter? presenter; // The presenter that controls the view.

        private Scatter scatter; //

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartView"> class.
        /// </summary>
        public ChartView()
        {
            // Scale points with zoom
            // Dragable axis lines
            // scale points according to number of stars
            // Colour points - spectrum
            // Colour back ground - spectrum
            // Tick mark density

            InitializeComponent();

            Name = Views.Chart;

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(ChartView)));



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
        public IChildViewController? Controller => (IChildViewController?)presenter;

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        public SplitViewPanels Panel => SplitViewPanels.Panel2;

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(); // TODO

            this.presenter = (IChartViewPresenter)presenter;
        }

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
            scatter.MarkerSize = 1;
            formsPlot.Refresh();

            //double[] tickPositions = new double[70];
            //string[] tickLabels = new string[70]; // = { "O", "B", "A", "F", "G", "K", "M" };

            //string c = "O";
            //int s = 2;

            //for (int n = 2; n < 70; n++)
            //{
            //    if (s > 9)
            //    {
            //        s = 0;
            //    }

            //    if (n > 9)
            //    {
            //        c = "B";
            //    }
            //    if (n > 19)
            //    {
            //        c = "A";
            //    }
            //    if (n > 29)
            //    {
            //        c = "F";
            //    }
            //    if (n > 39)
            //    {
            //        c = "G";
            //    }
            //    if (n > 49)
            //    {
            //        c = "K";
            //    }
            //    if (n > 59)
            //    {
            //        c = "M";
            //    }

            //    tickPositions[n] = n;
            //    tickLabels[n] = c + s.ToString();
            //    s++;
            //}

            //ScottPlot.TickGenerators.NumericAutomatic tickGenX = new();
            //tickGenX.TargetTickCount = 10;
            //formsPlot.Plot.Axes.Bottom.TickGenerator = tickGenX;

            //ScottPlot.TickGenerators.NumericAutomatic tickGenX = new();
            //tickGenX.MinimumTickSpacing = 5;
            //formsPlot.Plot.Axes.Bottom.TickGenerator = tickGenX;


            //formsPlot.Plot.Axes.Bottom.SetTicks(tickPositions, tickLabels);

            // Lock the X axis min and max
            formsPlot.Plot.Axes.Rules.Clear();
            formsPlot.Plot.Axes.Rules.Add(new LockAxisRule());
        }

        /// <summary>
        /// Updates the state of the chart.
        /// </summary>
        /// <param name="chart">The <see cref="IChart"/> that specifies the new state of the chart.</param>
        public void UpdateChart(IChart chart)
        {
            ConfigureChart(formsPlot.Plot, chart);

            formsPlot.Refresh();
        }

        /// <summary>
        /// Configures a chart axis.
        /// </summary>
        /// <param name="axis">The <see cref="ScottPlot.IAxis"/> being configured.</param>
        /// <param name="config">An <see cref="Presentation.Workspace.Documents.Charts.IAxis"/> configuration being applied.</param>
        private void ConfigureAxis(ScottPlot.IAxis axis, Presentation.Workspace.Documents.Charts.IAxis config)
        {
            ConfigureLabel(axis.Label, config.Label);
            ConfigureScale(axis, config.Scale);

            axis.FrameLineStyle.Color = GetColour(config.ForeColour);
            axis.IsVisible = config.Visible;
        }

        /// <summary>
        /// Configures a chart.
        /// </summary>
        /// <param name="chart">The <see cref="Plot"/> being configured.</param>
        /// <param name="config">The <see cref="IChart"/> configuration being applied.</param>
        private void ConfigureChart(Plot chart, IChart config)
        {
            ConfigureLabel(chart.Axes.Title.Label, config.Title);
            ConfigureAxis(chart.Axes.Bottom, config.X1);
            ConfigureAxis(chart.Axes.Right, config.Y2);
            ConfigureAxis(chart.Axes.Left, config.Y1);
            ConfigureAxis(chart.Axes.Top, config.X2);
            ConfigurePlotArea(chart, config);

            chart.FigureBackground.Color = GetColour(config.BackColour);
        }

        /// <summary>
        /// Configures a label.
        /// </summary>
        /// <param name="label">The <see cref="LabelStyle"/> being configured.</param>
        /// <param name="config">The <see cref="ILabel"/> configuration being applied.</param>
        private void ConfigureLabel(LabelStyle label, ILabel config)
        {
            label.BackgroundColor = GetColour(config.BackColour);
            label.ForeColor = GetColour(config.ForeColour);
            label.IsVisible = config.Visible;
            label.Text = config.Text;

            var font = config.Font;

            label.Underline = font.Underline;
            label.FontName = font.Family;
            label.FontSize = font.Size;
            label.Italic = font.Italic;
            label.Bold = font.Bold;
        }

        /// <summary>
        /// Configures the plot area.
        /// </summary>
        /// <param name="chart">The <see cref="Plot"/> being configured.</param>
        /// <param name="config">The <see cref="IChart"/> configuration being applied.</param>
        private void ConfigurePlotArea(Plot chart, IChart config)
        {
            chart.DataBackground.Color = GetColour(config.BackColour); // Should be plot area background colour

            var majorGridLines = config.PlotArea.Grid.MajorGridLines;
            var minorGridLines = config.PlotArea.Grid.MinorGridLines;

            var grid = chart.Grid;

            if (config.PlotArea.Grid.Visible)
            {
                chart.ShowGrid();

                grid.MajorLineWidth = majorGridLines.Visible ? 2 : 0;

                grid.MajorLineColor = GetColour(majorGridLines.ForeColour).WithOpacity(0.3);



                grid.MinorLineWidth = minorGridLines.Visible ? 2 : 0;

                grid.MinorLineColor = GetColour(minorGridLines.ForeColour).WithOpacity(0.1);

            }
            else
            {
                chart.HideGrid();
            }
        }

        /// <summary>
        /// Configures the axis scale.
        /// </summary>
        /// <param name="axis">The <see cref="ScottPlot.IAxis"/> being configured.</param>
        /// <param name="config">The <see cref="IScale"/> configuration being applied.</param>
        private void ConfigureScale(ScottPlot.IAxis axis, IScale config)
        {
            axis.TickLabelStyle.IsVisible = config.Visible;

            ConfigureTickMarks(axis.MajorTickStyle, config.MajorTickMarks);
            ConfigureTickMarks(axis.MinorTickStyle, config.MinorTickMarks);
            ConfigureTickLabels(axis.TickLabelStyle, config.TickLabels);

            if (config.Reversed)
            {
                axis.Max = config.Minimum;
                axis.Min = config.Maximum;
            }
            else
            {
                axis.Max = config.Maximum;
                axis.Min = config.Minimum;
            }
        }

        /// <summary>
        /// Configures the tick labels.
        /// </summary>
        /// <param name="tickLabels">The <see cref="LabelStyle"/> being configured.</param>
        /// <param name="config">The <see cref="ITickLabels"/> configuration being applied.</param>
        private void ConfigureTickLabels(LabelStyle tickLabels, ITickLabels config)
        {
            tickLabels.BackgroundColor = GetColour(config.BackColour);
            tickLabels.ForeColor = GetColour(config.ForeColour);
            tickLabels.Rotation = config.Rotation;
            tickLabels.IsVisible = config.Visible;

            var font = config.Font;

            tickLabels.Underline = font.Underline;
            tickLabels.FontName = font.Family;
            tickLabels.FontSize = font.Size;
            tickLabels.Italic = font.Italic;
            tickLabels.Bold = font.Bold;
        }

        /// <summary>
        /// Configures the tick marks.
        /// </summary>
        /// <param name="tickMarks">The <see cref="TickMarkStyle"/> being configured.</param>
        /// <param name="config">The <see cref="ITickMarks"/> configuration being applied.</param>
        private void ConfigureTickMarks(TickMarkStyle tickMarks, ITickMarks config)
        {
            tickMarks.Color = GetColour(config.ForeColour);
            tickMarks.Length = config.Length;
            tickMarks.Hairline = true;
        }

        /// <summary>
        /// Gets the specifed <see cref="ScottPlot.Color"/> from the colour name or RGB value provided.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour either by name or as an RGB value.</param>
        /// <returns>The required <see cref="ScottPlot.Color"/>.</returns>
        private static ScottPlot.Color GetColour(string colour)
        {
            colour = colour.StartsWith('#') ? colour.Substring(1) : colour;

            var argb = 0;

            if (int.TryParse(colour, out argb))
            {
                return ScottPlot.Color.FromARGB(argb);
            }

            return ScottPlot.Color.FromColor(System.Drawing.Color.FromName(colour));
        }





















        readonly Coordinates[] DataPoints;
        Coordinates MouseDownCoordinates;
        Coordinates MouseNowCoordinates;
        CoordinateRect MouseSlectionRect => new(MouseDownCoordinates, MouseNowCoordinates);
        bool MouseIsDown = true;

        bool selectPoints = false;

        private Tuple<double[], double[]> GetData()
        {
            //var stars = new StarsRepository(); // TODO - This should be done through the presenter, this should not know about the Domain model

            //stars.Populate();

            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            var errors = 0;

            //foreach (var star in stars)
            {
                try
                {
                    //if (!string.IsNullOrEmpty(star.SpectralType.SpectralClass))
                    //{
                        //xValues.Add(star.BVColourIndex);
                        //yValues.Add(star.AbsoluteMagnitude);
                    //}
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
                newMarker.MarkerStyle.LineWidth = 2;
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
            if (bottom.Min < -0.5) bottom.Min = -0.5;
            if (bottom.Max > 2) bottom.Max = 2;

            IYAxis left = rp.Plot.Axes.Left;
            if (left.Min > 15) left.Min = 15;
            if (left.Max < -5) left.Max = -5;

        }
    }
}
