namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of a chart axis.
    /// </summary>
    internal class Axis
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Axis"/>.</param>
        public Axis(AxisDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Label = dto.Label == null ? new Label() : new Label(dto.Label);
            Scale = dto.Scale == null ? new Scale() : new Scale(dto.Scale);

            Visible = dto.Visible;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        public Axis()
        {
            Colour = Constants.DefaultForeColour;

            Label = new Label();
            Scale = new Scale();
            
            Visible = true;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// Gets the axis label.
        /// </summary>
        public Label Label { get; }

        /// <summary>
        /// Gets the axis scale.
        /// </summary>
        public Scale Scale { get; }

        /// <summary>
        /// A flag indicating whether the axis is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
