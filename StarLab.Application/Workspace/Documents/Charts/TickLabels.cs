using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of the chart axis scale tick labels.
    /// </summary>
    internal class TickLabels
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabels"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="TickLabels"/>.</param>
        public TickLabels(TickLabelsDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.BackColour != null);
            Debug.Assert(dto.ForeColour != null);
            Debug.Assert(dto.Font != null);

            BackColour = dto.BackColour;
            ForeColour = dto.ForeColour;
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
        public Font Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the angle of rotation for the tick labels.
        /// </summary>
        public int Rotation { get; }

        /// <summary>
        /// A flag indicating whether the tickmarks are visible.
        /// </summary>
        public bool Visible { get; }
    }
}
