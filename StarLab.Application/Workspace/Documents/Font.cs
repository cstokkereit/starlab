namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Domain model represention of a font.
    /// </summary>
    internal class Font
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Font"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Font"/>.</param>
        public Font(FontDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Family = string.IsNullOrEmpty(dto.Family) ? Constants.DefaultFontFamily : dto.Family;
            Size = dto.Size == 0 ? Constants.DefaultFontSize : dto.Size;
            Underline = dto.Underline;
            Italic = dto.Italic;
            Bold = dto.Bold;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Font"> class.
        /// </summary>
        public Font()
        {
            Family = Constants.DefaultFontFamily;
            Size = Constants.DefaultFontSize;
            Underline = false;
            Italic = false;
            Bold = false;
        }

        /// <summary>
        /// Gets a flag indiciating whether the <see cref="Font"> is bold.
        /// </summary>
        public bool Bold { get; }

        /// <summary>
        /// Gets a flag indiciating whether the <see cref="Font"> has the italic style applied.
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
        /// Gets a flag indiciating whether the <see cref="Font"> is underlined.
        /// </summary>
        public bool Underline { get; }
    }
}
