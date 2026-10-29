using StarLab.Application.Workspace.Documents;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// View model represention of a font.
    /// </summary>
    internal class Font : IFont, IEquatable<Font>
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
        /// <param name="dto">A data transfer object that specifies the initial state of this font.</param>
        public Font(FontDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Underline = dto.Underline;
            Family = dto.Family;
            Italic = dto.Italic;
            Bold = dto.Bold;
            Size = dto.Size;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Font"> class.
        /// </summary>
        /// <param name="font">A font that specifies the initial state of this font.</param>
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
        /// A flag indiciating whether the font is bold.
        /// </summary>
        public bool Bold { get; }

        /// <summary>
        /// A flag indiciating whether the font has the italic style applied.
        /// </summary>
        public bool Italic { get; }

        /// <summary>
        /// Gets the name of the font family.
        /// </summary>
        public string Family { get; }

        /// <summary>
        /// Gets the size of the font.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// A flag indiciating whether the font is underlined.
        /// </summary>
        public bool Underline { get; }

        /// <summary>
        /// Indicates whether the current instance is equal to the object provided.
        /// </summary>
        /// <param name="obj">An object to compare with this instance.</param>
        /// <returns><c>true</c> if the current instance is equal to the object supplied; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj) => Equals(obj as Font);

        /// <summary>
        /// Indicates whether the current instance is equal to the object of the same type provided.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns><c>true</c> if the current instance is equal to the object supplied; <c>false</c> otherwise.</returns>
        public bool Equals(Font? other)
        {
            if (other is null) return false;
            
            if (ReferenceEquals(this, other)) return true;

            return other.Family == Family && other.Size == Size && other.Bold == Bold && other.Italic == Italic && other.Underline == Underline;
        }

        /// <summary>
        /// Generates a hash code for the current instance.
        /// </summary>
        /// <returns>A hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Family, Size, Bold, Italic, Underline);
        }
    }
}
