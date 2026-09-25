using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart grid lines.
    /// </summary>
    internal class GridLines : ChartElement, IGridLines
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridLines"> class.
        /// </summary>
        /// <param name="dto">A <see cref="GridLinesDTO"/> that specifies the initial state of the grid lines.</param>
        public GridLines(GridLinesDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Opacity = dto.Opacity;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="GridLines"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the grid lines.</param>
        /// <param name="opacity">The opacity of the grid lines.</param>
        /// <param name="visible">Specifies whether the grid lines are visble or not.</param>
        public GridLines(string colour, double opacity, bool visible)
            : base(colour, visible) 
        {
            Opacity = opacity;
        }

        /// <summary>
        /// Gets the opacity of the grid lines.
        /// </summary>
        public double Opacity { get; }
    }
}
