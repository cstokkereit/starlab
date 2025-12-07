using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart axis scale tick labels.
    /// </summary>
    internal class TickLabels : ITickLabels
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabels"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="TickLabels"/>.</param>
        public TickLabels(TickLabelsDTO? dto) 
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            Font = new Font(dto.Font);
            Rotation = dto.Rotation;
            Visible = dto.Visible;
        }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the tick label font.
        /// </summary>
        public IFont Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the angle of rotation for the tick labels.
        /// </summary>
        public int Rotation { get; }

        /// <summary>
        /// A flag indicating that the tick labels are visible.
        /// </summary>
        public bool Visible { get; }
    }
}
