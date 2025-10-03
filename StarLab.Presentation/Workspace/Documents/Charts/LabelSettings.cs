namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a label while the chart is being configured.
    /// </summary>
    internal class LabelSettings : ILabelSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LabelSettings"/> class.
        /// </summary>
        /// <param name="label">An <see cref="ILabel"/> that specifies the initial state of the label.</param>
        public LabelSettings(ILabel label)
        {
            BackColour = label.BackColour;
            ForeColour = label.ForeColour;
            Font = new FontSettings(label.Font);
            Visible = label.Visible;
            Text = label.Text;
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour { get; set; }

        /// <summary>
        /// Gets the font settings.
        /// </summary>
        public IFontSettings Font { get; private set; }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour { get; set; }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the axis is visible. 
        /// </summary>
        public bool Visible { get; set; }
    }
}
