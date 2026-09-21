using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart data points.
    /// </summary>
    internal class Points : ChartElement, IPoints
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlotArea"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Points"/>.</param>
        public Points(PointsDTO dto)
            : base(dto.Visible)
        {
            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultBackColour : dto.Colour;

            Size = dto.Size;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Points"> class.
        /// </summary>
        public Points()
            : base(true)
        {
            Colour = Constants.DefaultForeColour;

            Size = 1;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size { get; }
    }
}
