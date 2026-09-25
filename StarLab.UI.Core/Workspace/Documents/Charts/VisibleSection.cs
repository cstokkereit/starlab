using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the visiblity of chart elements.
    /// </summary>
    public partial class VisibleSection : UserControl, ISettingsSection
    {
        private readonly IChartElementSettings settings; // The chart element settings that are bound to this control.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="VisibleSection"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        public VisibleSection(IChartElementSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            checkBoxVisible.Checked = settings.Visible;

            checkBoxVisible.CheckStateChanged += OnCheckStateChanged;
        }

        /// <summary>
        /// Event handler for the <see cref="CheckBox.CheckStateChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnCheckStateChanged(object? sender, EventArgs e)
        {
            settings.Visible = checkBoxVisible.Checked;

            SectionChanged?.Invoke(this, new EventArgs());
        }
    }
}
