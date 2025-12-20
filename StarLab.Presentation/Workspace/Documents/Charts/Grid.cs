using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart grid.
    /// </summary>
    internal class Grid : FrameElement, IGrid
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Grid"/>.</param>
        public Grid(GridDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            MajorGridLines = dto.MajorGridLines == null ? new GridLines() : new GridLines(dto.MajorGridLines);
            MinorGridLines = dto.MinorGridLines == null ? new GridLines() : new GridLines(dto.MinorGridLines);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"> class.
        /// </summary>
        public Grid()
            : base(Constants.DefaultForeColour, true)
        {
            MajorGridLines = new GridLines();
            MinorGridLines = new GridLines();
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
