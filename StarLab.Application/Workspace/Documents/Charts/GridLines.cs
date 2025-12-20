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
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Visible = dto.Visible;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="GridLines"> class.
        /// </summary>
        public GridLines()
        {
            Colour = Constants.DefaultForeColour;

            Visible = true;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// A flag indicating whether the grid lines are visible.
        /// </summary>
        public bool Visible { get; }
    }
}
