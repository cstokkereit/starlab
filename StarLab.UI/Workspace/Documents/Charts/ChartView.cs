using log4net;
using ScottPlot;
using ScottPlot.Plottables;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared;

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

        readonly ScottPlot.Plottables.Rectangle selector; //

        private IChartViewPresenter? presenter; // The presenter that controls the view.

        private Scatter? points; //

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartView"> class.
        /// </summary>
        public ChartView()
        {
            InitializeComponent();

            ID = ViewIDs.Chart;
            Name = ViewNames.Chart;

            selector = chart.Plot.Add.Rectangle(0, 0, 0, 0);



            // add events to trigger in response to mouse actions
            chart.MouseMove += OnMouseMove;
            chart.MouseDown += OnMouseDown;
            chart.MouseUp += OnMouseUp;
        }

        /// <summary>
        /// Gets the <see cref="IClipboard"/>.
        /// </summary>
        public IClipboard Clipboard { get; }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public ViewID ID { get; }

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
            if (this.presenter != null) throw new InvalidOperationException(ExceptionMessages.PresenterAlreadyAttached);

            this.presenter = (IChartViewPresenter)presenter;

            log.Debug(LogEntries.PresenterAttached(presenter.GetType()));
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public void Detach()
        {
            if (presenter != null)
            {
                var type = presenter.GetType();

                presenter = null;

                log.Debug(LogEntries.PresenterDetached(type));
            }
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        public void Initialise()
        {
            selector.FillStyle.Color = Colors.Red.WithAlpha(.2);
        }

        /// <summary>
        /// Updates the chart following a change to the chart data.
        /// </summary>
        /// <param name="config">An <see cref="IChart"/> used to configure the chart.</param>
        /// <param name="data">An <see cref="IChartData"> that holds data that will be used to generate the chart.</param>
        public void UpdateChart(IChart config, IChartData data)
        {
            points = chart.Plot.Add.ScatterPoints(data.GetSeries("B-V"), data.GetSeries("Absolute Magnitude"));

            ConfigurePoints(config.PlotArea.Points);

            chart.Refresh();
        }

        /// <summary>
        /// Updates the chart following a change to the chart configuration.
        /// </summary>
        /// <param name="config">The <see cref="IChart"/> used to configure the chart.</param>
        public void UpdateChart(IChart config)
        {
            ConfigureChart(config);

            chart.Refresh();
        }

        /// <summary>
        /// Configures a chart axis.
        /// </summary>
        /// <param name="axis">The <see cref="ScottPlot.IAxis"/> being configured.</param>
        /// <param name="config">An <see cref="Presentation.Workspace.Documents.Charts.IAxis"/> configuration being applied.</param>
        private void ConfigureAxis(ScottPlot.IAxis axis, Presentation.Workspace.Documents.Charts.IAxis config)
        {
            axis.FrameLineStyle.Color = GetColour(config.Colour);

            ConfigureLabel(axis.Label, config.Label);
            ConfigureScale(axis, config.Scale);

            axis.IsVisible = config.Visible;
        }

        /// <summary>
        /// Configures a chart.
        /// </summary>
        /// <param name="config">The <see cref="IChart"/> used to configure the chart.</param>
        private void ConfigureChart(IChart config)
        {
            var plot = chart.Plot;

            plot.FigureBackground.Color = GetColour(config.BackColour);

            ConfigureLabel(plot.Axes.Title.Label, config.Title);

            ConfigureAxis(plot.Axes.Bottom, config.X1);
            ConfigureAxis(plot.Axes.Right, config.Y2);
            ConfigureAxis(plot.Axes.Left, config.Y1);
            ConfigureAxis(plot.Axes.Top, config.X2);

            ConfigurePlotArea(plot, config);
        }

        /// <summary>
        /// Configures a label.
        /// </summary>
        /// <param name="label">The <see cref="LabelStyle"/> being configured.</param>
        /// <param name="config">The <see cref="ILabel"/> used to configure the label.</param>
        private void ConfigureLabel(LabelStyle label, ILabel config)
        {
            label.ForeColor = GetColour(config.Colour);
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
        /// <param name="config">The <see cref="IChart"/> used to configure the plot area.</param>
        private void ConfigurePlotArea(Plot chart, IChart config)
        {
            chart.DataBackground.Color = GetColour(config.PlotArea.BackColour);

            chart.Layout.Fixed(new PixelPadding(50, 50, 50, 50));

            var majorGridLines = config.PlotArea.Grid.MajorGridLines;
            var minorGridLines = config.PlotArea.Grid.MinorGridLines;

            var grid = chart.Grid;

            if (config.PlotArea.Grid.Visible || majorGridLines.Visible || minorGridLines.Visible)
            {
                chart.ShowGrid();

                grid.MajorLineColor = GetColour(majorGridLines.Colour).WithOpacity(majorGridLines.Opacity);
                grid.MinorLineColor = GetColour(minorGridLines.Colour).WithOpacity(minorGridLines.Opacity);

                grid.MajorLineWidth = majorGridLines.Visible ? 2 : 0;
                grid.MinorLineWidth = minorGridLines.Visible ? 2 : 0;
            }
            else
            {
                chart.HideGrid();
            }

            ConfigurePoints(config.PlotArea.Points);
        }

        /// <summary>
        /// Configures the data points.
        /// </summary>
        /// <param name="config">The <see cref="IChart"/> used to configure the data points.</param>
        private void ConfigurePoints(IPoints config)
        {
            if (points != null)
            {
                points.MarkerColor = GetColour(config.Colour);
                points.IsVisible = config.Visible;
                points.MarkerSize = config.Size;
            }
        }

        /// <summary>
        /// Configures the axis scale.
        /// </summary>
        /// <param name="axis">The <see cref="ScottPlot.IAxis"/> being configured.</param>
        /// <param name="config">The <see cref="IScale"/> used to configure the scale.</param>
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
        /// <param name="config">The <see cref="ITickLabels"/> used to configure the tick labels.</param>
        private void ConfigureTickLabels(LabelStyle tickLabels, ITickLabels config)
        {
            tickLabels.ForeColor = GetColour(config.Colour);
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
        /// <param name="config">The <see cref="ITickMarks"/> used to configure the tick marks.</param>
        private void ConfigureTickMarks(TickMarkStyle tickMarks, ITickMarks config)
        {
            tickMarks.Color = GetColour(config.Colour);
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






        // This is all temporary

        // Split data into vertical series and colour according to spectral class



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
       //formsPlot.Plot.Axes.Rules.Clear();
       // formsPlot.Plot.Axes.Rules.Add(new LockAxisRule());











        Coordinates MouseDownCoordinates;
        Coordinates MouseNowCoordinates;
        CoordinateRect MouseSelectionRect => new(MouseDownCoordinates, MouseNowCoordinates);
        bool MouseIsDown = true;

        bool selectPoints = false;

       
        private void OnMouseDown(object? sender, MouseEventArgs e)
        {
            if (!selectPoints)
                return;

            MouseIsDown = true;
            selector.IsVisible = true;
            MouseDownCoordinates = chart.Plot.GetCoordinates(e.X, e.Y);
            //formsPlot.Interaction.Disable(); TODO - disable the default click-drag-pan behavior 
        }

        private void OnMouseUp(object? sender, MouseEventArgs e)
        {
            if (!selectPoints)
                return;

            MouseIsDown = false;
            selector.IsVisible = false;

            // clear old markers
            chart.Plot.Remove<Marker>();

            // identify selectedPoints
            var selectedPoints = points?.Data.GetScatterPoints().Where(x => MouseSelectionRect.Contains(x));

            // add markers to outline selected points
            foreach (Coordinates selectedPoint in selectedPoints)
            {
                var newMarker = chart.Plot.Add.Marker(selectedPoint);
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
            chart.Refresh();
            //formsPlot.Interaction.Enable(); // re-enable the default click-drag-pan behavior
        }

        private void OnMouseMove(object? sender, MouseEventArgs e)
        {
            if (!MouseIsDown || !selectPoints) return;

            MouseNowCoordinates = chart.Plot.GetCoordinates(e.X, e.Y);
            selector.CoordinateRect = MouseSelectionRect;
            chart.Refresh();
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
