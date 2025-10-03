using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the font used by chart labels.
    /// </summary>
    public partial class FontSection : UserControl, ISettingsSection
    {
        private const string TITLE = $"{Constants.Chart}/{Constants.Title}";
        private const string X1 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisX1}/{Constants.Label}";
        private const string X2 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisX2}/{Constants.Label}";
        private const string Y1 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisY1}/{Constants.Label}";
        private const string Y2 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisY2}/{Constants.Label}";

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

            foreach (var family in FontFamily.Families)
            {
                comboFontFamilies.Items.Add(family.Name);
            }

            for (int size = 6; size < 25; size++)
            {
                comboFontSizes.Items.Add(size.ToString());
            }

            var fontSettings = GetSettings();

            comboFontFamilies.SelectedIndex = comboFontFamilies.FindStringExact(fontSettings.Family);
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
                case X1:
                    settings = this.settings.Axes.X1.Label.Font;
                    break;

                case X2:
                    settings = this.settings.Axes.X2.Label.Font;
                    break;

                case Y1:
                    settings = this.settings.Axes.Y1.Label.Font;
                    break;

                case Y2:
                    settings = this.settings.Axes.X2.Label.Font;
                    break;

                case TITLE:
                    settings = this.settings.Title.Font;
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
            comboFontFamilies.TextChanged += OnFontChanged;
            checkBoxUnderline.CheckStateChanged += OnFontChanged;
            comboFontSizes.TextChanged += OnFontChanged;
            checkBoxItalic.CheckStateChanged += OnFontChanged;
            checkBoxBold.CheckStateChanged += OnFontChanged;
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
