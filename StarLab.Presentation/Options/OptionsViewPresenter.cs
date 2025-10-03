using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation.Options
{
    // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-create-application-settings?view=netframeworkdesktop-4.8

    /// <summary>
    /// Controls the behaviour of an <see cref="IOptionsView"/>.
    /// </summary>
    internal class OptionsViewPresenter : ChildViewPresenter<IOptionsView, IDialogController>, IOptionsViewPresenter, IChildViewController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionsViewPresenter)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="OptionsViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IOptionsView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public OptionsViewPresenter(IOptionsView view, ICommandManager commands, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, settings, mapper, events) 
        {
            log.Debug(string.Format(Resources.InstanceCreated, nameof(OptionsViewPresenter)));
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

            }
        }
    }
}
