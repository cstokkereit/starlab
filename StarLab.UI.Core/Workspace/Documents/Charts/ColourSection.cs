using StarLab.Presentation;
using StarLab.Shared.Properties;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the foreground and background colour of chart elements.
    /// </summary>
    public partial class ColourSection : UserControl, ISettingsSection
    {
        private const int HEIGHT_WITHOUT_BACKGROUND = 45; // The height of the control when handling only the foreground colour.
        private const int HEIGHT_WITH_BACKGROUND = 98; // The height of the control handling the background and foreground colours.

        private const string BUTTON_BACKGROUND = "buttonBackground"; // The name of the custom background colour button.
        private const string BUTTON_FOREGROUND = "buttonForeground"; // The name of the custom foreground colour button.

        private const string COMBO_BACKGROUND = "comboBackground"; // The name of the background colour combo box.
        private const string COMBO_FOREGROUND = "comboForeground"; // The name of the foreground colour combo box.

        private readonly ChartSettings chart; // The chart settings being configured.

        private readonly bool hideBackGround; // A flag that determines whether the background colour options are displayed.

        private readonly string key; // A key that identifies the settings by this instance.

        private string customBackColour = string.Empty; // The custom background colour.

        private string customForeColour = string.Empty; // The custom foreground colour.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// 
        /// </summary>Initialises a new instance of the <see cref="ColourSection"/> class.
        /// <param name="chart">The chart settings being configured.</param>
        /// <param name="key">A key that identifies the managed settings.</param>
        /// <param name="hideBackGround">A flag that determines whether the background colour options are displayed.</param>
        public ColourSection(ChartSettings chart, string key, bool hideBackGround)
        {
            InitializeComponent();

            this.hideBackGround = hideBackGround;
            this.chart = chart;
            this.key = key;

            ConfigureSection();
        }

        /// <summary>
        /// Configures the <see cref="UserControl"/> to display the child controls used to set the values of the colour properties.
        /// </summary> 
        private void ConfigureSection()
        {
            // NOTE - The SelectedText property must be set before wiring up the DropDown event handler.

            var settings = chart.GetColourSettings(key);

            if (!hideBackGround)
            {
                customBackColour = settings.BackColour.StartsWith('#') ? settings.BackColour : string.Empty;

                comboBackground.SelectedText = GetColourName(settings.BackColour);
                comboBackground.TextChanged += OnColourChanged;
                comboBackground.DropDown += OnDropDown;

                labelForeground.Text = Resources.ForeColour;
                labelBackground.Text = Resources.BackColour;

                Height = HEIGHT_WITH_BACKGROUND;
            }
            else
            {
                labelForeground.Text = Resources.Colour;

                Height = HEIGHT_WITHOUT_BACKGROUND;
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
            var settings = chart.GetColourSettings(key);

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
                var settings = chart.GetColourSettings(key);

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
                var settings = chart.GetColourSettings(key);

                switch (combo.Name)
                {
                    case COMBO_BACKGROUND:
                        if (!string.IsNullOrEmpty(customBackColour)) combo.Items.Add(Resources.Custom);
                        break;

                    case COMBO_FOREGROUND:
                        if (!string.IsNullOrEmpty(customForeColour)) combo.Items.Add(Resources.Custom);
                        break;
                }

                foreach (KnownColor value in Enum.GetValues(typeof(KnownColor)))
                {
                    var colour = Color.FromKnownColor(value);

                    if (!colour.IsSystemColor && colour.Name != Constants.Transparent)
                    {
                        combo.Items.Add(colour);
                    }
                }
            }
        }
    }
}
