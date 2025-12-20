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

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Visible = dto.Visible;
            Length = dto.Length;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarks"> class.
        /// </summary>
        /// <param name="length">The default length.</param>
        public TickMarks(int length)
        {
            Colour = Constants.DefaultForeColour;
            Visible = true;
            Length = length;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

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
