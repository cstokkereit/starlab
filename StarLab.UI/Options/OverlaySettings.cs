using StarLab.Presentation.Options;

namespace StarLab.UI.Options
{
    /// <summary>
    /// Provides read/write access to the overlay settings.
    /// </summary>
    internal class OverlaySettings : IOverlaySettings
    {
        /// <summary>
        /// Gets or sets the default overlay colour.
        /// </summary>
        public string DefaultColour
        {
            get => Properties.Settings.Default.DefaultOverlayColour;
            set => Properties.Settings.Default.DefaultOverlayColour = value;
        }

        /// <summary>
        /// Gets or sets the default overlay font family.
        /// </summary>
        public string DefaultFont
        {
            get => Properties.Settings.Default.DefaultOverlayFont;
            set => Properties.Settings.Default.DefaultOverlayFont = value;
        }

        /// <summary>
        /// Gets or sets the default overlay font size.
        /// </summary>
        public int DefaultFontSize
        {
            get => Properties.Settings.Default.DefaultOverlayFontSize;
            set => Properties.Settings.Default.DefaultOverlayFontSize = value;
        }
    }
}
