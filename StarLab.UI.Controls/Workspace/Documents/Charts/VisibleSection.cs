using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the visiblity of chart elements.
    /// </summary>
    public partial class VisibleSection : UserControl, ISettingsSection
    {
        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        public event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Initialises a new instance of the <see cref="VisibleSection"/> class.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that are bound to this control.</param>
        /// <param name="group">The name of the settings group that this control represents.</param>
        public VisibleSection(IChartSettings settings, string group)
        {
            InitializeComponent();

            this.group = group;
            this.settings = settings;

            checkBoxVisible.Checked = GetSettings().Visible;

            checkBoxVisible.CheckStateChanged += OnCheckStateChanged;
        }

        /// <summary>
        /// Gets the <see cref="IVisibilitySettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="IVisibilitySettings"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IVisibilitySettings GetSettings()
        {
            IVisibilitySettings? settings = null;

            switch (group)
            {
                case Constants.ChartAxes:
                    settings = this.settings.Axes;
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

                case Constants.ChartAxisX1TickLabels:
                    settings = this.settings.Axes.X1.Scale.TickLabels;
                    break;

                case Constants.ChartAxisX2:
                    settings = this.settings.Axes.X2;
                    break;

                case Constants.ChartAxisX2Label:
                    settings = this.settings.Axes.X2.Label;
                    break;

                case Constants.ChartAxisX2TickLabels:
                    settings = this.settings.Axes.X2.Scale.TickLabels;
                    break;

                case Constants.ChartAxisY1:
                    settings = this.settings.Axes.Y1;
                    break;

                case Constants.ChartAxisY1Label:
                    settings = this.settings.Axes.Y1.Label;
                    break;

                case Constants.ChartAxisY1TickLabels:
                    settings = this.settings.Axes.Y1.Scale.TickLabels;
                    break;

                case Constants.ChartAxisY2:
                    settings = this.settings.Axes.Y2;
                    break;

                case Constants.ChartAxisY2Label:
                    settings = this.settings.Axes.Y2.Label;
                    break;

                case Constants.ChartAxisY2TickLabels:
                    settings = this.settings.Axes.Y2.Scale.TickLabels;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(group), group);
            }

            return settings;
        }

        /// <summary>
        /// Event handler for the <see cref="CheckBox.CheckStateChanged"/> event.
        /// </summary>
        /// <param name="sender">The <see cref="object"> that was the originator of the event.</param>
        /// <param name="e">An <see cref="EventArgs"/> that provides context for the event.</param>
        private void OnCheckStateChanged(object? sender, EventArgs e)
        {
            GetSettings().Visible = checkBoxVisible.Checked;

            SectionChanged?.Invoke(this, settings);
        }
    }
}
