using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart axis.
    /// </summary>
    internal class Axis : IAxis
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Axis"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Axis"/>.</param>
        public Axis(AxisDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;
            Label = new Label(dto.Label);
            Scale = new Scale(dto.Scale);
            Visible = dto.Visible;
        }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the axis <see cref="ILabel"/>.
        /// </summary>
        public ILabel Label { get; }

        /// <summary>
        /// Gets the axis <see cref="IScale"/>.
        /// </summary>
        public IScale Scale { get; }

        /// <summary>
        /// A flag indicating that the axis is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
