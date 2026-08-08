using log4net;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// Controls the behaviour of a table settings panel.
    /// </summary>
    public class TableSettingsViewPresenter : ChildViewPresenter<ITableSettingsView, IDocumentController>, ITableSettingsViewPresenter, ITableSettingsController, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TableSettingsViewPresenter)); // The logger that will be used for writing log messages.

        //private readonly Dictionary<string, SettingsGroupManager<ITableSettingsView>> groupManagers = new Dictionary<string, SettingsGroupManager<ITableSettingsView>>(); // A dictionary that contains the group managers indexed by group.

        private readonly ITableSettingsUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        //private SettingsGroupManager<IChartSettingsView>? groupManager; // Displays the currently selected settings group.

        private ITableSettings? table; // Represents the current state of the table.

        private IWorkspace? workspace; // The workspace that contains the table.

        private DocumentID? documentId; // The ID of the document that contains the table.

        /// <summary>
        /// Initialises a new instance of the <see cref="TableSettingsViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="ITableSettingsView"/> controlled by this presenter.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public TableSettingsViewPresenter(ITableSettingsView view, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            useCaseService = services.GetService<ITableSettingsUseCaseService>();

            View.MinimumSize = new Size(600, 150);

            View.Attach(this);
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~TableSettingsViewPresenter()
        {
            Dispose(false);
        }

        public void ApplySettings()
        {
            throw new NotImplementedException();
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
            if (Initialised) throw new InvalidOperationException(string.Format(StringResources.AlreadyInitialised, nameof(ChartSettingsViewPresenter)));

            base.Initialise(controller);

            ParentController.AddToolbarButton(Constants.ShowSettings, StringResources.Settings, ImageResources.Settings, CreateCommand(Actions.ShowSplitContent, () => ParentController.ShowSplitContent(View.Name)));

            View.AttachOKButtonCommand(CreateCommand(Actions.ApplySettings, () =>
            {
                ParentController.HideSplitContent(View.Name);
                ApplySettings();
            }));

            View.AttachCancelButtonCommand(CreateCommand(Actions.RevertSettings, () =>
            {
                ParentController.HideSplitContent(View.Name);
                RevertSettings();
            }));

            //CreateSettingsGroups();

            View.Initialise();

            log.Debug(string.Format(LogEntries.Initialised, $"{nameof(ChartSettingsViewPresenter)}({View.Name})"));
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            workspace = args.Workspace;

            //View.SelectNode(Constants.Chart);
        }

        /// <summary>
        /// Reverts the changes to the settings.
        /// </summary>
        public void RevertSettings()
        {
            var controller = ParentController.GetController<ITableController>();

            controller.UpdatePreview();
        }

        /// <summary>
        /// Updates the table settings.
        /// </summary>
        /// <param name="document">The <see cref="ITableDocument"/> that contains the table.</param>
        public void UpdateSettings(ITableDocument document)
        {
            table = new TableSettings(document.Table);

            documentId = document.ID;
        }

        /// <summary>
        /// Releases any resources used by the <see cref="TableSettingsViewPresenter"/> object.
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
