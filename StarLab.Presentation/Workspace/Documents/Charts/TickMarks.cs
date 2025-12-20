using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart axis scale tick marks.
    /// </summary>
    internal class TickMarks : FrameElement, ITickMarks
    {
        private int length;

        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarks"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="TickMarks"/>.</param>
        public TickMarks(TickMarksDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            length = dto.Length;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarks"> class.
        /// </summary>
        /// <param name="length">The default length.</param>
        public TickMarks(int length)
            : base(Constants.DefaultForeColour, true)
        {
            this.length = length;
        }

        /// <summary>
        /// Gets the length of the tickamrks.
        /// </summary>
        public int Length => Visible ? length : 0;
    }
}
