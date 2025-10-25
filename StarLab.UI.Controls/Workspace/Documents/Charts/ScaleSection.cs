using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    public partial class ScaleSection : UserControl, ISettingsSection
    {
        public ScaleSection()
        {
            InitializeComponent();
        }

        public event EventHandler<IChartSettings>? SectionChanged;

        // TODO 
        // 1. Tick Labels - show or not
        // 2. Major/Minor tick marks
        // 3. Tick mark spacing/density
        // 4. Number format
        // 5. Log scale
        // 6. Angle 
        // 7. Font

        // Need to update the view creation code, display code, settings, chart and chart dto etc.
    }
}
