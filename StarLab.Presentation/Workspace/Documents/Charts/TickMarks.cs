using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart axis scale tick marks.
    /// </summary>
    internal class TickMarks : ITickMarks
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarks"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="TickMarks"/>.</param>
        public TickMarks(TickMarksDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            Visible = dto.Visible;
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
        /// A flag indicating that the tick marks are visible.
        /// </summary>
        public bool Visible { get; }
    }
}
