namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the visiblity of chart elements.
    /// </summary>
    public partial class VisibleSection : UserControl, ISettingsSection
    {
        private readonly ChartSettings chart; // The chart settings being configured.

        private readonly string key; // A key that identifies the settings by this instance.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="VisibleSection"/> class.
        /// </summary>
        /// <param name="chart">The chart settings being configured.</param>
        /// <param name="key">A key that identifies the settings by this instance.</param>
        public VisibleSection(ChartSettings chart, string key)
        {
            InitializeComponent();

            this.chart = chart;
            this.key = key;

            var element = chart.GetSettings(key);

            checkBoxVisible.Checked = element.Visible;

            checkBoxVisible.CheckStateChanged += OnCheckStateChanged;
        }

        /// <summary>
        /// Event handler for the <see cref="CheckBox.CheckStateChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnCheckStateChanged(object? sender, EventArgs e)
        {
            var element = chart.GetSettings(key);

            element.Visible = checkBoxVisible.Checked;

            SectionChanged?.Invoke(this, new EventArgs());
        }
    }
}
