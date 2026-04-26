using log4net;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Presentation.Configuration;
using StarLab.Shared;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IChartView"/>.
    /// </summary>
    public class TwoColourChartViewPresenter : ChildViewPresenter<IChartView, IDocumentController>, IChartViewPresenter, IChartController, IChartOutputPort
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TwoColourChartViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IChartUseCaseService useCases; // A service that executes the use cases that implement the functionality.

        private IChart? chart; // The chart that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="TwoColourChartViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartView"/> controlled by this presenter.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public TwoColourChartViewPresenter(IChartView view, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events) 
        {
            ArgumentNullException.ThrowIfNull(services, nameof(useCases));

            useCases = services.GetService<IChartUseCaseService>();

            View.Attach(this);
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~TwoColourChartViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="TwoColourChartViewPresenter"/> object.
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
            if (Initialised) throw new InvalidOperationException(string.Format(Resources.AlreadyInitialised, nameof(TwoColourChartViewPresenter)));

            base.Initialise(controller);

            View.Initialise();

            //View.MinimumSize = new Size(200, 200);

            log.Debug(string.Format(LogEntries.Initialised, $"{nameof(TwoColourChartViewPresenter)}({View.Name})"));
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
        /// Releases any resources used by the <see cref="TwoColourChartViewPresenter"/> object.
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
    }
}
