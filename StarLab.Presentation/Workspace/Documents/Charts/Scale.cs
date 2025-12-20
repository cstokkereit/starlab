using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart axis scale.
    /// </summary>
    internal class Scale : FrameElement, IScale
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Scale"/>.</param>
        public Scale(ScaleDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            MajorTickMarks = dto.MajorTickMarks == null ? new TickMarks(Constants.DefaultMajorTickLength) : new TickMarks(dto.MajorTickMarks);
            MinorTickMarks = dto.MinorTickMarks == null ? new TickMarks(Constants.DefaultMinorTickLength) : new TickMarks(dto.MinorTickMarks);
            TickLabels = dto.TickLabels == null ? new TickLabels() : new TickLabels(dto.TickLabels);

            Autoscale = dto.Autoscale;
            Reversed = dto.Reversed;
            Maximum = dto.Maximum;
            Minimum = dto.Minimum;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// </summary>
        public Scale()
            : base(Constants.DefaultForeColour, true)
        {
            MajorTickMarks = new TickMarks(Constants.DefaultMajorTickLength);
            MinorTickMarks = new TickMarks(Constants.DefaultMinorTickLength);
            TickLabels = new TickLabels();

            Autoscale = true;
            Reversed = false;
        }

        /// <summary>
        /// A flag indicating that the scale is generated automatically to fit the data.
        /// </summary>
        public bool Autoscale { get; }

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
    }
}
