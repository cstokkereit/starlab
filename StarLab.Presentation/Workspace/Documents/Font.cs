using StarLab.Application.Workspace.Documents;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// View model represention of a font.
    /// </summary>
    internal class Font : IFont
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Font"> class.
        /// </summary>
        /// <param name="family">The name of the font family.</param>
        /// <param name="size">The font size.</param>
        /// <param name="bold">A flag indiciating whether the font is bold.</param>
        /// <param name="italic">A flag indiciating whether the font has the italic style applied.</param>
        /// <param name="underline">A flag indiciating whether the font is underlined.</param>
        public Font(string family, int size, bool bold, bool italic, bool underline)
        {
            Underline = underline;
            Family = family;
            Italic = italic;
            Bold = bold;
            Size = size;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Font"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Font"/>.</param>
        public Font(FontDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Family = string.Empty + dto.Family;
            Underline = dto.Underline;
            Italic = dto.Italic;
            Bold = dto.Bold;
            Size = dto.Size;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Font"> class.
        /// </summary>
        /// <param name="font">An <see cref="IFont"/> that specifies the initial state of the <see cref="Font"/>.</param>
        public Font(IFont font)
        {
            ArgumentNullException.ThrowIfNull(font, nameof(font));

            Underline = font.Underline;
            Family = font.Family;
            Italic = font.Italic;
            Bold = font.Bold;
            Size = font.Size;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Font"> class.
        /// </summary>
        public Font()
            : this(Constants.DefaultFontFamily, Constants.DefaultFontSize, false, false, false) { }

        /// <summary>
        /// A flag indiciating whether the <see cref="Font"> is bold.
        /// </summary>
        public bool Bold { get; }

        /// <summary>
        /// A flag indiciating whether the <see cref="Font"> has the italic style applied.
        /// </summary>
        public bool Italic { get; }

        /// <summary>
        /// Gets the name of the font family.
        /// </summary>
        public string Family { get; }

        /// <summary>
        /// Gets the size of the <see cref="Font"/>.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// A flag indiciating whether the <see cref="Font"> is underlined.
        /// </summary>
        public bool Underline { get; }
    }
}
