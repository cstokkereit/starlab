using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that specify the data sources providing the coordinates of the plot elements with respect to each axis.
    /// </summary>
    public partial class DataSourceSection : UserControl, ISettingsSection
    {
        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="DataSourceSection"> class.
        /// </summary>
        public DataSourceSection(IChartSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            string[] xData = [ "", "ColourIndex(U-B)", "ColourIndex(B-V)", "ColourIndex(V-I)", "Effective Temperature", "SpectralClass" ]; // TODO : This needs to be generated from the available data
            string[] yData = [ "", "AbsoluteMagnitude", "Luminosity" ]; // TODO : This needs to be generated from the available data

            comboX1Data.Items.AddRange(xData);
            comboX2Data.Items.AddRange(xData);
            comboY1Data.Items.AddRange(yData);
            comboY2Data.Items.AddRange(yData);

            comboX1Data.SelectedText = settings.Axes.X1.Data;
            comboX2Data.SelectedText = settings.Axes.X2.Data;
            comboY1Data.SelectedText = settings.Axes.Y1.Data;
            comboY2Data.SelectedText = settings.Axes.Y2.Data;

            AttachEventHandlers();
        }

        /// <summary>
        /// Attaches the event handlers for the child <see cref="Control"/>s that comprise this <see cref="UserControl"/>
        /// </summary>
        private void AttachEventHandlers()
        {
            comboX1Data.TextChanged += OnTextChanged;
            comboX2Data.TextChanged += OnTextChanged;
            comboY1Data.TextChanged += OnTextChanged;
            comboY2Data.TextChanged += OnTextChanged;
        }

        /// <summary>
        /// Event handler for the <see cref="ComboBox.TextChanged"> events.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnTextChanged(object? sender, EventArgs e)
        {
            ApplySettings(settings.Axes.X1, comboX1Data.Text);
            ApplySettings(settings.Axes.X2, comboX2Data.Text);
            ApplySettings(settings.Axes.Y1, comboY1Data.Text);
            ApplySettings(settings.Axes.Y2, comboY2Data.Text);

            SectionChanged?.Invoke(this, new EventArgs());
        }

        /// <summary>
        /// Configures the specified axis with the new settings.
        /// </summary>
        /// <param name="axis">The axis being configured.</param>
        /// <param name="data">The name of the data field that the axis represents.</param>
        private void ApplySettings(IAxisSettings axis, string data)
        {
            axis.Data = data;

            // TODO : Setting data to empty should clear the axis

            if (!string.IsNullOrEmpty(data) && string.IsNullOrEmpty(axis.Label.Text))
            {
                // TODO : Set the axis min and max based on the range of the chosen data
                axis.Label.Text = data;
                axis.Visible = true;
            }
        }
    }
}
