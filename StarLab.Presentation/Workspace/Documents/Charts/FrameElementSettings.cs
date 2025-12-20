namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a frame element while the chart is being configured.
    /// </summary>
    internal abstract class FrameElementSettings : ChartElementSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FrameElementSettings"/> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the frame element.</param>
        /// <param name="visible">A flag indicating whether the frame element is visible.</param>
        public FrameElementSettings(string colour, bool visible)
            : base(visible)
        {
            Colour = colour;
        }

        /// <summary>
        /// Gets or sets the colour of the frame element.
        /// </summary>
        public virtual string Colour { get; set; }
    }
}
