namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Application model representation of a chart label.
    /// </summary>
    internal class Label
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Label"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Label"/>.</param>
        public Label(LabelDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Font = new Font(dto.Font);

            Visible = dto.Visible;
            Text = dto.Text;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Label"> class.
        /// </summary>
        public Label()
        {
            Colour = Constants.DefaultForeColour;
            Text = string.Empty;
            Font = new Font();
            Visible = true;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// Gets the label <see cref="Font"/>.
        /// </summary>
        public Font Font { get; }

        /// <summary>
        /// Gets the label text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets a flag indicating whether the label is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
