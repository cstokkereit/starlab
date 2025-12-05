using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    public partial class ScaleSection : UserControl, ISettingsSection
    {
        private const string X1 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisX1}/{Constants.Scale}";
        private const string X2 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisX2}/{Constants.Scale}";
        private const string Y1 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisY1}/{Constants.Scale}";
        private const string Y2 = $"{Constants.Chart}/{Constants.Axes}/{Constants.AxisY2}/{Constants.Scale}";

        private readonly IChartSettings settings; // The chart settings that are bound to this control.

        private readonly string group; // The name of the settings group that this control represents.

        public event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        public ScaleSection(IChartSettings settings, string group)
        {
            InitializeComponent();

            this.group = group;
            this.settings = settings;

            var scale = GetSettings();

            textMaximum.Text = scale.Maximum.ToString();
            textMinimum.Text = scale.Minimum.ToString();
            checkReversed.Checked = scale.Reversed;
        }

        

        // TODO 
        // 1. Tick Labels - show or not
        // 2. Major/Minor tick marks
        // 3. Tick mark spacing/density
        // 4. Number format
        // 5. Log scale
        // 6. Angle 
        // 7. Font

        // Need to update the view creation code, display code, settings, chart and chart dto etc.

        /// <summary>
        /// Gets the <see cref="ILabelSettings"/> for the specified settings group within the bound <see cref="IChartSettings"/>.
        /// </summary>
        /// <returns>The required <see cref="ILabelSettings"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        private IScaleSettings GetSettings()
        {
            IScaleSettings? settings = null;

            switch (group)
            {
                case X1:
                    settings = this.settings.Axes.X1.Scale;
                    break;

                case X2:
                    settings = this.settings.Axes.X2.Scale;
                    break;

                case Y1:
                    settings = this.settings.Axes.Y1.Scale;
                    break;

                case Y2:
                    settings = this.settings.Axes.X2.Scale;
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(group));
            }

            return settings;
        }
    }
}
