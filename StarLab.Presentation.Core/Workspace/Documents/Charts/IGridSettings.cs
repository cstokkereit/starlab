namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the grid while the chart is being configured.
    /// </summary>
    public interface IGridSettings : IFrameElementSettings
    {
        /// <summary>
        /// Gets the major grid line settings.
        /// </summary>
        IGridLineSettings MajorGridLines { get; }

        /// <summary>
        /// Gets the minor grid line settings.
        /// </summary>
        IGridLineSettings MinorGridLines { get; }
    }
}
