using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the font used by chart labels.
    /// </summary>
    public partial class FontSection : UserControl, ISettingsSection
    {
        private readonly IFontSettings settings; // The font settings that are bound to this control.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="FontSection"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        public FontSection(IFontSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            for (int size = 6; size < 25; size++)
            {
                comboFontSizes.Items.Add(size.ToString());
            }

            var font = settings.Font;

            comboFontFamilies.SelectedText = font.Family;
            comboFontSizes.SelectedIndex = font.Size - 6;
            checkBoxUnderline.Checked = font.Underline;
            checkBoxItalic.Checked = font.Italic;
            checkBoxBold.Checked = font.Bold;

            AttachEventHandlers();
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
