namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the axis tick marks while the chart is being configured.
    /// </summary>
    internal class TickMarkSettings : ITickMarkSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickMarkSettings"/> class.
        /// </summary>
        /// <param name="tickMarks">A <see cref="TickMarks"/> that specifies the initial state of the tick marks.</param>
        public TickMarkSettings(ITickMarks tickMarks)
        {
            BackColour = tickMarks.BackColour;
            ForeColour = tickMarks.ForeColour;
            Visible = tickMarks.Visible;
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour { get; set; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the tick marks are visible.
        /// </summary>
        public bool Visible { get; set; }
        
    }
}
