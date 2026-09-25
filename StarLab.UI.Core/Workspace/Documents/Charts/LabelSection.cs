using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the content and format of text displayed by chart elements.
    /// </summary>
    public partial class LabelSection : UserControl, ISettingsSection
    {
        private readonly ILabelSettings settings; // The label settings that are bound to this control.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="LabelSection"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        public LabelSection(ILabelSettings settings)
        {
            InitializeComponent();

            this.settings = settings;
            
            textLabel.Text = settings.Text;

            textLabel.TextChanged += OnTextChanged;
        }

        /// <summary>
        /// Event handler for the <see cref="TextBox.TextChanged"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnTextChanged(object? sender, EventArgs e)
        {
            settings.Text = textLabel.Text;

            SectionChanged?.Invoke(this, new EventArgs());
        }
    }
}
