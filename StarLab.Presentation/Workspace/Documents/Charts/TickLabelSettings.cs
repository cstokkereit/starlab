namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the tick labels while the chart is being configured.
    /// </summary>
    internal class TickLabelSettings : ITickLabelSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TickLabelSettings"/> class.
        /// </summary>
        /// <param name="tickLabels">An <see cref="ITickLabels"/> that specifies the initial state of the tick labels.</param>
        public TickLabelSettings(ITickLabels tickLabels) 
        {
            Font = new FontSettings(tickLabels.Font);

            BackColour = tickLabels.BackColour;
            ForeColour = tickLabels.ForeColour;
            Rotation = tickLabels.Rotation;
            Visible = tickLabels.Visible;
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour { get; set; }

        /// <summary>
        /// Gets the font settings.
        /// </summary>
        public IFontSettings Font { get; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour { get; set; }

        /// <summary>
        /// Gets or sets the angle of rotation for the tick labels.
        /// </summary>
        public int Rotation { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the tick labels are visible.
        /// </summary>
        public bool Visible { get; set; }
    }
}
