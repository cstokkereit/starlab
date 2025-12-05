namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// Domain model representation of a chart axis scale.
    /// </summary>
    internal class Scale
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// </summary>
        /// <param name="dto">A data transfer object that specifies the initial state of the <see cref="Scale"/>.</param>
        public Scale(ScaleDTO dto)
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

        /// <summary>
        /// 
        /// </summary>
        public bool Autoscale { get; }

        /// <summary>
        /// 
        /// </summary>
        public TickMarks MajorTickMarks { get; }

        /// <summary>
        /// Gets the maimum value.
        /// </summary>
        public double Maximum { get; }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public double Minimum { get; }

        /// <summary>
        /// 
        /// </summary>
        public TickMarks MinorTickMarks { get; }

        /// <summary>
        /// A flag indicating that the axis scale is reversed.
        /// </summary>
        public bool Reversed { get; }

        /// <summary>
        /// 
        /// </summary>
        public TickLabels TickLabels { get; }
     }
}
