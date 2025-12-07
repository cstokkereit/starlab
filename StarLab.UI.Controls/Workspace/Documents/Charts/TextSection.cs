using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the content and format of text displayed by chart elements.
    /// </summary>
    public partial class TextSection : UserControl, ISettingsSection
    {
        private readonly IDictionary<string, ILabelSettings> settingsByGroup = new Dictionary<string, ILabelSettings>(); // A dictionary containing the label settings indexed by settings group.

        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        public event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="TextSection"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        /// <param name="group">The name of the settings group that this control represents.</param>
        public TextSection(IChartSettings settings, string group)
        {
            InitializeComponent();

            this.group = group;
            this.settings = settings;
            
            textLabel.Text = GetSettings().Text;

            textLabel.TextChanged += OnTextChanged;
        }

        /// <summary>
        /// Gets the <see cref="ILabelSettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="ILabelSettings"/>.</returns>
        private ILabelSettings GetSettings()
        {
            if (settingsByGroup.Count == 0)
            {
                settingsByGroup.Add(Constants.ChartAxisX1Label, settings.Axes.X1.Label);
                settingsByGroup.Add(Constants.ChartAxisX2Label, settings.Axes.X2.Label);
                settingsByGroup.Add(Constants.ChartAxisY1Label, settings.Axes.Y1.Label);
                settingsByGroup.Add(Constants.ChartAxisY2Label, settings.Axes.Y2.Label);
                settingsByGroup.Add(Constants.ChartTitle, settings.Title);
            }

            return settingsByGroup[group];
        }

        /// <summary>
        /// Event handler for the <see cref="TextBox.TextChanged"> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnTextChanged(object? sender, EventArgs e)
        {
            GetSettings().Text = textLabel.Text;

            SectionChanged?.Invoke(this, settings);
        }
    }
}
