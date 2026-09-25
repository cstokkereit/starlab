using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the data points of a chart.
    /// </summary>
    public partial class NumericSection : UserControl, ISettingsSection
    {
        private readonly IPointSettings settings; // The chart settings that are bound to this control.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="NumericSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IPointSettings"/> that are bound to this control.</param>
        public NumericSection(IPointSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

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

            if (int.TryParse(textSize.Text, out int size)) settings.Size = size;

            SectionChanged?.Invoke(this, new EventArgs());
        }
    }
}
