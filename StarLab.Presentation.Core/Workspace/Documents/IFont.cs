namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Represents a font.
    /// </summary>
    public interface IFont
    {
        /// <summary>
        /// Gets a flag indiciating whether the <see cref="Font"> is bold.
        /// </summary>
        bool Bold { get; }

        /// <summary>
        /// Gets a flag indiciating whether the <see cref="Font"> has the italic style applied.
        /// </summary>
        bool Italic { get; }

        /// <summary>
        /// Gets the name of the font family.
        /// </summary>
        string Family { get; }

        /// <summary>
        /// Gets the size of the <see cref="Font"/>.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Gets a flag indiciating whether the <see cref="Font"> is underlined.
        /// </summary>
        bool Underline { get; }
    }
}
