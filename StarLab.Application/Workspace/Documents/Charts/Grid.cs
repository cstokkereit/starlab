namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of the chart grid.
    /// </summary>
    internal class Grid
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Grid"/>.</param>
        public Grid(GridDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            MajorGridLines = dto.MajorGridLines == null ? new GridLines() : new GridLines(dto.MajorGridLines);
            MinorGridLines = dto.MinorGridLines == null ? new GridLines() : new GridLines(dto.MinorGridLines);

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Visible = dto.Visible;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"> class.
        /// </summary
        public Grid()
        {
            MajorGridLines = new GridLines();
            MinorGridLines = new GridLines();

            Colour = Constants.DefaultForeColour;
            
            Visible = true;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// Gets the major grid lines.
        /// </summary>
        public GridLines MajorGridLines { get; }

        /// <summary>
        /// Gets the minor grid lines.
        /// </summary>
        public GridLines MinorGridLines { get; }

        /// <summary>
        /// A flag indicating whether the grid is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
