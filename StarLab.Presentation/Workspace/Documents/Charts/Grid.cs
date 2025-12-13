using StarLab.Application.Workspace.Documents.Charts;
using System.Diagnostics;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart grid.
    /// </summary>
    internal class Grid : IGrid
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Grid"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Grid"/>.</param>
        public Grid(GridDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.MajorGridLines != null);
            Debug.Assert(dto.MinorGridLines != null);

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            MajorGridLines = new GridLines(dto.MajorGridLines);
            MinorGridLines = new GridLines(dto.MinorGridLines);

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
        /// Gets the major grid lines.
        /// </summary>
        public IGridLines MajorGridLines { get; }

        /// <summary>
        /// Gets the minor grid lines.
        /// </summary>
        public IGridLines MinorGridLines { get; }

        /// <summary>
        /// A flag indicating whether the grid is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
