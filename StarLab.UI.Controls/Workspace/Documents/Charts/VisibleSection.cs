using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that is used to update the settings that control the visiblity of chart elements.
    /// </summary>
    public partial class VisibleSection : UserControl, ISettingsSection
    {
        private readonly IDictionary<string, IVisibilitySettings> settingsByGroup = new Dictionary<string, IVisibilitySettings>(); // A dictionary containing the visibility settings indexed by settings group.

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
        private IVisibilitySettings GetSettings()
        {
            if (settingsByGroup.Count == 0)
            {
                settingsByGroup.Add(Constants.ChartAxes, settings.Axes);
                settingsByGroup.Add(Constants.ChartAxisX1, settings.Axes.X1);
                settingsByGroup.Add(Constants.ChartAxisX1Label, settings.Axes.X1.Label);
                settingsByGroup.Add(Constants.ChartAxisX1MajorTickMarks, settings.Axes.X1.Scale.MajorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisX1MinorTickMarks, settings.Axes.X1.Scale.MinorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisX1Scale, settings.Axes.X1.Scale);
                settingsByGroup.Add(Constants.ChartAxisX1TickLabels, settings.Axes.X1.Scale.TickLabels);
                settingsByGroup.Add(Constants.ChartAxisX2, settings.Axes.X2);
                settingsByGroup.Add(Constants.ChartAxisX2Label, settings.Axes.X2.Label);
                settingsByGroup.Add(Constants.ChartAxisX2MajorTickMarks, settings.Axes.X2.Scale.MajorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisX2MinorTickMarks, settings.Axes.X2.Scale.MinorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisX2Scale, settings.Axes.X2.Scale);
                settingsByGroup.Add(Constants.ChartAxisX2TickLabels, settings.Axes.X2.Scale.TickLabels);
                settingsByGroup.Add(Constants.ChartAxisY1, settings.Axes.Y1);
                settingsByGroup.Add(Constants.ChartAxisY1Label, settings.Axes.Y1.Label);
                settingsByGroup.Add(Constants.ChartAxisY1MajorTickMarks, settings.Axes.Y1.Scale.MajorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisY1MinorTickMarks, settings.Axes.Y1.Scale.MinorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisY1Scale, settings.Axes.Y1.Scale);
                settingsByGroup.Add(Constants.ChartAxisY1TickLabels, settings.Axes.Y1.Scale.TickLabels);
                settingsByGroup.Add(Constants.ChartAxisY2, settings.Axes.Y2);
                settingsByGroup.Add(Constants.ChartAxisY2Label, settings.Axes.Y2.Label);
                settingsByGroup.Add(Constants.ChartAxisY2MajorTickMarks, settings.Axes.Y2.Scale.MajorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisY2MinorTickMarks, settings.Axes.Y2.Scale.MinorTickMarks);
                settingsByGroup.Add(Constants.ChartAxisY2Scale, settings.Axes.Y2.Scale);
                settingsByGroup.Add(Constants.ChartAxisY2TickLabels, settings.Axes.Y2.Scale.TickLabels);
                settingsByGroup.Add(Constants.ChartPlotAreaGrid, settings.PlotArea.Grid);
                settingsByGroup.Add(Constants.ChartPlotAreaMajorGridLines, settings.PlotArea.Grid.MajorGridLines);
                settingsByGroup.Add(Constants.ChartPlotAreaMinorGridLines, settings.PlotArea.Grid.MinorGridLines);
                settingsByGroup.Add(Constants.ChartTitle, settings.Title);
            }

            return settingsByGroup[group];
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
