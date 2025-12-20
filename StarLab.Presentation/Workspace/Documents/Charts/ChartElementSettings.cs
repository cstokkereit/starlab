namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a chart element while the chart is being configured.
    /// </summary>
    internal abstract class ChartElementSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartElementSettings"/> class.
        /// </summary>
        /// <param name="visible">A flag indicating whether the frame element is visible.</param>
        public ChartElementSettings(bool visible)
        {
            Visible = visible;
        }

        /// <summary>
        /// Gets or sets a flag indicating that the chart element is visible.
        /// </summary>
        public virtual bool Visible { get; set; }
    }
}
