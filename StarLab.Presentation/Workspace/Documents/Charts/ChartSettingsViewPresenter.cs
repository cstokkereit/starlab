using log4net;
using StarLab.Shared.Resources;
using Stratosoft.Commands;
using System.Diagnostics;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of a chart settings panel.
    /// </summary>
    internal class ChartSettingsViewPresenter : ChildViewPresenter<IChartSettingsView, IDocumentController>, IChartSettingsViewPresenter, IChartSettingsController, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsViewPresenter)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, SettingsGroupManager<IChartSettingsView>> groupManagers = new Dictionary<string, SettingsGroupManager<IChartSettingsView>>(); // A dictionary that contains the group managers indexed by group.

        private readonly IChartSettingsUseCaseService useCaseService; // A service that executes the use cases that implement the chart settings panel functionality.

        private SettingsGroupManager<IChartSettingsView>? groupManager; // Displays the currently selected settings group.

        private IChartSettings? chart; // Represents the current state of the chart.

        private IWorkspace? workspace; // The workspace that contains the chart.

        private string id; // The ID of the document that contains the chart.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartSettingsView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ChartSettingsViewPresenter(IChartSettingsView view, ICommandManager commands, IServiceRegistry services, IApplicationSettings settings, IEventAggregator events)
            : base(view, commands, settings, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            useCaseService = services.GetService<IChartSettingsUseCaseService>();

            View.MinimumSize = new Size(600, 150);

            View.Attach(this);

            id = string.Empty;
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~ChartSettingsViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Applies the preview settings to the chart view.
        /// </summary>
        /// <param name="chart">The <see cref="IChartSettings"/> that specifies the state of the chart.</param>
        public void ApplyPreviewSettings(IChartSettings chart)
        {
            useCaseService.UpdateChart(ParentController.ID, chart);

            this.chart = chart;
        }

        /// <summary>
        /// Applies the chart settings to the document.
        /// </summary>
        public void ApplySettings()
        {
            if (string.IsNullOrEmpty(id)) throw new InvalidOperationException($"{StringResources.ObjectNotInitialised} {string.Format(StringResources.VariableNotSet, StringResources.DocumentID)}");
            if (workspace == null) throw new InvalidOperationException($"{StringResources.ObjectNotInitialised} {string.Format(StringResources.VariableNotSet, StringResources.Workspace)}");
            if (chart == null) throw new InvalidOperationException($"{StringResources.ObjectNotInitialised} {string.Format(StringResources.VariableNotSet, StringResources.Chart)}");
            
            useCaseService.UpdateDocument(workspace, id, chart);
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

            View.AttachOKButtonCommand(CreateCommand(Actions.ApplySettings, () => {
                ParentController.HideSplitContent(View.Name);
                ApplySettings();
            }));

            View.AttachCancelButtonCommand(CreateCommand(Actions.RevertSettings, () => {
                ParentController.HideSplitContent(View.Name);
                RevertSettings();
            }));

            CreateSettingsGroups();

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
        }

        /// <summary>
        /// Reverts the changes to the settings.
        /// </summary>
        public void RevertSettings()
        {
            var controller = ParentController.GetController<IChartController>();

            controller.UpdatePreview();
        }

        /// <summary>
        /// Shows the settings for the specified settings group.
        /// </summary>
        /// <param name="group">The name of the settings group to show.</param>
        public void ShowSettingsGroup(string group)
        {
            if (chart == null) throw new InvalidOperationException($"{StringResources.ObjectNotInitialised} {string.Format(StringResources.VariableNotSet, StringResources.Chart)}");

            Debug.Assert(groupManagers.ContainsKey(group));

            View.Clear();

            groupManager = groupManagers[group];

            groupManager.ShowSettings(chart);
        }

        /// <summary>
        /// Updates the chart settings.
        /// </summary>
        /// <param name="document">The <see cref="IChartDocument"/> that contains the chart.</param>
        public void UpdateSettings(IChartDocument document)
        {
            chart = new ChartSettings(document.Chart);

            id = document.ID;
        }

        /// <summary>
        /// Adds the <see cref="SettingsGroupManager{TView}"/> to the dictionary.
        /// </summary>
        /// <param name="manager">The <see cref="SettingsGroupManager{TView}"/> to add.</param>
        private void AddGroupManager(SettingsGroupManager<IChartSettingsView> manager)
        {
            groupManagers.Add(manager.Group, manager);
        }

        /// <summary>
        /// Releases any resources used by the <see cref="ChartSettingsViewPresenter"/> object.
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
        /// Creates the axis settings group nodes and their respective group managers.
        /// </summary>
        private void CreateAxisSettingsGroups(string name, string parentKey, string text)
        {
            var axis = View.AddNode(name, parentKey, text);

            AddGroupManager(new AxisSettingsGroupManager(View, axis));

            AddGroupManager(new LabelSettingsGroupManager(View, View.AddNode(Constants.Label, axis, StringResources.Label)));

            var scale = View.AddNode(Constants.Scale, axis, StringResources.Scale);

            AddGroupManager(new ScaleSettingsGroupManager(View, scale));

            AddGroupManager(new TickMarkSettingsGroupManager(View, View.AddNode(Constants.MinorTickMarks, scale, StringResources.MinorTickMarks)));
            AddGroupManager(new TickMarkSettingsGroupManager(View, View.AddNode(Constants.MajorTickMarks, scale, StringResources.MajorTickMarks)));
            AddGroupManager(new TickLabelSettingsGroupManager(View, View.AddNode(Constants.TickLabels, scale, StringResources.TickLabels)));
        }

        /// <summary>
        /// Creates the settings group nodes and their respective group managers.
        /// </summary>
        private void CreateSettingsGroups()
        {
            AddGroupManager(new ChartSettingsGroupManager(View, View.AddNode(Constants.Chart, StringResources.Chart)));

            AddGroupManager(new LabelSettingsGroupManager(View, View.AddNode(Constants.Title, Constants.Chart, StringResources.Title)));

            var axes = View.AddNode(Constants.Axes, Constants.Chart, StringResources.Axes);

            AddGroupManager(new AxesSettingsGroupManager(View, axes));

            CreateAxisSettingsGroups(Constants.AxisX1, axes, StringResources.AxisX1);
            CreateAxisSettingsGroups(Constants.AxisX2, axes, StringResources.AxisX2);
            CreateAxisSettingsGroups(Constants.AxisY1, axes, StringResources.AxisY1);
            CreateAxisSettingsGroups(Constants.AxisY2, axes, StringResources.AxisY2);

            var plotArea = View.AddNode(Constants.PlotArea, Constants.Chart, StringResources.PlotArea);

            AddGroupManager(new PlotAreaSettingsGroupManager(View, plotArea));

            var grid = View.AddNode(Constants.Grid, plotArea, StringResources.Grid);

            AddGroupManager(new GridSettingsGroupManager(View, grid));

            AddGroupManager(new GridLineSettingsGroupManager(View, View.AddNode(Constants.MinorGridLines, grid, StringResources.MinorGridLines)));
            AddGroupManager(new GridLineSettingsGroupManager(View, View.AddNode(Constants.MajorGridLines, grid, StringResources.MajorGridLines)));
        }
    }
}
