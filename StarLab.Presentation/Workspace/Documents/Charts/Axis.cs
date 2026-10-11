using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart axis.
    /// </summary>
    internal class Axis : ChartElement, IAxis
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        /// <param name="font">An <see cref="IFont"/> that specifies the axis label font.</param>
        /// <param name="dto">A <see cref="AxisDTO"/> that specifies the initial state of the axis.</param>
        public Axis(IFont font, AxisDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Label = dto.Label == null ? new Label(Colour, new Font(font)) : new Label(dto.Label);

            Scale = new Scale(dto.Scale);

            Data = dto.Data;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the axis.</param>
        /// <param name="font">An <see cref="IFont"/> that specifies the axis label font.</param>
        /// <param name="visible">Specifies whether the axis is visble or not.</param>
        public Axis(string colour, IFont font, bool visible)
            : base(colour, visible)
        {
            Label = new Label(Colour, font);

            Scale = new Scale(Colour, font);

            Data = string.Empty;
        }

        /// <summary>
        /// Gets the name of the data field the axis represents.
        /// </summary>
        public string Data { get; }

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
