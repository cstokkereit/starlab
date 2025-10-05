using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared.Properties;
using StarLab.UI.Controls.Workspace.Documents.Charts;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the settings panel used to configure a chart.
    /// </summary>
    public partial class ChartSettingsView : UserControl, IChartSettingsView
    {
        private const int SECTION_MARGIN = 15; // 

        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsView)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, TreeNode> nodes = new Dictionary<string, TreeNode>(); // A dictionary containing the tree nodes indexed by node key.

        private readonly List<ISettingsSection> sections = new List<ISettingsSection>(); // A list containing the settings sections applicable to the current settings group.

        private IChartSettingsViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsView"> class.
        /// </summary>
        public ChartSettingsView()
        {
            InitializeComponent();

            Name = Views.ChartSettings;

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(ChartSettingsView)));
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"> that controls this view.
        /// </summary>
        public IChildViewController? Controller => (IChildViewController?)presenter;

        /// <summary>
        /// Gets the panel that will contain the view.
        /// </summary>
        public SplitViewPanels Panel => SplitViewPanels.Panel1;

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
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        public void AppendColourSection(IChartSettings settings, string group)
        {
            var section = new ColourSection(settings, group);

            section.SectionChanged += Section_SettingsChanged;

            AppendSection(section);
        }

        /// <summary>
        /// Appends a font settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        public void AppendFontSection(IChartSettings settings, string group)
        {
            var section = new FontSection(settings, group);

            section.SectionChanged += Section_SettingsChanged;

            AppendSection(section);
        }

        /// <summary>
        /// Appends a text settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        public void AppendTextSection(IChartSettings settings, string group)
        {
            var section = new TextSection(settings, group);

            section.SectionChanged += Section_SettingsChanged;

            AppendSection(section);
        }

        /// <summary>
        /// Appends a visibility settings section to the settings panel.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        /// <param name="group">The name of the settings group.</param>
        public void AppendVisibleSection(IChartSettings settings, string group)
        {
            var section = new VisibleSection(settings, group);

            section.SectionChanged += Section_SettingsChanged;

            AppendSection(section);
        }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(); // TODO

            this.presenter = (IChartSettingsViewPresenter)presenter;
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
                section.SectionChanged -= Section_SettingsChanged;
            }

            panelSettings.Controls.Clear();

            sections.Clear();
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            foreach (TreeNode node in treeView.Nodes)
            {
                node.Expand();
            }
        }

        /// <summary>
        /// Sets the minimum size for the control.
        /// </summary>
        /// <param name="size">A <see cref="Size"/> that specifies the minimum height and width.</param>
        public void SetMinimumSize(Size size)
        {
            MinimumSize = size;
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
                top += SECTION_MARGIN + control.Height;
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
        private void Section_SettingsChanged(object? sender, IChartSettings e)
        {
            presenter.ApplyPreviewSettings(e);
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
                presenter.ShowSettingsGroup(e.Node.Name);
            }
        }
    }
}
