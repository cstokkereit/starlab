namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Defines the methods used by the <see cref="IChartSettingsView"/> to communicate with its presenter.
    /// </summary>
    public interface IChartSettingsViewPresenter : IChildViewPresenter
    {
        /// <summary>
        /// Applies the preview settings to the chart view.
        /// </summary>
        /// <param name="settings">The <see cref="IChartSettings"/> that specifies the state of the chart.</param>
        void ApplyPreviewSettings(IChartSettings settings);

        /// <summary>
        /// Shows the settings for the specified settings group.
        /// </summary>
        /// <param name="group">The name of the settings group to show.</param>
        void ShowSettingsGroup(string name);
    }
}
