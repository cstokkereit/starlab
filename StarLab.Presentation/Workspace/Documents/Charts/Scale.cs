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
            Autoscale = dto.Autoscale;
            Reversed = dto.Reversed;
            Maximum = dto.Maximum;
            Minimum = dto.Minimum;
        }

        public bool Autoscale { get; }

        public ITickMarks MajorTickMarks { get; }

        /// <summary>
        /// Gets the maimum value.
        /// </summary>
        public double Maximum { get; }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public double Minimum { get; }

        public ITickMarks MinorTickMarks { get; }

        /// <summary>
        /// A flag indicating that the axis scale is reversed.
        /// </summary>
        public bool Reversed { get; }

        public ITickLabels TickLabels { get; }
    }
}
