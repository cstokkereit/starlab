namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the chart axis scale tick labels.
    /// </summary>
    public interface ITickLabels
    {
        /// <summary>
        /// Gets the background colour.
        /// </summary>
        string BackColour { get; }

        /// <summary>
        /// Gets the tick label font.
        /// </summary>
        IFont Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the rotation angle for the tick labels.
        /// </summary>
        int Rotation { get; }

        /// <summary>
        /// A flag indicating that the tick labels are visible.
        /// </summary>
        bool Visible { get; }
    }
}
