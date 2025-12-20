namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a text element that is part of a chart.
    /// </summary>
    internal abstract class TextElementSettings : FrameElementSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TextElementSettings"/> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the text element.</param>
        /// <param name="font">An <see cref="IFont"/> that specifies the text element font.</param>
        /// <param name="visible">A flag indicating whether the text element is visible.</param>
        public TextElementSettings(string colour, IFont font, bool visible)
            : base(colour, visible)
        {
            ArgumentNullException.ThrowIfNull(font, nameof(font));

            Font = font;
        }

        /// <summary>
        /// Gets or sets the font for the text element.
        /// </summary>
        public virtual IFont Font { get; set; }

        /// <summary>
        /// Sets the font for the text element.
        /// </summary>
        /// <param name="family">The name of the font family.</param>
        /// <param name="size">The font size.</param>
        /// <param name="bold">A flag indiciating whether the font is bold.</param>
        /// <param name="italic">A flag indiciating whether the font has the italic style applied.</param>
        /// <param name="underline">A flag indiciating whether the font is underlined.</param>
        public void SetFont(string family, int size, bool bold, bool italic, bool underline)
        {
            Font = new Font(family, size, bold, italic, underline);
        }
    }
}
