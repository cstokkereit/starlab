namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the chart axes while the chart is being configured.
    /// </summary>
    public interface IAxesSettings : IColourSettings, IVisibilitySettings
    {
        /// <summary>
        /// Gets the settings for the bottom axis.
        /// </summary>
        IAxisSettings X1 { get; }

        /// <summary>
        /// Gets the settings for the top axis.
        /// </summary>
        IAxisSettings X2 { get; }

        /// <summary>
        /// Gets the settings for the left axis.
        /// </summary>
        IAxisSettings Y1 { get; }

        /// <summary>
        /// Gets the settings for the right axis.
        /// </summary>
        IAxisSettings Y2 { get; }

        /// <summary>
        /// Gets the font settings.
        /// </summary>
        IFontSettings Font { get; }
    }
}
