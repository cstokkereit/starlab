using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of the chart axis scale tick marks.
    /// </summary>
    public class TickMarks
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarks"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="TickMarks"/>.</param>
        public TickMarks(TickMarksDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.BackColour != null);
            Debug.Assert(dto.ForeColour != null);

            BackColour = dto.BackColour;
            ForeColour = dto.ForeColour;
            Visible = dto.Visible;
            Length = dto.Length;
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
        /// Gets the length of the tickamrks.
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// A flag indicating whether the tickmarks are visible.
        /// </summary>
        public bool Visible { get; }
    }
}
