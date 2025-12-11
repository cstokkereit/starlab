using System.Diagnostics;

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
            ArgumentNullException.ThrowIfNull(nameof(dto));

            Debug.Assert(dto.MajorGridLines != null);
            Debug.Assert(dto.MinorGridLines != null);

            if (string.IsNullOrEmpty(dto.BackColour))
            {
                BackColour = Constants.DefaultBackColour;
            }
            else
            {
                BackColour = dto.BackColour;
            }

            if (string.IsNullOrEmpty(dto.ForeColour))
            {
                ForeColour = Constants.DefaultForeColour;
            }
            else
            {
                ForeColour = dto.ForeColour;
            }

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
