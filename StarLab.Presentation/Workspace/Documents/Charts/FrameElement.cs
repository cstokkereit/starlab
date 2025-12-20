namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a visual element that is part of the chart frame.
    /// </summary>
    internal abstract class FrameElement : ChartElement
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FrameElement"> class.
        /// </summary>
        /// <param name="colour">Specifies the colour of this chart element.</param>
        /// <param name="visible">Specifies whether this chart element is visble or not.</param>
        public FrameElement(string? colour, bool visible)
            : base(visible)
        {
            Colour = string.IsNullOrEmpty(colour) ? Constants.DefaultForeColour : colour;
        }

        /// <summary>
        /// Gets the colour of the frame element.
        /// </summary>
        public string Colour { get; }
    }
}
