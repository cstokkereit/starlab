using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart plot area.
    /// </summary>
    internal class PlotArea : IPlotArea
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlotArea"> class.
        /// </summary>
        /// <param name="dto">A <see cref="PlotAreaDTO"/> that specifies the initial state of the plot area.</param>
        public PlotArea(PlotAreaDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = dto.BackColour;
            ForeColour = dto.ForeColour;
            Visible = dto.Visible;

            Points = dto.Points == null ? new Points(ForeColour) : new Points(dto.Points);

            Grid = dto.Grid == null ? new Grid(ForeColour) : new Grid(dto.Grid);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PlotArea"> class.
        /// </summary>
        /// <param name="backColour">A <see cref="string"/> value that specifies the background colour of the plot area.</param>
        /// <param name="foreColour">A <see cref="string"/> value that specifies the foreground colour of the plot area.</param>
        public PlotArea(string backColour, string foreColour)
        {
            BackColour = backColour;
            ForeColour = foreColour;
            Visible = true;

            Points = new Points(ForeColour);

            Grid = new Grid(ForeColour);
        }

        /// <summary>
        /// Gets the background colour of the plot area.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour of the plot area.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the chart grid.
        /// </summary>
        public IGrid Grid { get; }

        /// <summary>
        /// Gets the chart data points.
        /// </summary>
        public IPoints Points { get; }

        /// <summary>
        /// A flag indicating that the plot area is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
