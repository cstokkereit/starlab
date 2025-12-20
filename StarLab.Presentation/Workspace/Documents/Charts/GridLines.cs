using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart grid lines.
    /// </summary>
    internal class GridLines : FrameElement, IGridLines
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridLines"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="GridLines"/>.</param>
        public GridLines(GridLinesDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="GridLines"> class.
        /// </summary>
        public GridLines()
            : base(Constants.DefaultForeColour, true) { }
    }
}
