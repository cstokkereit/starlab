using log4net;
using StarLab.Presentation.Configuration;
using StarLab.Shared;
using Stratosoft.Commands;

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

        private readonly IChartSettingsUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        private IChartSettings? chart; // Represents the current state of the chart.

        private IDocument? document; // The document that contains the chart.

        private IWorkspace? workspace; // The workspace that contains the document.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartSettingsView"/> controlled by this presenter.</param>
        /// <param name="document">The <see cref="IDocument"/> that the presenter .</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public ChartSettingsViewPresenter(IChartSettingsView view, IDocument document, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            useCaseService = services.GetService<IChartSettingsUseCaseService>();

            this.document = document ?? throw new ArgumentNullException(nameof(document));

            View.MinimumSize = new Size(600, 150);

            View.Attach(this);
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
        public void ApplyPreviewSettings()
        {
            if (document == null) throw new InvalidOperationException(ExceptionMessages.InvalidState); 
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);

            useCaseService.UpdateChart(document.ID, chart);
        }

        /// <summary>
        /// Applies the chart settings to the document.
        /// </summary>
        public void ApplySettings()
        {
            if (workspace == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);
            if (document == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);

            useCaseService.UpdateDocument(workspace, document.ID, chart);
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

            ParentController.AddToolbarButton(Constants.ShowSettings, StringResources.Settings, ImageResources.Settings, CreateCommand(Actions.ShowSplitContent, () => ParentController.ShowSplitContent(View.Name)));

            View.AttachOKButtonCommand(CreateCommand(Actions.ApplySettings, () => {
                ParentController.HideSplitContent(View.Name);
                ApplySettings();
            }));

            View.AttachCancelButtonCommand(CreateCommand(Actions.RevertSettings, () => {
                ParentController.HideSplitContent(View.Name);
                RevertSettings();
            }));

            View.Initialise();

            CreateSettingsNavigator();

            log.Debug(LogEntries.PresenterInitialised(GetType()));
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            View.SelectNode(Constants.Chart);

            workspace = args.Workspace;
        }

        /// <summary>
        /// Reverts the changes to the settings.
        /// </summary>
        public void RevertSettings()
        {
            var controller = ParentController.GetController<IChartController>(new ControllerID(Constants.Chart));

            controller.UpdatePreview();
        }

        /// <summary>
        /// Runs the child view.
        /// </summary>
        public override void Run()
        {
            var controller = ParentController.GetController<IChartController>(new ControllerID(Constants.Chart));

            if (controller.Chart != null)
            {
                chart = new ChartSettings(controller.Chart);
            }

            View.ExpandNode(Constants.Chart);
        }

        /// <summary>
        /// Shows the settings for the specified key.
        /// </summary>
        /// <param name="key">The settings key.</param>
        public void ShowSettings(string key)
        {
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);

            View.Clear();

            switch (key)
            {
                case Constants.Chart:
                    View.AppendColourSection(chart);
                    break;

                case Constants.ChartPlotArea:
                    View.AppendColourSection(chart.PlotArea);
                    break;

                default:
                    AppendSettings(chart.GetSettings(key));
                    break;
            }
        }

        /// <summary>
        /// Releases any resources used by the <see cref="ChartSettingsViewPresenter"/> object.
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

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(IAxesSettings settings)
        {
            View.AppendColourSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(IChartElementSettings settings)
        {
            // TODO - Replace with commands or lambdas?

            if (settings is IAxesSettings axes)
            {
                AppendSettings(axes);
            }
            if (settings is IAxisSettings axis)
            {
                AppendSettings(axis);
            }
            else if (settings is IGridSettings grid)
            {
                AppendSettings(grid);
            }
            else if (settings is IGridLineSettings gridLines)
            {
                AppendSettings(gridLines);
            }
            else if (settings is ILabelSettings label)
            {
                AppendSettings(label);
            }
            else if (settings is IOverlaySettings overlays)
            {
                AppendSettings(overlays);
            }
            else if (settings is IPointSettings points)
            {
                AppendSettings(points);
            }
            else if (settings is IScaleSettings scale)
            {
                AppendSettings(scale);
            }
            else if (settings is ITickLabelSettings tickLabels)
            {
                AppendSettings(tickLabels);
            }
            else if (settings is ITickMarkSettings tickMarks)
            {
                AppendSettings(tickMarks);
            }
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(IAxisSettings settings)
        {
            View.AppendColourSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(IGridSettings settings)
        {
            View.AppendColourSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(IGridLineSettings settings)
        {
            View.AppendColourSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// Appends the sections required to configure the settings for a label.
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(ILabelSettings settings)
        {
            View.AppendTextSection(settings);
            View.AppendFontSection(settings);
            View.AppendColourSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(IPointSettings settings)
        {
            View.AppendColourSection(settings);
            View.AppendSizeSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(IScaleSettings settings)
        {
            View.AppendColourSection(settings);
            View.AppendScaleSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(ITickLabelSettings settings)
        {
            View.AppendFontSection(settings);
            View.AppendColourSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="settings"></param>
        private void AppendSettings(ITickMarkSettings settings)
        {
            View.AppendColourSection(settings);
            View.AppendVisibleSection(settings);
        }

        /// <summary>
        /// Creates the nodes that provide access to the axis settings.
        /// </summary>
        /// <param name="name">The node name.</param>
        /// <param name="parent">The parent node key.</param>
        /// <param name="text">The node text.</param>
        private void CreateAxisNodes(string name, string parent, string text)
        {
            var axis = View.AddNode(name, parent, text);

            View.AddNode(Constants.Label, axis, StringResources.Label);

            var scale = View.AddNode(Constants.Scale, axis, StringResources.Scale);

            View.AddNode(Constants.MinorTickMarks, scale, StringResources.MinorTickMarks);
            View.AddNode(Constants.MajorTickMarks, scale, StringResources.MajorTickMarks);
            View.AddNode(Constants.TickLabels, scale, StringResources.TickLabels);
        }

        /// <summary>
        /// 
        /// </summary>
        private void CreateSettingsNavigator()
        {
            View.AddNode(Constants.Chart, StringResources.Chart);

            View.AddNode(Constants.Title, Constants.Chart, StringResources.Title);

            var axes = View.AddNode(Constants.Axes, Constants.Chart, StringResources.Axes);

            CreateAxisNodes(Constants.AxisX1, axes, StringResources.AxisX1);
            CreateAxisNodes(Constants.AxisX2, axes, StringResources.AxisX2);
            CreateAxisNodes(Constants.AxisY1, axes, StringResources.AxisY1);
            CreateAxisNodes(Constants.AxisY2, axes, StringResources.AxisY2);

            var plotArea = View.AddNode(Constants.PlotArea, Constants.Chart, StringResources.PlotArea);

            var grid = View.AddNode(Constants.Grid, plotArea, StringResources.Grid);

            View.AddNode(Constants.MinorGridLines, grid, StringResources.MinorGridLines);
            View.AddNode(Constants.MajorGridLines, grid, StringResources.MajorGridLines);

            View.AddNode(Constants.Points, plotArea, StringResources.Points);

            var overlays = View.AddNode(Constants.Overlays, Constants.Chart, StringResources.Overlays);
        }
    }
}
