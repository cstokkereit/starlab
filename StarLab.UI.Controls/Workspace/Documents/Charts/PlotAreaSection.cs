using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the plot area of a chart.
    /// </summary>
    public partial class PlotAreaSection : UserControl, ISettingsSection
    {
        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        public event EventHandler<IChartSettings>? SectionChanged;

        public PlotAreaSection(IChartSettings settings, string group)
        {
            InitializeComponent();

            this.group = group;
            this.settings = settings;
        }

        /// <summary>
        /// Gets the <see cref="IChartElementSettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="IChartElementSettings"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IChartElementSettings GetSettings()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Event handler for the <see cref="CheckBox.CheckStateChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnCheckStateChanged(object? sender, EventArgs e)
        {
            //GetSettings().Visible = checkBoxVisible.Checked;

            SectionChanged?.Invoke(this, settings);
        }
    }
}
