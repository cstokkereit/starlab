namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the grid while the chart is being configured.
    /// </summary>
    internal class GridSettings : IGridSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridSettings"/> class.
        /// </summary>
        /// <param name="grid">An <see cref="IGrid"/> that specifies the initial state of the grid.</param>
        public GridSettings(IGrid grid)
        {
            BackColour = grid.BackColour;
            ForeColour = grid.ForeColour;

            MajorGridLines = new GridLineSettings(grid.MajorGridLines);
            MinorGridLines = new GridLineSettings(grid.MinorGridLines);

            Visible = grid.Visible;
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
        /// Gets the major grid line settings.
        /// </summary>
        public IGridLineSettings MajorGridLines { get; }

        /// <summary>
        /// Gets the minor grid line settings.
        /// </summary>
        public IGridLineSettings MinorGridLines { get; }

        /// <summary>
        /// Gets or sets a flag that determines whether the grid is visible.
        /// </summary>
        public bool Visible { get; set; }
    }
}
