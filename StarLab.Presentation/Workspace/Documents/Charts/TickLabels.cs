using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of the chart axis scale tick labels.
    /// </summary>
    internal class TickLabels : TextElement, ITickLabels
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabels"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="TickLabels"/>.</param>
        public TickLabels(TickLabelsDTO dto)
            : base(dto.Colour, dto.Font == null ? new Font() : new Font(dto.Font), dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Rotation = dto.Rotation;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabels"> class.
        /// </summary>
        public TickLabels()
            : base(Constants.DefaultForeColour, new Font(), true)
        {

        }

        /// <summary>
        /// Gets the angle of rotation for the tick labels.
        /// </summary>
        public int Rotation { get; }
    }
}
