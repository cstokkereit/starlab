﻿namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Displays the settings sections applicable to the chart settings group.
    /// </summary>
    internal class ChartSettingsGroupManager : SettingsGroupManager<IChartSettingsView>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsGroupManager"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IChartSettingsView"/> that this class uses to display the settings sections applicable to this settings group.</param>
        /// <param name="group">The name of the settings group managed by this settings manager.</param>
        public ChartSettingsGroupManager(IChartSettingsView view, string group)
            : base(view, group) { }

        /// <summary>
        /// Shows the settings sections required by this group in the target view.
        /// </summary>
        /// <param name="settings">An <see cref="IChartSettings"/> that represents the current state of the chart.</param>
        public override void ShowSettings(IChartSettings settings)
        {
            View.AppendColourSection(settings, Group);
        }
    }
}
