using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Presentation.Workspace.Documents.Charts.Overlays;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the font used by chart labels.
    /// </summary>
    public partial class FontSection : UserControl, ISettingsSection
    {
        private const int HEIGHT_WITHOUT_STYLES = 45; // The height of the control when style properties are not being handled.
        private const int HEIGHT_WITH_STYLES = 64; // The height of the control when style properties are being handled.

        private readonly IFontSettings settings; // The font settings that are bound to this control.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="FontSection"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IFontSettings"/> that are bound to this control.</param>
        public FontSection(IFontSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            ConfigureSection();

            AttachEventHandlers();
        }

        /// <summary>
        /// Configures the <see cref="UserControl"/> to display the child controls used to set the values of the font properties.
        /// </summary>
        private void ConfigureSection()
        {
            for (int size = 6; size < 25; size++)
            {
                comboFontSizes.Items.Add(size.ToString());
            }

            var font = settings.Font;

            comboFontFamilies.SelectedText = font.Family;
            comboFontSizes.SelectedIndex = font.Size - 6;

            if (settings is IOverlaySettings)
            {
                Height = HEIGHT_WITHOUT_STYLES;
            }
            else
            {
                checkBoxUnderline.Checked = font.Underline;
                checkBoxItalic.Checked = font.Italic;
                checkBoxBold.Checked = font.Bold;

                Height = HEIGHT_WITH_STYLES;
            }
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
            settings.SetFont(comboFontFamilies.Text, int.Parse(comboFontSizes.Text), checkBoxBold.Checked, checkBoxItalic.Checked, checkBoxUnderline.Checked);

            SectionChanged?.Invoke(this, new EventArgs());
        }
    }
}
