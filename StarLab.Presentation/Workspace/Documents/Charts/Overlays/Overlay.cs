using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// View model representation of a chart overlay.
    /// </summary>
    internal class Overlay : ChartElement, IOverlay
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Overlay"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the overlay.</param>
        public Overlay(OverlayDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            Font = new Font(dto.Font);

            Name = dto.Name;
        }

        /// <summary>
        /// Gets the overlay font.
        /// </summary>
        public IFont Font { get; }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        public string Name { get; }
    }
}
