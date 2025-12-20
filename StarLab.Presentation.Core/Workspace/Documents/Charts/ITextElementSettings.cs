namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a text element while the chart is being configured.
    /// </summary>
    public interface ITextElementSettings : IFrameElementSettings
    {
        /// <summary>
        /// Gets or sets the font for the text element.
        /// </summary>
        IFont Font { get; set; }

        /// <summary>
        /// Sets the font for the text element.
        /// </summary>
        /// <param name="family">The name of the font family.</param>
        /// <param name="size">The font size.</param>
        /// <param name="bold">A flag indiciating whether the font is bold.</param>
        /// <param name="italic">A flag indiciating whether the font has the italic style applied.</param>
        /// <param name="underline">A flag indiciating whether the font is underlined.</param>
        void SetFont(string family, int size, bool bold, bool italic, bool underline);
    }
}
