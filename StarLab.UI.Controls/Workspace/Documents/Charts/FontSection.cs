using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the font used by chart labels.
    /// </summary>
    public partial class FontSection : UserControl, ISettingsSection
    {
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
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IFontSettings GetSettings()
        {
            IFontSettings? settings = null;

            switch (group)
            {
                case Constants.ChartTitle:
                    settings = this.settings.Title.Font;
                    break;

                case Constants.ChartAxisX1Label:
                    settings = this.settings.Axes.X1.Label.Font;
                    break;

                case Constants.ChartAxisX1TickLabels:
                    settings = this.settings.Axes.X1.Scale.TickLabels.Font;
                    break;

                case Constants.ChartAxisX2Label:
                    settings = this.settings.Axes.X2.Label.Font;
                    break;

                case Constants.ChartAxisX2TickLabels:
                    settings = this.settings.Axes.X2.Scale.TickLabels.Font;
                    break;

                case Constants.ChartAxisY1Label:
                    settings = this.settings.Axes.Y1.Label.Font;
                    break;

                case Constants.ChartAxisY1TickLabels:
                    settings = this.settings.Axes.Y1.Scale.TickLabels.Font;
                    break;

                case Constants.ChartAxisY2Label:
                    settings = this.settings.Axes.X2.Label.Font;
                    break;

                case Constants.ChartAxisY2TickLabels:
                    settings = this.settings.Axes.X2.Scale.TickLabels.Font;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(group), group);
            }

            return settings;
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
