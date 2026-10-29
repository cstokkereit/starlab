using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Presentation.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// Represents the current state of an overlay while the chart is being configured.
    /// </summary>
    internal class OverlaySettings : ChartElementSettings, IOverlaySettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="OverlaySettings"/> class.
        /// </summary>
        /// <param name="overlay">An overlay that specifies the initial state of this instance.</param>
        public OverlaySettings(IOverlay overlay) 
            : base(overlay)
        {
            Font = new Font(overlay.Font.Family, overlay.Font.Size, false, false, false);

            Name = overlay.Name;
            Type = overlay.Type;
        }

        /// <summary>
        /// Gets or sets the font for the overlay.
        /// </summary>
        public virtual IFont Font { get; set; }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the overlay name.
        /// </summary>
        public OverlayType Type { get; }

        /// <summary>
        /// Sets the font for the overlay.
        /// </summary>
        /// <param name="family">The name of the font family.</param>
        /// <param name="size">The font size.</param>
        /// <param name="bold">A flag indiciating whether the font is bold.</param>
        /// <param name="italic">A flag indiciating whether the font has the italic style applied.</param>
        /// <param name="underline">A flag indiciating whether the font is underlined.</param>
        public void SetFont(string family, int size, bool bold, bool italic, bool underline)
        {
            Font = new Font(family, size, bold, italic, underline);
        }
    }
}
