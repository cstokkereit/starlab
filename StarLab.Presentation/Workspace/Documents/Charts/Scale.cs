using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// View model representation of a chart axis scale.
    /// </summary>
    internal class Scale : ChartElement, IScale
    {
        private const int DEFAULT_LENGTH_MAJOR = 4;
        private const int DEFAULT_LENGTH_MINOR = 2;

        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// </summary>
        /// <param name="dto">A <see cref="ScaleDTO"/> that specifies the initial state of the scale.</param>
        public Scale(ScaleDTO dto)
            : base(dto.Colour, dto.Visible)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            MajorTickMarks = dto.MajorTickMarks == null ? new TickMarks(Colour, 4, true) : new TickMarks(dto.MajorTickMarks);
            MinorTickMarks = dto.MinorTickMarks == null ? new TickMarks(Colour, 2, false) : new TickMarks(dto.MinorTickMarks);
            TickLabels = new TickLabels(dto.TickLabels);

            Autoscale = dto.Autoscale;
            Reversed = dto.Reversed;
            Maximum = dto.Maximum;
            Minimum = dto.Minimum;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// <param name="colour">A <see cref="string"/> value that specifies the colour of the scale.</param>
        /// <param name="font">An <see cref="IFont"/> that specifies the tick label font.</param>
        /// </summary>
        public Scale(string colour, IFont font)
            : base(colour, true)
        {
            MajorTickMarks = new TickMarks(Colour, DEFAULT_LENGTH_MAJOR, true);
            MinorTickMarks = new TickMarks(Colour, DEFAULT_LENGTH_MINOR, false);
            TickLabels = new TickLabels(Colour, font);

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
