namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the tick labels while the chart is being configured.
    /// </summary>
    internal class TickLabelSettings : ChartElementSettings, ITickLabelSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabelSettings"/> class.
        /// </summary>
        /// <param name="tickLabels">An <see cref="ITickLabels"/> that specifies the initial state of the tick label settings.</param>
        public TickLabelSettings(ITickLabels tickLabels)
            : base(tickLabels)
        {
            Rotation = tickLabels.Rotation;
            Font = tickLabels.Font;
        }

        /// <summary>
        /// Gets or sets the font for the tick labels.
        /// </summary>
        public virtual IFont Font { get; set; }

        /// <summary>
        /// Gets or sets the angle of rotation for the tick labels.
        /// </summary>
        public int Rotation { get; set; }

        /// <summary>
        /// Sets the font for the tick labels.
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
