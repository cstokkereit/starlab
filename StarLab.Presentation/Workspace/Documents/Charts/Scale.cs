using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart axis scale.
    /// </summary>
    internal class Scale : IScale
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Scale"/>.</param>
        public Scale(ScaleDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            MajorTickMarks = new TickMarks(dto.MajorTickMarks);
            MinorTickMarks = new TickMarks(dto.MinorTickMarks);
            TickLabels = new TickLabels(dto.TickLabels);

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            Autoscale = dto.Autoscale;
            Reversed = dto.Reversed;
            Maximum = dto.Maximum;
            Minimum = dto.Minimum;
            Visible = dto.Visible;
        }

        /// <summary>
        /// A flag indicating that the scale is generated automatically to fit the data.
        /// </summary>
        public bool Autoscale { get; }

        /// <summary>
        /// Gets the background colour.
        /// </summary>
        public string BackColour { get; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour { get; }

        /// <summary>
        /// Gets the major tick marks.
        /// </summary>
        public ITickMarks MajorTickMarks { get; }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        public double Maximum { get; }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public double Minimum { get; }

        /// <summary>
        /// Gets the minor tick marks.
        /// </summary>
        public ITickMarks MinorTickMarks { get; }

        /// <summary>
        /// A flag indicating that the axis scale is reversed.
        /// </summary>
        public bool Reversed { get; }

        /// <summary>
        /// Gets the tick labels.
        /// </summary>
        public ITickLabels TickLabels { get; }

        /// <summary>
        /// A flag indicating whether the scale is visible.
        /// </summary>
        public bool Visible { get; }
    }
}
