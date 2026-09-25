using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart grid.
    /// </summary>
    internal class Grid : ChartElement, IGrid
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the grid.</param>
        public Grid(GridDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            MajorGridLines = dto.MajorGridLines == null ? new GridLines(Colour, 0.3, true) : new GridLines(dto.MajorGridLines);
            MinorGridLines = dto.MinorGridLines == null ? new GridLines(Colour, 0.1, true) : new GridLines(dto.MinorGridLines);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the foreground colour of the grid.</param>
        public Grid(string colour)
            : base(colour, true)
        {
            MajorGridLines = new GridLines(Colour, 0.3, true);
            MinorGridLines = new GridLines(Colour, 0.1, true);
        }

        /// <summary>
        /// Gets the major grid lines.
        /// </summary>
        public IGridLines MajorGridLines { get; }

        /// <summary>
        /// Gets the minor grid lines.
        /// </summary>
        public IGridLines MinorGridLines { get; }
    }
}
