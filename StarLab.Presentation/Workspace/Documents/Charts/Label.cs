using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of a chart label.
    /// </summary>
    internal class Label : ILabel
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Label"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Label"/>.</param>
        public Label(LabelDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;
            Text = string.Empty + dto.Text;
            Font = new Font(dto.Font);
            Visible = dto.Visible;
        }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the label font.
        /// </summary>
        public IFont Font { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the label text.
        /// </summary>
        public string Text { get; }

        /// <summary>
        /// Gets a flag indicating whether the label is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
