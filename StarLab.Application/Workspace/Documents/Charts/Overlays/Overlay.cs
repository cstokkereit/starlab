namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Application model representation of a chart overlay.
    /// </summary>
    internal class Overlay
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Overlay"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the overlay.</param>
        public Overlay(OverlayDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Font = new Font(dto.Font);

            Visible = dto.Visible;
            Name = dto.Name;
        }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// Gets the overlay font.
        /// </summary>
        public Font Font { get; }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// A flag indicating whether the overlay is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
