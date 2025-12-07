using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the axis scale.
    /// </summary>
    public partial class ScaleSection : UserControl, ISettingsSection
    {
        private readonly IDictionary<string , IScaleSettings> settingsByGroup = new Dictionary<string , IScaleSettings>(); // A dictionary containing the scale settings indexed by settings group.

        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        public event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        public ScaleSection(IChartSettings settings, string group)
        {
            InitializeComponent();

            this.group = group;
            this.settings = settings;

            var scaleSettings = GetSettings();

            textMaximum.Text = scaleSettings.Maximum.ToString();
            textMinimum.Text = scaleSettings.Minimum.ToString();
            checkAutoScale.Checked = scaleSettings.Autoscale;
            checkReversed.Checked = scaleSettings.Reversed;

            AttachEventHandlers();
        }

        /// <summary>
        /// Attaches the event handlers for the child <see cref="Control"/>s that comprise this <see cref="UserControl"/>
        /// </summary>
        private void AttachEventHandlers()
        {
            checkAutoScale.CheckStateChanged += OnScaleChanged;
            checkReversed.CheckStateChanged += OnScaleChanged;
            textMaximum.TextChanged += OnScaleChanged;
            textMinimum.TextChanged += OnScaleChanged;
        }

        /// <summary>
        /// Gets the <see cref="ILabelSettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="IScaleSettings"/>.</returns>
        private IScaleSettings GetSettings()
        {
            if (settingsByGroup.Count == 0)
            {
                settingsByGroup.Add(Constants.ChartAxisX1Scale, settings.Axes.X1.Scale);
                settingsByGroup.Add(Constants.ChartAxisX2Scale, settings.Axes.X2.Scale);
                settingsByGroup.Add(Constants.ChartAxisY1Scale, settings.Axes.Y1.Scale);
                settingsByGroup.Add(Constants.ChartAxisY2Scale, settings.Axes.Y2.Scale);
            }

            return settingsByGroup[group];
        }

        /// <summary>
        /// Event handler for the <see cref="TextBox.TextChanged"/> and <see cref="CheckBox.CheckStateChanged"> events.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnScaleChanged(object? sender, EventArgs e)
        {
            var scaleSettings = settingsByGroup[group];

            if (double.TryParse(textMinimum.Text, out double minimum)) scaleSettings.Minimum = minimum;
            if (double.TryParse(textMaximum.Text, out double maximum)) scaleSettings.Maximum = maximum;
            
            scaleSettings.Autoscale = checkAutoScale.Checked;
            scaleSettings.Reversed = checkReversed.Checked;

            SectionChanged?.Invoke(this, settings);
        }
    }
}
