using log4net;
using StarLab.Shared.Properties;
using StarLab.Shared.Resources;
using Stratosoft.Commands;

namespace StarLab.Presentation.Options
{
    // https://learn.microsoft.com/en-us/dotnet/desktop/winforms/advanced/how-to-create-application-settings?view=netframeworkdesktop-4.8

    /// <summary>
    /// Controls the behaviour of an <see cref="IOptionsView"/>.
    /// </summary>
    internal class OptionsViewPresenter : ChildViewPresenter<IOptionsView, IViewController>, IOptionsViewPresenter, IChildViewController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionsViewPresenter)); // The logger that will be used for writing log messages.

        //private readonly IOptionsUseCaseService useCases; // A facade that aggregates the available use cases.

        /// <summary>
        /// Initialises a new instance of the <see cref="OptionsViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IOptionsView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public OptionsViewPresenter(IOptionsView view, ICommandManager commands, IServiceRegistry services, IApplicationSettings settings, IEventAggregator events)
            : base(view, commands, settings, events) 
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            View.Attach(this);
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~OptionsViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="OptionsViewPresenter"/> object.
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
            if (Initialised) throw new InvalidOperationException(string.Format(Resources.AlreadyInitialised, nameof(OptionsViewPresenter)));

            base.Initialise(controller);

            log.Debug(string.Format(LogEntries.Initialised, nameof(OptionsViewPresenter)));
        }

        /// <summary>
        /// Releases all resources used by the <see cref="OptionsViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                View.Detach();
            }
        }
    }
}
