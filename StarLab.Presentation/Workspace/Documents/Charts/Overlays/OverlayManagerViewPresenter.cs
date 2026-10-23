using log4net;
using StarLab.Shared;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Controls the behaviour of a chart overlays panel.
    /// </summary>
    internal class OverlayManagerViewPresenter : ChildViewPresenter<IOverlayManagerView, IDocumentController>, IChildViewController, IOverlayManagerViewPresenter, ISubscriber<ChartSettingsChangedEventArgs>, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OverlayManagerViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IChartOverlaysUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        private IChartSettings? chart; // Represents the current state of the chart.

        private readonly IDocument document; // The document that contains the chart.

        private IWorkspace? workspace; // The workspace that contains the document.

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

            useCaseService = services.GetService<IChartOverlaysUseCaseService>();

            this.document = document ?? throw new ArgumentNullException(nameof(document));

            View.MinimumSize = new Size(600, 150);

            View.Attach(this);
        }

        /// <summary>
        /// Adds the specified overlay to the chart.
        /// </summary>
        /// <param name="name">The name of the overlay.</param>
        public void AddOverlay(string name)
        {
            if (document == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);

            useCaseService.AddChartOverlay(document.ID, chart, name);
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

            View.Initialise();

            log.Debug(LogEntries.PresenterInitialised(GetType()));
        }

        /// <summary>
        /// Event handler for the ChartSettingsChanged event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(ChartSettingsChangedEventArgs args)
        {
            if (args.ID == document.ID)
            {
                chart = new ChartSettings(args.Chart);

                View.Clear();

                if (chart != null)
                {
                    var overlays = GetOverlayNames(chart);

                    foreach (var overlay in SessionContext.Configuration.Overlays)
                    {
                        View.AddOverlay(overlay, GetCaption(overlay), overlays.Contains(overlay));
                    }
                }
            }
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            workspace = args.Workspace;
        }

        /// <summary>
        /// Removes the specified overlay from the chart.
        /// </summary>
        /// <param name="name">The name of the overlay.</param>
        public void RemoveOverlay(string name)
        {
            if (document == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);

            useCaseService.RemoveChartOverlay(document.ID, chart, name);
        }

        /// <summary>
        /// Runs the child view.
        /// </summary>
        public override void Run()
        {
            View.Clear();

            if (chart != null)
            {
                var overlays = GetOverlayNames(chart);

                foreach (var overlay in SessionContext.Configuration.Overlays)
                {
                    View.AddOverlay(overlay, GetCaption(overlay), overlays.Contains(overlay));
                }
            }
        }

        /// <summary>
        /// Applies the chart settings to the document.
        /// </summary>
        private void ApplySettings()
        {
            if (workspace == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);
            if (document == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);

            useCaseService.UpdateChart(workspace, document.ID, chart);
        }

        /// <summary>
        /// Gets the names of the overlays that have been added to a chart.
        /// </summary>
        /// <param name="chart">An <see cref="IChartSettings"/> that may have added overlays.</param>
        /// <returns></returns>
        private List<string> GetOverlayNames(IChartSettings chart)
        {
            var names = new List<string>();

            foreach (var overlay in chart.Overlays)
            {
                names.Add(overlay.Name);
            }

            return names;
        }

        /// <summary>
        /// Reverts the changes to the settings.
        /// </summary>
        private void RevertSettings()
        {
            var controller = ParentController.GetController<IChartController>(new ControllerID(Constants.Chart));

            controller.RevertChanges();
        }
    }
}
