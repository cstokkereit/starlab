namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the data points while the chart is being configured.
    /// </summary>
    internal class PointSettings : ChartElementSettings, IPointSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="IPointSettings"> class.
        /// </summary>
        /// <param name="points">An <see cref="IPoints"/> that specifies the initial state of the data points.</param>
        public PointSettings(IPoints points)
            : base(points)
        {
            Size = points.Size;
        }

        /// <summary>
        /// Gets or sets the point size.
        /// </summary>
        public int Size { get; set; }
    }
}
