using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that specify the data sources providing the coordinates of the plot elements with respect to each axis.
    /// </summary>
    public partial class DataSourceSection : UserControl, ISettingsSection
    {
        private readonly ChartSettings chart; // The chart settings being configured.

        private readonly string key; // A key that identifies the settings by this instance.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="DataSourceSection"> class.
        /// </summary>
        /// <param name="chart">The chart settings being configured.</param>
        /// <param name="key">A key that identifies the settings by this instance.</param> 
        /// <param name="horizontalAxisFields">The fields that can be represented on a horizontal axis.</param>
        /// <param name="verticalAxisFields">The fields that can be represented on a vertical axis.</param>
        public DataSourceSection(ChartSettings chart, string key, IEnumerable<IDisplayField> horizontalAxisFields, IEnumerable<IDisplayField> verticalAxisFields)
        {
            InitializeComponent();

            this.chart = chart;
            this.key = key;

            var axes = (IAxesSettings)chart.GetSettings(key);

            foreach (var field in horizontalAxisFields)
            {
                comboX1Data.Items.Add(new ComboBoxItem(field));
                comboX2Data.Items.Add(new ComboBoxItem(field));

                if (axes.X1.Data == field.Name) comboX1Data.SelectedText = field.Caption;
                if (axes.X2.Data == field.Name) comboX2Data.SelectedText = field.Caption;
            }

            foreach (var field in verticalAxisFields)
            {
                comboY1Data.Items.Add(new ComboBoxItem(field));
                comboY2Data.Items.Add(new ComboBoxItem(field));

                if (axes.Y1.Data == field.Name) comboY1Data.SelectedText = field.Caption;
                if (axes.Y2.Data == field.Name) comboY2Data.SelectedText = field.Caption;
            }

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
            var axes = (IAxesSettings)chart.GetSettings(key);

            ApplySettings(axes.X1, comboX1Data.Text);
            ApplySettings(axes.X2, comboX2Data.Text);
            ApplySettings(axes.Y1, comboY1Data.Text);
            ApplySettings(axes.Y2, comboY2Data.Text);

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

        /// <summary>
        /// A custom combo box item.
        /// </summary>
        private class ComboBoxItem
        {
            private readonly IDisplayField field;

            public ComboBoxItem(IDisplayField field)
            {
                this.field = field;
            }

            public string Name => @field.Name;

            public override string ToString()
            {
                return @field.Caption;
            }
        }
    }
}
