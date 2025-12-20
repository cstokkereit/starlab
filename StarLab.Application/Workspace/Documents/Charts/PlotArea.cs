namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of the chart plot area.
    /// </summary>
    internal class PlotArea
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlotArea"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="PlotArea"/>.</param>
        public PlotArea(PlotAreaDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Grid = dto.Grid == null ? new Grid() : new Grid(dto.Grid);

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.DefaultBackColour : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.DefaultForeColour : dto.ForeColour;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PlotArea"> class.
        /// </summary>
        public PlotArea()
        {
            BackColour = Constants.DefaultBackColour;
            ForeColour = Constants.DefaultForeColour;

            Grid = new Grid();
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
        /// Gets the chart grid.
        /// </summary>
        public Grid Grid { get; }
    }
}
