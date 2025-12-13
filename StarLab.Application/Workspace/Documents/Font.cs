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

            if (string.IsNullOrEmpty(dto.Family))
            {
                Family = Constants.DefaultFontFamily;
            }
            else
            {
                Family = dto.Family;
            }

            if (dto.Size == 0)
            {
                Size = Constants.DefaultFontSize;
            }
            else
            {
                Size = dto.Size;
            }

            Underline = dto.Underline;
            Italic = dto.Italic;
            Bold = dto.Bold;
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
