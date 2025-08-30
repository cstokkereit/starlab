using AutoMapper;
using StarLab.Application;
using StarLab.Presentation.Configuration;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDockableView"/>.
    /// </summary>
    public class ToolViewPresenter : Presenter, IDockableViewPresenter, IViewController
    {
        private readonly IDockableView view; // The view controlled by the presenter.

        /// <summary>
        /// Initialises a new instance of the <see cref="ToolViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDockableView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IApplicationConfiguration"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ToolViewPresenter(IDockableView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.view = view;

            Location = Constants.DockRight; // TODO - Optional default locations?
        }

        /// <summary>
        /// Gets or sets the current location of the view.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => ControllerNames.GetViewControllerName(view.ID);

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                view.Initialise(controller);
            }
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            this.view.Show(view);
        }
    }
}
