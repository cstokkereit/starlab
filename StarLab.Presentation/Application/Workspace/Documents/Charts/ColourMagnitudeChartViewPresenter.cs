using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IChartView"/>.
    /// </summary>
    internal class ColourMagnitudeChartViewPresenter : ChildViewPresenter<IChartView, IDocumentController>, IChartViewPresenter, IChartController
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ColourMagnitudeChartViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IConfigurationService"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ColourMagnitudeChartViewPresenter(IChartView view, ICommandManager commands, IUseCaseFactory factory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, configuration, mapper, events) { }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => throw new NotImplementedException();

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
    }
}
