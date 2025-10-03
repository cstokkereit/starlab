using StarLab.Presentation.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Represents the current state of a label font while the chart is being configured.
    /// </summary>
    internal class FontSettings : IFontSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FontSettings"/> class.
        /// </summary>
        /// <param name="font">An <see cref="IFont"/> that specifies the initial state of the label font.</param>
        public FontSettings(IFont font)
        {
            Underline = font.Underline;
            Family = font.Family;
            Italic = font.Italic;
            Bold = font.Bold;
            Size = font.Size;
        }

        /// <summary>
        /// Gets or sets a flag that determines whether the fold is bold.
        /// </summary>
        public bool Bold { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the fold has the italic style applied.
        /// </summary>
        public bool Italic { get; set; }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        public string Family { get; set; }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        public int Size { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the fold is underlined.
        /// </summary>
        public bool Underline { get; set; }

        /// <summary>
        /// Applies the specified settings.
        /// </summary>
        /// <param name="family">The font family.</param>
        /// <param name="size">The size of the font.</param>
        /// <param name="bold">A flag that determines whether the fold is bold.</param>
        /// <param name="italic">A flag that determines whether the fold has the italic style applied.</param>
        /// <param name="underline">A flag that determines whether the fold is underlined.</param>
        public void SetFont(string family, int size, bool bold, bool italic, bool underline)
        {
            Underline = underline;
            Family = family;
            Italic = italic;
            Bold = bold;
            Size = size;
        }

        /// <summary>
        /// Applies the settings from the <see cref="IFontSettings"/> provided.
        /// </summary>
        /// <param name="font">An <see cref="IFontSettings"/> that specifies the new state of the font.</param>
        public void SetFont(IFontSettings font)
        {
            SetFont(font.Family, font.Size, font.Bold, font.Italic, font.Underline);
        }
    }
}
