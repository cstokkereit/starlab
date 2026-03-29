using log4net;
using StarLab.Shared.Properties;
using StarLab.Shared.Resources;
using Stratosoft.Commands;
using System.Reflection;

namespace StarLab.Presentation.Help
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IAboutView"/>.
    /// </summary>
    internal class AboutViewPresenter : ChildViewPresenter<IAboutView, IViewController>, IAboutViewPresenter, IChildViewController
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AboutViewPresenter)); // The logger that will be used for writing log messages.

        //private readonly IAboutUseCaseService useCases; // 

        //private IDialogController parentController; // The controller that can be used to control the parent dialog box. TODO - probably not needed

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IAboutView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public AboutViewPresenter(IAboutView view, ICommandManager commands, IServiceRegistry services, IApplicationSettings settings, IEventAggregator events)
            : base(view, commands, settings, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            View.Attach(this);
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~AboutViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="AboutViewPresenter"/> object.
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
            if (Initialised) throw new InvalidOperationException(string.Format(Resources.AlreadyInitialised, nameof(AboutViewPresenter)));

            base.Initialise(controller);

            //View.SetCompanyName(Resources.CompanyName);
            //View.SetCopyright(Resources.Copyright);
            //View.SetDescription(Resources.ProductDescription);
            //View.SetLogo("");
            //View.SetProductName(Resources.StarLab);
            //View.SetVersion(string.Format(Resources.Version, GetVersion()));

            log.Debug(string.Format(LogEntries.Initialised, nameof(AboutViewPresenter)));
        }

        //public override void Run(IDialogConfiguration config)
        //{
        //    base.Run(config);

        //    ParentController.Show();
        //}

        /// <summary>
        /// Releases all resources used by the <see cref="AboutViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                View.Detach();
            }
        }

        /// <summary>
        /// Gets the version information.
        /// </summary>
        /// <returns>The version information.</returns>
        private string GetVersion()
        {
            string version = string.Empty;

            Assembly assembly = Assembly.GetExecutingAssembly();

            if (assembly != null)
            {
                AssemblyName name = assembly.GetName();

                if (name != null && name.Version != null)
                {
                    version = name.Version.ToString();
                }
            }

            return version;
        }
    }
}
