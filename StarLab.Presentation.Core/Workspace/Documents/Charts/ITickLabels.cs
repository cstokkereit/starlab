namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart axis scale tick labels.
    /// </summary>
    public interface ITickLabels : IChartElement
    {
        /// <summary>
        /// Gets the tick label font.
        /// </summary>
        IFont Font { get; }

        /// <summary>
        /// Gets the rotation angle for the tick labels.
        /// </summary>
        int Rotation { get; }
    }
}
