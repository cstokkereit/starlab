namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Application model representation of the chart data points.
    /// </summary>
    internal class Points
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Points"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Points"/>.</param>
        public Points(PointsDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Visible = dto.Visible;

            Size = dto.Size;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Points"> class.
        /// </summary
        public Points()
        {
            Colour = Constants.DefaultForeColour;

            Visible = true;

            Size = 1;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// Gets the size.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// A flag indicating whether the data points are visible.
        /// </summary>
        public bool Visible { get; }
    }
}
