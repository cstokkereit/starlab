namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of a label while the chart is being configured.
    /// </summary>
    internal class LabelSettings : TextElementSettings, ILabelSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="LabelSettings"/> class.
        /// </summary>
        /// <param name="label">An <see cref="ILabel"/> that specifies the initial state of the label.</param>
        public LabelSettings(ILabel label)
            : base(label.Colour, label.Font, label.Visible)
        {
            Visible = label.Visible;
            Text = label.Text;
        }

        /// <summary>
        /// Gets or sets the label text.
        /// </summary>
        public string Text { get; set; }
    }
}
