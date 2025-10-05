using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDockableView"/>.
    /// </summary>
    public class ToolViewPresenter : Presenter<IDockableView>, IDockableViewPresenter, IViewController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ToolViewPresenter)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="ToolViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDockableView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ToolViewPresenter(IDockableView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, settings, mapper, events)
        {
            View.Attach(this);

            Name = Controllers.GetViewControllerName(View.ID);

            Location = Constants.DockRight; // TODO - Optional default locations?

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(ToolViewPresenter)));
        }

        /// <summary>
        /// Gets or sets the current location of the view.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name { get; }

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
            }
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
        public void Show(IView view)
        {
            View.Show(view);
        }
    }
}
