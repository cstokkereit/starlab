namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// Represents a controller that can be used to control a table settings panel.
    /// </summary>
    public interface ITableSettingsController : IChildViewController
    {
        /// <summary>
        /// Applies the table settings to the document.
        /// </summary>
        void ApplySettings();

        /// <summary>
        /// Reverts the changes to the settings.
        /// </summary>
        void RevertSettings();

        /// <summary>
        /// Updates the table settings.
        /// </summary>
        /// <param name="document">The <see cref="ITableDocument"/> that contains the table.</param>
        void UpdateSettings(ITableDocument document);
    }
}
