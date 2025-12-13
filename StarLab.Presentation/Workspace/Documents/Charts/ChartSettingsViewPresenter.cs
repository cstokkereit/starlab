using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts;
using Stratosoft.Commands;
using System.Diagnostics;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IChartSettingsView"/>.
    /// </summary>
    internal class ChartSettingsViewPresenter : ChildViewPresenter<IChartSettingsView, IDocumentController>, IChartSettingsViewPresenter, IChartSettingsController, ISubscriber<WorkspaceChangedEventArgs>
    {
        private readonly Dictionary<string, SettingsGroupManager<IChartSettingsView>> groupManagers = new Dictionary<string, SettingsGroupManager<IChartSettingsView>>(); // A dictionary that contains the group managers indexed by group.

        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsViewPresenter)); // The logger that will be used for writing log messages.

        private SettingsGroupManager<IChartSettingsView>? groupManager; // Displays the currently selected settings group.

        private IChartSettings? chart; // Represents the current state of the chart.

        private IWorkspace? workspace; // The workspace that contains the chart.

        private string id; // The ID of the document that contains the chart.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartSettingsView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ChartSettingsViewPresenter(IChartSettingsView view, ICommandManager commands, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, settings, mapper, events)
        {
            View.MinimumSize = new Size(600, 150);

            View.Attach(this);

            id = string.Empty;

            if (log.IsDebugEnabled) log.Debug(string.Format(StringResources.InstanceCreated, nameof(ChartSettingsViewPresenter)));
        }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => Controllers.ChartSettingsController;

        /// <summary>
        /// Activates the view.
        /// </summary>
        public void Activate()
        {
            View.SelectNode(Constants.Chart);
        }

        /// <summary>
        /// Applies the preview settings to the chart view.
        /// </summary>
        /// <param name="chart">The <see cref="IChartSettings"/> that specifies the state of the chart.</param>
        public void ApplyPreviewSettings(IChartSettings chart)
        {
            this.chart = chart;

            if (ParentController.GetController(Controllers.ChartController) is IChartOutputPort outputPort)
            {
                var interactor = UseCaseFactory.CreateUpdateChartUseCase(outputPort);
                var dto = Mapper.Map<ChartDTO>(chart);
                interactor.Execute(dto);
            }
        }

        /// <summary>
        /// Applies the chart settings to the document.
        /// </summary>
        public void ApplySettings()
        {
            if (chart != null && AppController.GetController(Controllers.ApplicationViewController) is IApplicationOutputPort outputPort)
            {
                var interactor = UseCaseFactory.CreateUpdateDocumentUseCase(outputPort);
                var workspaceDTO = Mapper.Map<WorkspaceDTO>(workspace);
                var chartDto = Mapper.Map<ChartDTO>(chart);
                interactor.Execute(workspaceDTO, id, chartDto);
            }
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

                ParentController.AddToolbarButton(Constants.ShowSettings, StringResources.Settings, ImageResources.Settings, GetCommand(ParentController, Actions.ShowSplitContent, View.Name));

                var applyCommand = GetCommandChain();
                applyCommand.Add(GetCommand(this, Actions.ApplySettings));
                applyCommand.Add(GetCommand(ParentController, Actions.HideSplitContent, View.Name));
                View.AttachOKButtonCommand(applyCommand);

                var revertCommand = GetCommandChain();
                revertCommand.Add(GetCommand(this, Actions.RevertSettings));
                revertCommand.Add(GetCommand(ParentController, Actions.HideSplitContent, View.Name));
                View.AttachCancelButtonCommand(revertCommand);

                CreateSettingsGroups();

                View.Initialise(controller);
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
        /// Reverts the changes to the settings.
        /// </summary>
        public void RevertSettings()
        {
            if (ParentController.GetController(Controllers.ChartController) is IChartController controller)
            {
                controller.UpdateChart();
            }
        }

        /// <summary>
        /// Shows the settings for the specified settings group.
        /// </summary>
        /// <param name="group">The name of the settings group to show.</param>
        public void ShowSettingsGroup(string group)
        {
            Debug.Assert(groupManagers.ContainsKey(group));
            Debug.Assert(chart != null);

            View.Clear();

            groupManager = groupManagers[group];

            groupManager.ShowSettings(chart);
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
        /// Updates the chart settings.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that contains the chart.</param>
        public void UpdateSettings(IDocument document)
        {
            chart = new ChartSettings(document.Chart);

            id = document.ID;
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

        /// <summary>
        /// Adds the <see cref="SettingsGroupManager{TView}"/> to the dictionary.
        /// </summary>
        /// <param name="manager">The <see cref="SettingsGroupManager{TView}"/> to add.</param>
        private void AddGroupManager(SettingsGroupManager<IChartSettingsView> manager)
        {
            groupManagers.Add(manager.Group, manager);
        }
    }
}
