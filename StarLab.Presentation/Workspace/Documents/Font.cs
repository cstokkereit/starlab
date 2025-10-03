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
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Font"/>.</param>
        public Font(FontDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Family = string.Empty + dto.Family;
            Underline = dto.Underline;
            Italic = dto.Italic;
            Bold = dto.Bold;
            Size = dto.Size;
        }

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
