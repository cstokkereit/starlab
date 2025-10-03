using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.UI.Controls.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a section of the user interface that contains a group of releated settings.
    /// </summary>
    public interface ISettingsSection
    {
        /// <summary>
        /// An event that gets fired whenever any of the section settings is changed.
        /// </summary>
        event EventHandler<IChartSettings>? SectionChanged; // An event that gets fired whenever any of the section settings is changed.

        /// <summary>
        /// Gets or sets the top coordinate of this section.
        /// </summary>
        int Top { get; set; }
    }
}
