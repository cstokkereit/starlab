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
        void ApplyPreviewSettings();

        /// <summary>
        /// Shows the settings for the specified key.
        /// </summary>
        /// <param name="key">The settings key.</param>
        void ShowSettings(string key);
    }
}
