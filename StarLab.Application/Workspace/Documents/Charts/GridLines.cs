namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of the chart grid lines.
    /// </summary>
    internal class GridLines
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="GridLines"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="GridLines"/>.</param>
        public GridLines(GridLinesDTO dto)
        {
            ArgumentNullException.ThrowIfNull(nameof(dto));

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
