using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart grid lines.
    /// </summary>
    internal class GridLines : IGridLines
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridLines"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="GridLines"/>.</param>
        public GridLines(GridLinesDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            Visible = dto.Visible;
        }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// A flag indicating whether the grid lines are visible.
        /// </summary>
        public bool Visible { get; }
    }
}
