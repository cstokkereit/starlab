namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the axis tick marks while the chart is being configured.
    /// </summary>
    internal class TickMarkSettings : FrameElementSettings, ITickMarkSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarkSettings"/> class.
        /// </summary>
        /// <param name="tickMarks">A <see cref="TickMarks"/> that specifies the initial state of the tick marks.</param>
        public TickMarkSettings(ITickMarks tickMarks)
            : base(tickMarks.Colour, tickMarks.Visible)
        {
            Length = tickMarks.Length;
        }

        /// <summary>
        /// Gets or sets the length of the tickamrks.
        /// </summary>
        public int Length { get; set; }
    }
}
