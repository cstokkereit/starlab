using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart data points.
    /// </summary>
    internal class Points : ChartElement, IPoints
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Points"> class.
        /// </summary>
        /// <param name="backColour">A <see cref="string"/> value that specifies the background colour of the points.</param>
        /// <param name="dto">A <see cref="PointsDTO"/> that specifies the initial state of the points.</param>
        public Points(PointsDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            Size = dto.Size;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Points"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the foreground colour of the points.</param>
        public Points(string colour)
            : base(colour, true)
        {
            Size = 1;
        }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size { get; }
    }
}
