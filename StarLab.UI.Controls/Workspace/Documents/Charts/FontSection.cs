using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the font used by chart labels.
    /// </summary>
    public partial class FontSection : UserControl, ISettingsSection
    {
        private readonly IDictionary<string, IFontSettings> settingsByGroup = new Dictionary<string, IFontSettings>(); // A dictionary containing the font settings indexed by settings group.

        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        public event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="FontSection"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        /// <param name="group">The name of the settings group that this control represents.</param>
        public FontSection(IChartSettings settings, string group)
        {
            InitializeComponent();

            this.group = group;
            this.settings = settings;

            for (int size = 6; size < 25; size++)
            {
                comboFontSizes.Items.Add(size.ToString());
            }

            var fontSettings = GetSettings();

            comboFontFamilies.SelectedText = fontSettings.Family;
            comboFontSizes.SelectedIndex = fontSettings.Size - 6;
            checkBoxUnderline.Checked = fontSettings.Underline;
            checkBoxItalic.Checked = fontSettings.Italic;
            checkBoxBold.Checked = fontSettings.Bold;

            AttachEventHandlers();
        }

        /// <summary>
        /// Gets the <see cref="IFontSettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="IFontSettings"/>.</returns>
        private IFontSettings GetSettings()
        {
            if (settingsByGroup.Count == 0)
            {
                settingsByGroup.Add(Constants.ChartAxisX1Label, settings.Axes.X1.Label.Font);
                settingsByGroup.Add(Constants.ChartAxisX1TickLabels, settings.Axes.X1.Scale.TickLabels.Font);
                settingsByGroup.Add(Constants.ChartAxisX2Label, settings.Axes.X2.Label.Font);
                settingsByGroup.Add(Constants.ChartAxisX2TickLabels, settings.Axes.X2.Scale.TickLabels.Font);
                settingsByGroup.Add(Constants.ChartAxisY1Label, settings.Axes.Y1.Label.Font);
                settingsByGroup.Add(Constants.ChartAxisY1TickLabels, settings.Axes.Y1.Scale.TickLabels.Font);
                settingsByGroup.Add(Constants.ChartAxisY2Label, settings.Axes.Y2.Label.Font);
                settingsByGroup.Add(Constants.ChartAxisY2TickLabels, settings.Axes.Y2.Scale.TickLabels.Font);
                settingsByGroup.Add(Constants.ChartTitle, settings.Title.Font);
            }

            return settingsByGroup[group];
        }

        /// <summary>
        /// Attaches the event handlers for the child <see cref="Control"/>s that comprise this <see cref="UserControl"/>
        /// </summary>
        private void AttachEventHandlers()
        {
            checkBoxUnderline.CheckStateChanged += OnFontChanged;
            checkBoxItalic.CheckStateChanged += OnFontChanged;
            checkBoxBold.CheckStateChanged += OnFontChanged;
            comboFontFamilies.TextChanged += OnFontChanged;
            comboFontSizes.TextChanged += OnFontChanged;
        }

        /// <summary>
        /// Event handler for the <see cref="ComboBox.DropDown"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnDropDown(object sender, EventArgs e)
        {
            if (comboFontFamilies.Items.Count == 0)
            {
                foreach (var family in FontFamily.Families)
                {
                    comboFontFamilies.Items.Add(family.Name);
                }
            }
        }

        /// <summary>
        /// Event handler for the <see cref="CheckBox.CheckStateChanged"/> and <see cref="ComboBox.TextChanged"> events.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnFontChanged(object? sender, EventArgs e)
        {
            var fontSettings = GetSettings();

            fontSettings.Family = comboFontFamilies.Text;
            fontSettings.Size = int.Parse(comboFontSizes.Text);
            fontSettings.Underline = checkBoxUnderline.Checked;
            fontSettings.Italic = checkBoxItalic.Checked;
            fontSettings.Bold = checkBoxBold.Checked;

            SectionChanged?.Invoke(this, settings);
        }
    }
}
