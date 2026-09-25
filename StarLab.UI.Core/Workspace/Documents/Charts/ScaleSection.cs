using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Core.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the axis scale.
    /// </summary>
    public partial class ScaleSection : UserControl, ISettingsSection
    {
        private readonly IScaleSettings settings; // The chart settings that are bound to this control.

        public event EventHandler? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ScaleSection"> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        public ScaleSection(IScaleSettings settings)
        {
            InitializeComponent();

            this.settings = settings;

            textMaximum.Text = settings.Maximum.ToString();
            textMinimum.Text = settings.Minimum.ToString();
            checkAutoScale.Checked = settings.Autoscale;
            checkReversed.Checked = settings.Reversed;

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
        /// Event handler for the <see cref="TextBox.TextChanged"/> and <see cref="CheckBox.CheckStateChanged"> events.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnScaleChanged(object? sender, EventArgs e)
        {
            if (double.TryParse(textMinimum.Text, out double minimum)) settings.Minimum = minimum;
            if (double.TryParse(textMaximum.Text, out double maximum)) settings.Maximum = maximum;

            settings.Autoscale = checkAutoScale.Checked;
            settings.Reversed = checkReversed.Checked;

            SectionChanged?.Invoke(this, new EventArgs());
        }
    }
}
