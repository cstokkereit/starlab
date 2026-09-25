using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents a label that is part of a chart.
    /// </summary>
    internal class Label : ChartElement, ILabel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Label"> class.
        /// </summary>
        /// <param name="dto">A <see cref="LabelDTO"/> that specifies the initial state of the <see cref="Label"/>.</param>
        public Label(LabelDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            Font = new Font(dto.Font);

            Text = dto.Text;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Label"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the label.</param>
        /// <param name="font">An <see cref="IFont"/> that specifies the label font.</param>
        public Label(string colour, IFont font)
            : base(colour, false) 
        {
            Text = string.Empty;

            Font = font;
        }

        /// <summary>
        /// Gets the label font.
        /// </summary>
        public IFont Font { get; }

        /// <summary>
        /// Gets the label text.
        /// </summary>
        public string Text { get; }
    }
}
