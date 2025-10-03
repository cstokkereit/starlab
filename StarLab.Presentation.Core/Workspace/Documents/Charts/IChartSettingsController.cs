namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a controller that can be used to control a chart settings panel.
    /// </summary>
    public interface IChartSettingsController : IChildViewController
    {
        /// <summary>
        /// Applies the chart settings to the document.
        /// </summary>
        void ApplySettings();

        /// <summary>
        /// Reverts the changes to the settings.
        /// </summary>
        void RevertSettings();

        /// <summary>
        /// Updates the chart settings.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that contains the chart.</param>
        void UpdateSettings(IDocument document);
    }
}
