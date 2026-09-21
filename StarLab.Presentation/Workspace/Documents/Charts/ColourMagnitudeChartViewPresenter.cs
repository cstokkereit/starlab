using log4net;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Presentation.Configuration;
using StarLab.Shared;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IChartView"/>.
    /// </summary>
    public class ColourMagnitudeChartViewPresenter : ChildViewPresenter<IChartView, IDocumentController>, IChartViewPresenter, IChartController, IChartOutputPort, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ColourMagnitudeChartViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IChartUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        private readonly ChartData data; // The current chart data.

        private IChart? chart; // The chart that the view represents.

        private IDocument? document; // The document that contains the chart.

        private IWorkspace? workspace; // The workspace that contains the document.

        private bool dirty; // A flag indicating that the chart data needs to be refreshed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourMagnitudeChartViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartView"/> controlled by this presenter.</param>
        /// <param name="document">The <see cref="IDocument"/> that the presenter .</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ColourMagnitudeChartViewPresenter(IChartView view, IDocument document, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events) 
        {
            this.document = document ?? throw new ArgumentNullException(nameof(document));

            ArgumentNullException.ThrowIfNull(services, nameof(services));

            useCaseService = services.GetService<IChartUseCaseService>();
            
            data = new ChartData();

            dirty = true;

            View.Attach(this);


            //var converter = new SpectralClassConverter();

            // Make the IForwardOnlyCursor implement async methods
            // Add a Data section to the settings view to choose the data series for the x and y axes
            // Use the nomenclature dictionary to load the names of data fields and series, units, symbols etc
            // Scale points with zoom
            // Dragable axis lines
            // scale points according to number of stars
            // Colour points - spectrum
            // Colour back ground - spectrum
            // Tick mark density
            // Teff and Luminosity axes
            // Select points
            // Add magnitude class overlay
            // Add variable star overlays
            // Select data for each axis (B-V, U-I, Spectral Class, Teff, Luminosity) - enforce that this is a temp/luminosity not a 2 colour diagram
            // Table of data (B-V, U-I, Spectral Class, Teff, Luminosity) available to the chart - should not add other fields. So may include parallax/distance apparent magnitude.
            // Filter for data used to make the table and variable types.
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~ColourMagnitudeChartViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="ColourMagnitudeChartViewPresenter"/> object.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (Initialised) throw new InvalidOperationException(ExceptionMessages.PresenterAlreadyInitialised(GetType()));

            base.Initialise(controller);

            View.Initialise();

            //View.MinimumSize = new Size(200, 200);

            log.Debug(LogEntries.PresenterInitialised(GetType()));
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            workspace = args.Workspace;

            UpdateChart();
        }

        /// <summary>
        /// 
        /// </summary>
        public void SetData(List<StarDTO> stars)
        {
            if (chart != null)
            {
                var nn = stars.Count;

                var xs = new double[nn];
                var ys = new double[nn];

                for (var n = 0; n < nn; n++)
                {
                    xs[n] = stars[n].ColourIndex;
                    ys[n] = stars[n].AbsoluteMagnitude;
                }

                data.AddSeries("B-V", xs);
                data.AddSeries("Absolute Magnitude", ys);

                View.UpdateChart(chart, data);

                dirty = false;
            }
        }

        /// <summary>
        /// Updates the chart following a change to the document or workspace.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the state of the chart.</param>
        public void UpdateChart(IChart chart)
        {
            View.UpdateChart(chart);

            this.chart = chart;
        }

        /// <summary>
        /// Applies the new chart settings to the preview.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the state of the chart.</param>
        public void UpdatePreview(ChartDTO dto)
        {
            View.UpdateChart(new Chart(dto));
        }

        /// <summary>
        /// Reverts the preview to the old chart settings.
        /// </summary>
        public void UpdatePreview()
        {
            if(chart != null) View.UpdateChart(chart);
        }

        /// <summary>
        /// Releases any resources used by the <see cref="ColourMagnitudeChartViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                View.Detach();
            }
        }

        /// <summary>
        /// Updates the chart.
        /// </summary>
        private void UpdateChart()
        {
            if (dirty && workspace != null && document != null)
            {
                useCaseService.UpdateChart(workspace, document.ID);
            }
        }
    }
}
