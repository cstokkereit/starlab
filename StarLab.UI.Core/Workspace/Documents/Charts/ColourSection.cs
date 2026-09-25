using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared.Properties;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the foreground and background colour of chart elements.
    /// </summary>
    public partial class ColourSection : UserControl, ISettingsSection
    {
        private const string BUTTON_BACKGROUND = "buttonBackground"; // The name of the custom background colour button.
        private const string BUTTON_FOREGROUND = "buttonForeground"; // The name of the custom foreground colour button.

        private const string COMBO_BACKGROUND = "comboBackground"; // The name of the background colour combo box.
        private const string COMBO_FOREGROUND = "comboForeground"; // The name of the foreground colour combo box.

        private readonly IColourSettings settings; // The colour settings that are bound to this control.

        private string customBackColour; // The custom background colour.

        private string customForeColour; // The custom foreground colour.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartElementSettings"/> that are bound to this control.</param>
        public ColourSection(IChartElementSettings settings)
            : this (new ChartElementSettingsAdapter(settings)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        public ColourSection(IChartSettings settings)
            : this (new ChartSettingsAdapter(settings)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IPlotAreaSettings"/> that are bound to this control.</param>
        public ColourSection(IPlotAreaSettings settings)
            : this(new PlotAreaSettingsAdapter(settings)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IColourSettings"/> that are bound to this control.</param>
        private ColourSection(IColourSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            ConfigureSection();
        }

        /// <summary>
        /// TODO
        /// </summary>
        private void ConfigureSection()
        {
            // The SelectedText property must be set before wiring up the DropDown event handler.

            if (!settings.ForegroundOnly)
            {
                customBackColour = settings.BackColour.StartsWith('#') ? settings.BackColour : string.Empty;

                comboBackground.SelectedText = GetColourName(settings.BackColour);
                comboBackground.TextChanged += OnColourChanged;
                comboBackground.DropDown += OnDropDown;

                labelForeground.Text = Resources.ForeColour;
                labelBackground.Text = Resources.BackColour;

                Height = 100;
            }
            else
            {
                labelForeground.Text = Resources.Colour;

                Height = 50;
            }

            customForeColour = settings.ForeColour.StartsWith('#') ? settings.ForeColour : string.Empty;

            comboForeground.SelectedText = GetColourName(settings.ForeColour);
            comboForeground.TextChanged += OnColourChanged;
            comboForeground.DropDown += OnDropDown;
        }

        /// <summary>
        /// Gets the name of the colour. Returns <<see cref="Resources.Custom"/>> for custom colours.
        /// </summary>
        /// <param name="colour">The name of the colour or its ARBG value prefixed by "#" for custom colours.</param>
        /// <returns>The name of the colour.</returns>
        private string GetColourName(string colour)
        {
            return colour.StartsWith('#') ? Resources.Custom : colour;
        }

        /// <summary>
        /// Event handler for the <see cref="Button.Click"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnClick(object? sender, EventArgs e)
        {
            if (dialogCustomColour.ShowDialog() == DialogResult.OK && sender is Button button)
            {
                // TODO - Will need to maintain a list of custom colours and use them to populate the dialog, save to settings etc.

                var colour = $"#{dialogCustomColour.Color.ToArgb()}";

                switch (button.Name)
                {
                    case BUTTON_BACKGROUND:
                        settings.BackColour = colour;
                        comboBackground.SelectAll();
                        comboBackground.SelectedText = GetColourName(settings.BackColour);
                        break;

                    case BUTTON_FOREGROUND:
                        settings.ForeColour = colour;
                        comboForeground.SelectAll();
                        comboForeground.SelectedText = GetColourName(settings.ForeColour);
                        break;
                }
            }
        }

        /// <summary>
        /// Event handler for the <see cref="ComboBox.TextChanged"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnColourChanged(object? sender, EventArgs e)
        {
            if (sender is ComboBox combo)
            {
                switch (combo.Name)
                {
                    case COMBO_BACKGROUND:
                        settings.BackColour = (combo.Text == Resources.Custom && !string.IsNullOrEmpty(customBackColour)) ? customBackColour : combo.Text;
                        break;

                    case COMBO_FOREGROUND:
                        settings.ForeColour = (combo.Text == Resources.Custom && !string.IsNullOrEmpty(customForeColour)) ? customForeColour : combo.Text;
                        break;
                }

                SectionChanged?.Invoke(this, new EventArgs());
            }
        }

        /// <summary>
        /// Event handler for the <see cref="ComboBox.DropDown"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnDropDown(object? sender, EventArgs e)
        {
            if (sender is ComboBox combo && combo.Items.Count == 0)
            {
                switch (combo.Name)
                {
                    case COMBO_BACKGROUND:
                        if (!string.IsNullOrEmpty(customBackColour)) combo.Items.Add(Resources.Custom);
                        break;

                    case COMBO_FOREGROUND:
                        if (!string.IsNullOrEmpty(customForeColour)) combo.Items.Add(Resources.Custom);
                        break;
                }

                var converter = new ColorConverter();

                var colours = converter.GetStandardValues();

                if (colours != null)
                {
                    foreach (var colour in colours)
                    {
                        combo.Items.Add(colour);
                    }
                }
            }
        }

        /// <summary>
        /// Represents the available colour settings.
        /// </summary>
        private interface IColourSettings
        {
            /// <summary>
            /// Gets or sets the background colour.
            /// </summary>
            string BackColour { get; set; }

            /// <summary>
            /// Gets or sets the foreground colour.
            /// </summary>
            string ForeColour { get; set; }

            /// <summary>
            /// A flag indicating that only the foreground colour can be modified.
            /// </summary>
            bool ForegroundOnly { get; }
        }

        /// <summary>
        /// The available colour settings for a chart element.
        /// </summary>
        private class ChartElementSettingsAdapter : IColourSettings
        {
            private readonly IChartElementSettings settings;

            public ChartElementSettingsAdapter(IChartElementSettings settings) { this.settings = settings; }

            public string BackColour { get => throw new NotSupportedException(); set => throw new NotSupportedException(); }

            public string ForeColour { get => settings.Colour; set { settings.Colour = value; } }

            public bool ForegroundOnly => true;
        }

        /// <summary>
        /// The available colour settings for the chart.
        /// </summary>
        private class ChartSettingsAdapter : IColourSettings
        {
            private readonly IChartSettings settings;

            public ChartSettingsAdapter(IChartSettings settings) { this.settings = settings; }

            public string BackColour { get => settings.BackColour; set { settings.BackColour = value; } }

            public string ForeColour { get => settings.ForeColour; set { settings.ForeColour = value; } }

            public bool ForegroundOnly => false;
        }

        /// <summary>
        /// The available colour settings for the plot area.
        /// </summary>
        private class PlotAreaSettingsAdapter : IColourSettings
        {
            private readonly IPlotAreaSettings settings;

            public PlotAreaSettingsAdapter(IPlotAreaSettings settings) { this.settings = settings; }

            public string BackColour { get => settings.BackColour; set { settings.BackColour = value; } }

            public string ForeColour { get => settings.ForeColour; set { settings.ForeColour = value; } }

            public bool ForegroundOnly => false;
        }
    }
}
