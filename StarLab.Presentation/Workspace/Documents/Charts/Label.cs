using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of a chart label.
    /// </summary>
    internal class Label : TextElement, ILabel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Label"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Label"/>.</param>
        public Label(LabelDTO dto)
            : base(dto.Colour, dto.Font == null ? null : new Font(dto.Font), dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Text = string.Empty + dto.Text;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Label"> class.
        /// </summary>
        public Label()
            : base(Constants.DefaultForeColour, new Font(), true)
        {
            Text = string.Empty;
        }

        /// <summary>
        /// Gets the label text.
        /// </summary>
        public string Text { get; }
    }
}
