using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart axis scale tick labels.
    /// </summary>
    internal class TickLabels : ChartElement, ITickLabels
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabels"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the tick labels.</param>
        public TickLabels(TickLabelsDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Font = new Font(dto.Font);

            Rotation = dto.Rotation;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabels"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the foreground colour of the tick labels.</param>
        /// <param name="font">An <see cref="IFont"/> that specifies the tick label font.</param>
        /// 
        public TickLabels(string colour, IFont font)
            : base(colour, true)
        {
            Font = font;

            Rotation = 0;
        }

        /// <summary>
        /// Gets the tick label font.
        /// </summary>
        public IFont Font { get; }

        /// <summary>
        /// Gets the angle of rotation for the tick labels.
        /// </summary>
        public int Rotation { get; }
    }
}
