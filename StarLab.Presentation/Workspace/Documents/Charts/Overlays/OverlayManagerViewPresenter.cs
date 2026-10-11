using log4net;
using StarLab.Presentation.Configuration;
using StarLab.Shared;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Controls the behaviour of a chart overlays panel.
    /// </summary>
    internal class OverlayManagerViewPresenter : ChildViewPresenter<IOverlayManagerView, IDocumentController>, IChildViewController, IOverlayManagerViewPresenter
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OverlayManagerViewPresenter)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="OverlayManagerViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IOverlayManagerView"/> controlled by this presenter.</param>
        /// <param name="document">The <see cref="IDocument"/> that the presenter .</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public OverlayManagerViewPresenter(IOverlayManagerView view, IDocument document, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            //useCaseService = services.GetService<IChartSettingsUseCaseService>();

            //this.document = document ?? throw new ArgumentNullException(nameof(document));

            View.MinimumSize = new Size(600, 150);

            View.Attach(this);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="ChartSettingsViewPresenter"/> object.
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

            ParentController.AddToolbarButton(Constants.ShowOverlays, StringResources.Overlays, ImageResources.Overlays, CreateCommand(Actions.ShowOverlays, () => ParentController.ShowSplitContent(View.Name)));

            //View.AttachOKButtonCommand(CreateCommand(Actions.ApplySettings, () => {
            //    ParentController.HideSplitContent(View.Name);
            //    ApplySettings();
            //}));

            //View.AttachCancelButtonCommand(CreateCommand(Actions.RevertSettings, () => {
            //    ParentController.HideSplitContent(View.Name);
            //    RevertSettings();
            //}));

            View.Initialise();

            //CreateSettingsNavigator();

            log.Debug(LogEntries.PresenterInitialised(GetType()));
        }

        // Overlays:
        // Magnitude classes
        // Named stars
        // Brightest stars
        // Variable types
        // Stellar types e.g. Wolf rayet, brown dwarf
        // Evolutionary tracks (could prove difficult - can they be calculated?)
        // Standard stars for spectral and magnitude classes
        // Our sun
        // Radius lines (R/Rsun)
        // Mass lines (M/Msun)

        // Custom overlays - for the future
    }
}
