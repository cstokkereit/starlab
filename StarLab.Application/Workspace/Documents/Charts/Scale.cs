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

            MajorTickMarks = dto.MajorTickMarks == null ? new TickMarks(Constants.DefaultMajorTickMarkLength) : new TickMarks(dto.MajorTickMarks);
            MinorTickMarks = dto.MinorTickMarks == null ? new TickMarks(Constants.DefaultMinorTickMarkLength) : new TickMarks(dto.MinorTickMarks);
            TickLabels = dto.TickLabels == null ? new TickLabels() : new TickLabels(dto.TickLabels);

            Colour = string.IsNullOrEmpty(dto.Colour) ? Constants.DefaultForeColour : dto.Colour;

            Autoscale = dto.Autoscale;
            Reversed = dto.Reversed;
            Maximum = dto.Maximum;
            Minimum = dto.Minimum;
            Visible = dto.Visible;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Scale"> class.
        /// </summary>
        public Scale()
        {
            MajorTickMarks = new TickMarks(Constants.DefaultMajorTickMarkLength);
            MinorTickMarks = new TickMarks(Constants.DefaultMinorTickMarkLength);
            TickLabels =  new TickLabels();

            Colour = Constants.DefaultForeColour;

            Autoscale = true;
            Reversed = false;
            Visible = true;
        }

        /// <summary>
        /// A flag indicating that the scale is generated automatically to fit the data.
        /// </summary>
        public bool Autoscale { get; }

        /// <summary>
        /// Gets the colour.
        /// </summary>
        public string Colour { get; }

        /// <summary>
        /// Gets the major tick marks.
        /// </summary>
        public TickMarks MajorTickMarks { get; }

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
        public TickMarks MinorTickMarks { get; }

        /// <summary>
        /// A flag indicating that the axis scale is reversed.
        /// </summary>
        public bool Reversed { get; }

        /// <summary>
        /// Gets the tick labels.
        /// </summary>
        public TickLabels TickLabels { get; }

        /// <summary>
        /// A flag indicating whether the scale is visible.
        /// </summary>
        public bool Visible { get; }
     }
}
