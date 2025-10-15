using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Shared.Properties;
using Stratosoft.Commands;
using System.Diagnostics;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IChartView"/>.
    /// </summary>
    internal class ColourMagnitudeChartViewPresenter : ChildViewPresenter<IChartView, IDocumentController>, IChartViewPresenter, IChartController, IChartOutputPort
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ColourMagnitudeChartViewPresenter)); // The logger that will be used for writing log messages.

        private IChart? chart; // The chart that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourMagnitudeChartViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ColourMagnitudeChartViewPresenter(IChartView view, ICommandManager commands, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, settings, mapper, events) 
        {
            View.Attach(this);
            
            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(ColourMagnitudeChartViewPresenter)));
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => Controllers.ChartController;

        /// <summary>
        /// Activates the view.
        /// </summary>
        public void Activate()
        {
            // Do Nothing
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                View.Initialise(controller);

                //View.MinimumSize = new Size(200, 200);
            }
        }

        /// <summary>
        /// Updates the view with the new <see cref="IChart"/> definition following a change to the document or workspace. This will replace the current chart definition.
        /// </summary>
        /// <param name="chart">An <see cref="IChart"/> that specifies the state of the chart.</param>
        public void UpdateChart(IChart chart)
        {
            View.UpdateChart(chart);

            this.chart = chart;
        }

        /// <summary>
        /// Updates the view with the preview chart definition. This does not replace the current chart definition.
        /// </summary>
        /// <param name="dto">A <see cref="ChartDTO"/> that specifies the state of the chart.</param>
        public void UpdateChart(ChartDTO dto)
        {
            View.UpdateChart(new Chart(dto));
        }

        /// <summary>
        /// Updates the view with the current chart definition. This will revert any preview changes.
        /// </summary>
        public void UpdateChart()
        {
            Debug.Assert(chart != null);

            View.UpdateChart(chart);
        }
    }
}
