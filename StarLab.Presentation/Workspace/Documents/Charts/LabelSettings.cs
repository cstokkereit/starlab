namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a label while the chart is being configured.
    /// </summary>
    internal class LabelSettings : ChartElementSettings, ILabelSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LabelSettings"/> class.
        /// </summary>
        /// <param name="label">An <see cref="ILabel"/> TODO.</param>
        public LabelSettings(ILabel label)
            : base(label)
        {
            Font = label.Font;
            Text = label.Text;
        }

        /// <summary>
        /// Gets or sets the font for the label.
        /// </summary>
        public virtual IFont Font { get; set; }

        /// <summary>
        /// Gets or sets the text.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Sets the font for the label.
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
