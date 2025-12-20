namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a text element that is part of a chart.
    /// </summary>
    internal abstract class TextElement : FrameElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TextElement"> class.
        /// </summary>
        /// <param name="colour">Specifies the colour of this chart element.</param>
        /// <param name="font">Specifies the <see cref="IFont"/> to use for this chart element.</param>
        /// <param name="visible">Specifies whether this chart element is visble or not.</param>
        public TextElement(string? colour, IFont? font, bool visible)
            : base(colour, visible)
        {
            ArgumentNullException.ThrowIfNull(font, nameof(font));

            Font = font;
        }

        /// <summary>
        /// Gets the font for the text element.
        /// </summary>
        public IFont Font { get; }
    }
}
