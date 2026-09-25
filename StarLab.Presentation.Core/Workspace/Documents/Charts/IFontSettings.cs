namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// TODO while the chart is being configured.
    /// </summary>
    public interface IFontSettings
    {
        /// <summary>
        /// Gets or sets the font.
        /// </summary>
        IFont Font { get; set; }

        /// <summary>
        /// Sets the font.
        /// </summary>
        /// <param name="family">The name of the font family.</param>
        /// <param name="size">The font size.</param>
        /// <param name="bold">A flag indiciating whether the font is bold.</param>
        /// <param name="italic">A flag indiciating whether the font has the italic style applied.</param>
        /// <param name="underline">A flag indiciating whether the font is underlined.</param>
        void SetFont(string family, int size, bool bold, bool italic, bool underline);
    }
}
