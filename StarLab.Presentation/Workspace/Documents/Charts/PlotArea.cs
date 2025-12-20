using StarLab.Application.Workspace.Documents.Charts;
using System.Diagnostics;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart plot area.
    /// </summary>
    internal class PlotArea : ChartElement, IPlotArea
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlotArea"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="PlotArea"/>.</param>
        public PlotArea(PlotAreaDTO dto)
            : base(true)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.Grid != null);

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.DefaultBackColour : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.DefaultForeColour : dto.ForeColour;

            Grid = dto.Grid == null ? new Grid() : new Grid(dto.Grid);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="PlotArea"> class.
        /// </summary>
        public PlotArea()
            : base(true)
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
        public IGrid Grid { get; }
    }
}
