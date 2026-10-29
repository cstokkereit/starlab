using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the settings panel used to configure a chart.
    /// </summary>
    public partial class ChartSettingsView : ChildView, IChartSettingsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsView)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, TreeNode> nodes = []; // A dictionary containing the tree nodes indexed by node key.

        private readonly List<ISettingsSection> sections = []; // A list containing the settings sections applicable to the current settings group.

        private readonly ChartSettings chart = new ChartSettings();

        private IChartSettingsViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsView"> class.
        /// </summary>
        public ChartSettingsView()
            : base(ViewIDs.ChartSettings, SplitViewPanels.Panel1)
        {
            InitializeComponent();

            Name = ViewNames.ChartSettings;
        }

        /// <summary>
        /// Adds a node to the tree view that displays the chart property groups.
        /// </summary>
        /// <param name="name">The node name.</param>
        /// <param name="parentKey">The parent node key.</param>
        /// <param name="text">The node text.</param>
        /// <returns>The path to the new node.</returns>
        public string AddNode(string name, string parentKey, string text)
        {
            var parent = nodes[parentKey];

            if (parent != null)
            {
                var key = $"{parentKey}/{name}";
                var node = parent.Nodes.Add(key, text);
                nodes.Add(key, node);
                return node.Name;
            }

            throw new ArgumentOutOfRangeException(nameof(parentKey), parentKey);
        }

        /// <summary>
        /// Adds a node to the tree view that displays the chart property groups.
        /// </summary>
        /// <param name="name">The node name.</param>
        /// <param name="text">The node text.</param>
        /// <returns>The path to the new node.</returns>
        public string AddNode(string name, string text)
        {
            var node = treeView.Nodes.Add(name, text);
            nodes.Add(name, node);
            return node.Name;
        }

        /// <summary>
        /// Appends a colour settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        /// <param name="hideBackGround">A flag that controls whether the background colour options are displayed.</param>
        public void AppendColourSection(string key, bool hideBackGround)
        {
            var section = new ColourSection(chart, key, hideBackGround);
            section.SectionChanged += OnSettingsChanged;
            AppendSection(section);
        }

        /// <summary>
        /// Appends a data source settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        /// <param name="horizontalAxisFields">The fields that can be represented on a horizontal axis.</param>
        /// <param name="verticalAxisFields">The fields that can be represented on a vertical axis.</param>
        public void AppendDataSourceSection(string key, IEnumerable<IDisplayField> horizontalAxisFields, IEnumerable<IDisplayField> verticalAxisFields)
        {
            var section = new DataSourceSection(chart, key, horizontalAxisFields, verticalAxisFields);
            section.SectionChanged += OnSettingsChanged;
            AppendSection(section);
        }

        /// <summary>
        /// Appends a font settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        /// <param name="hideStyles">A flag that controls whether the font styles are displayed.</param>
        public void AppendFontSection(string key, bool hideStyles)
        {
            var section = new FontSection(chart, key, hideStyles);
            section.SectionChanged += OnSettingsChanged;
            AppendSection(section);
        }

        /// <summary>
        /// Appends a scale settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        public void AppendScaleSection(string key)
        {
            var section = new ScaleSection(chart, key);
            section.SectionChanged += OnSettingsChanged;
            AppendSection(section);
        }

        /// <summary>
        /// Appends a size settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        public void AppendSizeSection(string key)
        {
            var section = new NumericSection(chart, key);
            section.SectionChanged += OnSettingsChanged;
            AppendSection(section);
        }

        /// <summary>
        /// Appends a text settings section to the settings panel.
        /// </summary>
        /// <param name="key">A key that identifies the settings that are manged by the new settings section.</param>
        public void AppendTextSection(string key)
        {
            var section = new LabelSection(chart, key);
            section.SectionChanged += OnSettingsChanged;
            AppendSection(section);
        }

        /// <summary>
        /// Appends a visibility settings section to the settings panel.
        /// </summary>
        public void AppendVisibleSection(string key)
        {
            var section = new VisibleSection(chart, key);
            section.SectionChanged += OnSettingsChanged;
            AppendSection(section);
        }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public override void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(ExceptionMessages.PresenterAlreadyAttached);

            this.presenter = (IChartSettingsViewPresenter)presenter;

            log.Debug(LogEntries.PresenterAttached(presenter.GetType()));
        }

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the Cancel button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the Cancel button is clicked.</param>
        public void AttachCancelButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonCancel);
            }
        }

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the OK button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the OK button is clicked.</param>
        public void AttachOKButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonOK);
            }
        }

        /// <summary>
        /// Clears the settings panel.
        /// </summary>
        public void Clear()
        {
            foreach (var section in sections)
            {
                section.SectionChanged -= OnSettingsChanged;
            }

            panelSettings.Controls.Clear();

            sections.Clear();
        }

        /// <summary>
        /// Clears the overlays.
        /// </summary>
        public void ClearOverlays()
        {
            foreach (var key in nodes.Keys)
            {
                if (key.Contains(Constants.Overlays))
                {
                    treeView.Nodes.Remove(nodes[key]);
                    nodes.Remove(key);
                }
            }
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public override void Detach()
        {
            if (presenter != null)
            {
                var type = presenter.GetType();

                presenter = null;

                log.Debug(LogEntries.PresenterDetached(type));
            }
        }

        /// <summary>
        /// Expands the specified tree view node.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void ExpandNode(string key)
        {
            nodes[key].Expand();
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        public override void Initialise()
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                node.Expand();
            }
        }

        /// <summary>
        /// Selects the specified tree view node.
        /// </summary>
        /// <param name="key">The node key.</param>
        public void SelectNode(string key)
        {
            if (nodes.ContainsKey(key)) treeView.SelectedNode = nodes[key];
        }

        /// <summary>
        /// Updates the chart settings following a change.
        /// </summary>
        /// <param name="chart">The new chart settings.</param>
        public void UpdateSettings(IChartSettings chart)
        {
            this.chart.Update(chart);
        }

        /// <summary>
        /// Appends the <see cref="ISettingsSection"/> to the settings panel.
        /// </summary>
        /// <param name="section">The <see cref="ISettingsSection"/> to append.</param>
        private void AppendSection(ISettingsSection section)
        {
            var top = 0;

            foreach (Control control in panelSettings.Controls)
            {
                top += SECTION_SPACING + control.Height;
            }

            sections.Add(section);

            panelSettings.Controls.Add((Control)section);

            section.Top = top;
        }

        /// <summary>
        /// Event handler for the <see cref="ISettingsSection.SettingsChanged"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnSettingsChanged(object? sender, EventArgs e)
        {
            presenter?.UpdateChartPreview();
        }

        /// <summary>
        /// Event handler for the <see cref="TreeView.AfterSelect"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e != null && e.Node != null)
            {
                presenter?.ShowSettings(e.Node.Name);
            }
        }
    }
}
