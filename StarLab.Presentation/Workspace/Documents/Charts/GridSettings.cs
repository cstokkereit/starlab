namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the grid while the chart is being configured.
    /// </summary>
    internal class GridSettings : FrameElementSettings, IGridSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridSettings"/> class.
        /// </summary>
        /// <param name="grid">An <see cref="IGrid"/> that specifies the initial state of the grid.</param>
        public GridSettings(IGrid grid)
            : base(grid.Colour, grid.Visible)
        {
            MajorGridLines = new GridLineSettings(grid.MajorGridLines);
            MinorGridLines = new GridLineSettings(grid.MinorGridLines);
        }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public override string Colour 
        {
            get => base.Colour;

            set
            {
                if (MajorGridLines != null) MajorGridLines.Colour = value;
                if (MinorGridLines != null) MinorGridLines.Colour = value;

                base.Colour = value;
            }
        }

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
        public override bool Visible
        { 
            get => base.Visible;

            set
            {
                if (MajorGridLines != null) MajorGridLines.Visible = value;
                if (MinorGridLines != null) MinorGridLines.Visible = value;

                base.Visible = value;
            }
        }
    }
}
