using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart axis.
    /// </summary>
    internal class Axis : FrameElement, IAxis
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Axis"/>.</param>
        public Axis(AxisDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Label = dto.Label == null ? new Label() : new Label(dto.Label);
            Scale = dto.Scale == null ? new Scale() : new Scale(dto.Scale);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        public Axis()
            : base(Constants.DefaultForeColour, true)
        {
            Label = new Label();
            Scale = new Scale();
        }

        /// <summary>
        /// Gets the axis <see cref="ILabel"/>.
        /// </summary>
        public ILabel Label { get; }

        /// <summary>
        /// Gets the axis <see cref="IScale"/>.
        /// </summary>
        public IScale Scale { get; }
    }
}
