using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the data points of a chart.
    /// </summary>
    public partial class NumericSection : UserControl, ISettingsSection
    {
        private readonly ChartSettings chart; // The chart settings being configured.

        private readonly string key; // A key that identifies the settings by this instance.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="NumericSection"> class.
        /// </summary>
        /// <param name="chart">The chart settings being configured.</param>
        /// <param name="key">A key that identifies the settings by this instance.</param>
        public NumericSection(ChartSettings chart, string key)
        {
            InitializeComponent();

            this.chart = chart;
            this.key = key;

            var settings = (IPointSettings)chart.GetSettings(key); // TODO - Switch to using an adapter if more than one of this type of field

            var size = settings.Size;

            textSize.Text = size.ToString();
            trackBarSize.Value = size;

            trackBarSize.ValueChanged += OnSizeChanged;
        }

        /// <summary>
        /// Event handler for the <see cref="TrackBar.ValueChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnSizeChanged(object? sender, EventArgs e)
        {
            textSize.Text = trackBarSize.Value.ToString();

            var settings = (IPointSettings)chart.GetSettings(key);

            if (int.TryParse(textSize.Text, out int size)) settings.Size = size;

            SectionChanged?.Invoke(this, new EventArgs());
        }
    }
}
