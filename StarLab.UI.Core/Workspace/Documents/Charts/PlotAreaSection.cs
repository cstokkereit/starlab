using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the plot area of a chart.
    /// </summary>
    public partial class PlotAreaSection : UserControl, ISettingsSection
    {
        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        public event EventHandler<IChartSettings>? SectionChanged;

        /// <summary>
        /// Initialises a new instance of the <see cref="PlotAreaSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        public PlotAreaSection(IChartSettings settings)
        {
            InitializeComponent();

            this.settings = settings;
        }

        /// <summary>
        /// Event handler for the <see cref="CheckBox.CheckStateChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnCheckStateChanged(object? sender, EventArgs e)
        {
            SectionChanged?.Invoke(this, settings);
        }

        // Overlays 
        //  Named stars
        //  Magnitude classes
        //  Variable types
        //  Radius
        //  Life cycle path

        // Background - Spectrum

        // Gridline opacity
    }
}
