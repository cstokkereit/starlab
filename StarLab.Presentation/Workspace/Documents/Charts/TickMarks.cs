using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart axis scale tick marks.
    /// </summary>
    internal class TickMarks : ChartElement, ITickMarks
    {
        private int length; // The length of the tick marks.

        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarks"> class.
        /// </summary>
        /// <param name="dto">A <see cref="TickMarksDTO"/> that specifies the initial state of the tick marks.</param>
        public TickMarks(TickMarksDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            length = dto.Length;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarks"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the tick marks.</param>
        /// <param name="length">The default length.</param>
        /// <param name="visible">Specifies whether the tick marks are visble or not.</param>
        public TickMarks(string colour, int length, bool visible)
            : base(colour, visible)
        {
            this.length = length;
        }

        /// <summary>
        /// Gets the length of the tickamrks.
        /// </summary>
        public int Length => Visible ? length : 0;
    }
}
