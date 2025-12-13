using StarLab.Application.Workspace.Documents.Charts;
using System.Diagnostics;

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
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="PlotArea"/>.</param>
        public PlotArea(PlotAreaDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.Grid != null);

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            Grid = new Grid(dto.Grid);
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
