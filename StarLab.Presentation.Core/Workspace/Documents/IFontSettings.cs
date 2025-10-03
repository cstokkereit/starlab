namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a label font while the chart is being configured.
    /// </summary>
    public interface IFontSettings
    {
        /// <summary>
        /// Gets or sets a flag that determines whether the fold is bold.
        /// </summary>
        bool Bold { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the fold has the italic style applied.
        /// </summary>
        bool Italic { get; set; }

        /// <summary>
        /// Gets or sets the font family.
        /// </summary>
        string Family { get; set; }

        /// <summary>
        /// Gets or sets the size of the font.
        /// </summary>
        int Size { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the fold is underlined.
        /// </summary>
        bool Underline { get; set; }

        /// <summary>
        /// Applies the specified settings.
        /// </summary>
        /// <param name="family">The font family.</param>
        /// <param name="size">The size of the font.</param>
        /// <param name="bold">A flag that determines whether the fold is bold.</param>
        /// <param name="italic">A flag that determines whether the fold has the italic style applied.</param>
        /// <param name="underline">A flag that determines whether the fold is underlined.</param>
        void SetFont(string family, int size, bool bold, bool italic, bool underline);

        /// <summary>
        /// Applies the settings from the <see cref="IFontSettings"/> provided.
        /// </summary>
        /// <param name="font">An <see cref="IFontSettings"/> that specifies the new state of the font.</param>
        void SetFont(IFontSettings font);
    }
}
