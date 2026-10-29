using log4net;
using StarLab.Domain.Data;
using StarLab.Presentation.Workspace.Documents.Charts.Overlays;
using StarLab.Shared;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Controls the behaviour of a chart settings panel.
    /// </summary>
    internal class ChartSettingsViewPresenter : ChildViewPresenter<IChartSettingsView, IDocumentController>, IChildViewController, IChartSettingsViewPresenter, ISubscriber<ChartSettingsChangedEventArgs>, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IChartSettingsUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        private IChartSettings? chart; // Represents the current state of the chart.

        private readonly IDocument document; // The document that contains the chart.

        private string key = Constants.Chart; // The current settings key.

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

            ParentController.AddToolbarButton(Constants.ShowSettings, StringResources.Settings, ImageResources.Settings, CreateCommand(Actions.ShowSettings, () => ParentController.ShowSplitContent(View.Name)));

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

            CreateSettingsNavigator();

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

                View.UpdateSettings(chart);

                //ShowSettings(key);
            }
        }

        /// <summary>
        /// Event handler for the WorkspaceChanged event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            workspace = args.Workspace;

            View.SelectNode(key);
        }

        /// <summary>
        /// Runs the child view.
        /// </summary>
        public override void Run()
        {
            CreateOverlayNodes();

            ShowSettings(key);

            View.ExpandNode(key);
        }

        /// <summary>
        /// Shows the settings for the specified key.
        /// </summary>
        /// <param name="key">The settings key.</param>
        public void ShowSettings(string key)
        {
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.ObjectNotInitialised(GetType()));

            this.key = key;

            View.Clear();

            switch (key)
            {
                case Constants.Chart:
                case Constants.ChartPlotArea:
                    View.AppendColourSection(key, false);
                    break;

                case Constants.Data:
                    View.AppendDataSourceSection(Constants.ChartAxes, GetAvailableHorizontalAxisParameters(), GetAvailableVerticalAxisParameters());
                    break;

                default:
                    AppendSettings(chart.GetSettings(key));
                    break;
            }
        }

        /// <summary>
        /// Updates the chart preview following a change to the chart settings.
        /// </summary>
        /// <param name="chart">The <see cref="IChartSettings"/> that specifies the state of the chart.</param>
        public void UpdateChartPreview()
        {
            if (document == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);
            if (chart == null) throw new InvalidOperationException(ExceptionMessages.InvalidState);

            useCaseService.UpdateChartPreview(document.ID, chart);
        }

        /// <summary>
        /// Releases any resources used by the <see cref="ChartSettingsViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; <c>false</c>.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                View.Detach();
            }
        }

        /// <summary>
        /// Appends the sections required to configure the settings for a chart element.
        /// </summary>
        /// <param name="settings">An <see cref="IChartElementSettings"/> that specifies the chart element settings.</param>
        private void AppendSettings(IChartElementSettings settings)
        {
            if (settings is ILabelSettings label)
            {
                View.AppendTextSection(key);
                View.AppendFontSection(key, false);
            }

            if (settings is IOverlaySettings || settings is IOverlayGroupSettings || settings is ITickLabelSettings)
            {
                View.AppendFontSection(key, true);
            }

            View.AppendColourSection(key, true);

            if (settings is IPointSettings points)
            {
                View.AppendSizeSection(key);
            }

            if (settings is IScaleSettings scale)
            {
                View.AppendScaleSection(key);
            }

            View.AppendVisibleSection(key);
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
        /// Creates the nodes that provide access to the chart overlay settings.
        /// </summary>
        private void CreateOverlayNodes()
        {
            View.ClearOverlays();

            if (chart != null && chart.Overlays.Count > 0)
            {
                var overlays = View.AddNode(Constants.Overlays, Constants.Chart, StringResources.Overlays);

                foreach (var overlay in chart.Overlays)
                {
                    View.AddNode(overlay.Name, overlays, GetCaption(overlay.Name));
                }
            }
        }

        /// <summary>
        /// Adds the nodes that will be used to navigate to the sections containing the configuration settings for the individual chart elements.
        /// </summary>
        private void CreateSettingsNavigator()
        {
            View.AddNode(Constants.Chart, StringResources.Chart);

            View.AddNode(Constants.Data, StringResources.Data);

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
        }

        /// <summary>
        /// Generates a list containing the stellar parameters that can be represented by the horizontal axes.
        /// </summary>
        /// <returns>A list containing the names of the available stellar parameters.</returns>
        private List<IDisplayField> GetAvailableHorizontalAxisParameters()
        {
            // TODO : These should be checked against the available data

            var parameters = new List<IDisplayField>()
            {
                new DisplayField(StellarParameters.EffectiveTemperature, StringResources.EffectiveTemperature),
                new DisplayField(StellarParameters.ColourIndex("B", "V"), string.Format(StringResources.ColourIndex, "B", "V")),
                new DisplayField(StellarParameters.SpectralClass, StringResources.SpectralClass)
            };

            return parameters;
        }

        /// <summary>
        /// Generates a list containing the stellar parameters that can be represented by the vertical axes.
        /// </summary>
        /// <returns>A list containing the names of the available stellar parameters.</returns>
        private List<IDisplayField> GetAvailableVerticalAxisParameters()
        {
            // TODO : These should be checked against the available data

            var parameters = new List<IDisplayField>()
            {
                new DisplayField(StellarParameters.AbsoluteMagnitude, StringResources.AbsoluteMagnitude),
                new DisplayField(StellarParameters.Luminosity, StringResources.Luminosity)
            };

            return parameters;
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
