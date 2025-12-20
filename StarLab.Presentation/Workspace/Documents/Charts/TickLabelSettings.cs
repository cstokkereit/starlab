namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the tick labels while the chart is being configured.
    /// </summary>
    internal class TickLabelSettings : TextElementSettings, ITickLabelSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabelSettings"/> class.
        /// </summary>
        /// <param name="tickLabels">An <see cref="ITickLabels"/> that specifies the initial state of the tick labels.</param>
        public TickLabelSettings(ITickLabels tickLabels)
            : base(tickLabels.Colour, tickLabels.Font, tickLabels.Visible)
        {
            Rotation = tickLabels.Rotation;
        }

        /// <summary>
        /// Gets or sets the angle of rotation for the tick labels.
        /// </summary>
        public int Rotation { get; set; }
    }
}
