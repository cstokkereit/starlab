namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the grid lines while the chart is being configured.
    /// </summary>
    internal class GridLineSettings : FrameElementSettings, IGridLineSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridLineSettings"/> class.
        /// </summary>
        /// <param name="gridLines">An <see cref="IGridLines"/> that specifies the initial state of the grid lines.</param>
        public GridLineSettings(IGridLines gridLines)
            : base(gridLines.Colour, gridLines.Visible)
        {
        
        }
    }
}
