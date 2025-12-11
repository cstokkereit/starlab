using System.Diagnostics;

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
            ArgumentNullException.ThrowIfNull(nameof(dto));

            Debug.Assert(dto.Grid != null);

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
        /// Gets the plot area grid.
        /// </summary>
        public Grid Grid { get; }
    }
}
