namespace StarLab.Presentation.Options
{
    /// <summary>
    /// Represents the overlay settings.
    /// </summary>
    public interface IOverlaySettings
    {
        /// <summary>
        /// Gets or sets the default overlay colour.
        /// </summary>
        string DefaultColour { get; set; }

        /// <summary>
        /// Gets or sets the default overlay font family.
        /// </summary>
        string DefaultFont { get; set; }

        /// <summary>
        /// Gets or sets the default overlay font size.
        /// </summary>
        int DefaultFontSize { get; set; }
    }
}
