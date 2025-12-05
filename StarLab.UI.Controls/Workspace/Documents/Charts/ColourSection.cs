using StarLab.Presentation;
using StarLab.Shared.Properties;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the foreground and background colour of chart elements.
    /// </summary>
    public partial class ColourSection : UserControl, ISettingsSection
    {
        private const string BUTTON_BACKGROUND = "buttonBackground";
        private const string BUTTON_FOREGROUND = "buttonForeground";

        private const string COMBO_BACKGROUND = "comboBackground";
        private const string COMBO_FOREGROUND = "comboForeground";

        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        private string customBackColour; // The custom background colour.

        private string customForeColour; // The custom foreground colour.

        public event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ColourSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        /// <param name="group">The name of the settings group that this control represents.</param>
        public ColourSection(IChartSettings settings, string group, bool dualSelection)
        {
            InitializeComponent();

            if (dualSelection)
            {
                labelBackground.Text = Resources.BackColour;
                labelForeground.Text = Resources.ForeColour;
                Height = 100;
            }
            else
            {
                labelForeground.Text = Resources.Colour;
                Height = 43;
            }

            customBackColour = settings.BackColour.StartsWith('#') ? settings.BackColour : string.Empty;
            customForeColour = settings.ForeColour.StartsWith('#') ? settings.ForeColour : string.Empty;

            this.settings = settings;
            this.group = group;

            InitialiseComboBoxes();
        }

        /// <summary>
        /// Populates the <see cref="ComboBox"/> controls and wires up their event handlers.
        /// </summary>
        private void InitialiseComboBoxes()
        {
            var settings = GetSettings();

            // NOTE - The SelectedText property must be set before wiring up the DropDown event handler.

            comboForeground.SelectedText = GetColourName(settings.ForeColour);
            comboForeground.TextChanged += OnColourChanged;
            comboForeground.DropDown += OnDropDown;

            comboBackground.SelectedText = GetColourName(settings.BackColour);
            comboBackground.TextChanged += OnColourChanged;
            comboBackground.DropDown += OnDropDown;
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
        /// Gets the <see cref="IColourSettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="IColourSettings"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IColourSettings GetSettings()
        {
            IColourSettings? settings = null;

            switch (group)
            {
                case Constants.ChartAxes:
                    settings = this.settings.Axes;
                    break;

                case Constants.Chart:
                    settings = this.settings;
                    break;

                case Constants.ChartTitle:
                    settings = this.settings.Title;
                    break;

                case Constants.ChartAxisX1:
                    settings = this.settings.Axes.X1;
                    break;

                case Constants.ChartAxisX1Label:
                    settings = this.settings.Axes.X1.Label;
                    break;

                case Constants.ChartAxisX2:
                    settings = this.settings.Axes.X2;
                    break;

                case Constants.ChartAxisX2Label:
                    settings = this.settings.Axes.X2.Label;
                    break;

                case Constants.ChartAxisY1:
                    settings = this.settings.Axes.Y1;
                    break;

                case Constants.ChartAxisY1Label:
                    settings = this.settings.Axes.Y1.Label;
                    break;

                case Constants.ChartAxisY2:
                    settings = this.settings.Axes.Y2;
                    break;

                case Constants.ChartAxisY2Label:
                    settings = this.settings.Axes.Y2.Label;
                    break;

                default:
                    settings = this.settings; // TODO 
                    break;
            }

            return settings;
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
                var colour = dialogCustomColour.Color.ToArgb();

                var settings = GetSettings();

                // TODO - Will need to maintain a list of custom colours and use them to populate the dialog, save to settings etc.

                switch (button.Name)
                {
                    case BUTTON_BACKGROUND:
                        settings.BackColour = $"#{colour}";
                        comboBackground.SelectAll();
                        comboBackground.SelectedText = GetColourName(settings.BackColour);
                        break;

                    case BUTTON_FOREGROUND:
                        settings.ForeColour = $"#{colour}";
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
                var colourSettings = GetSettings();

                switch (combo.Name)
                {
                    case COMBO_BACKGROUND:
                        colourSettings.BackColour = (combo.Text == Resources.Custom && !string.IsNullOrEmpty(customBackColour)) ? customBackColour : combo.Text;
                        break;

                    case COMBO_FOREGROUND:
                        colourSettings.ForeColour = (combo.Text == Resources.Custom && !string.IsNullOrEmpty(customForeColour)) ? customForeColour : combo.Text;
                        break;
                }

                SectionChanged?.Invoke(this, settings);
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
    }
}
