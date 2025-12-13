namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the grid lines while the chart is being configured.
    /// </summary>
    internal class GridLineSettings : IGridLineSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridLineSettings"/> class.
        /// </summary>
        /// <param name="gridLines">An <see cref="IGridLines"/> that specifies the initial state of the grid lines.</param>
        public GridLineSettings(IGridLines gridLines)
        {
            BackColour = gridLines.BackColour;
            ForeColour = gridLines.ForeColour;
            Visible = gridLines.Visible;
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the grid lines are visible.
        /// </summary>
        public bool Visible { get; set; }
    }
}
