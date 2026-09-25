namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis scale while the chart is being configured.
    /// </summary>
    internal class ScaleSettings : ChartElementSettings, IScaleSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ScaleSettings"/> class.
        /// </summary>
        /// <param name="scale">An <see cref="IScale"/> that specifies the initial state of the scale.</param>
        public ScaleSettings(IScale scale)
            : base(scale)
        {
            MajorTickMarks = new TickMarkSettings(scale.MajorTickMarks);
            MinorTickMarks = new TickMarkSettings(scale.MinorTickMarks);
            TickLabels = new TickLabelSettings(scale.TickLabels);

            Reversed = scale.Reversed;
            Maximum = scale.Maximum;
            Minimum = scale.Minimum;
        }

        /// <summary>
        /// Gets or sets a flag that determines whether the scale is generated automatically to fit the data.
        /// </summary>
        public bool Autoscale { get; set; }

        /// <summary>
        /// Gets or sets the colour.
        /// </summary>
        public override string Colour
        {
            get
            {
                return base.Colour;
            }

            set
            {
                MajorTickMarks?.Colour = value;
                MinorTickMarks?.Colour = value;
                TickLabels?.Colour = value;

                base.Colour = value;
            }
        }

        /// <summary>
        /// Gets the settings for the major tick marks.
        /// </summary>
        public ITickMarkSettings MajorTickMarks { get; }

        /// <summary>
        /// Gets or sets the maximum value.
        /// </summary>
        public double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public double Minimum { get; set; }

        /// <summary>
        /// Gets the settings for the minor tick marks.
        /// </summary>
        public ITickMarkSettings MinorTickMarks { get; }

        /// <summary>
        /// Gets or sets a flag indicating that the axis scale is reversed.
        /// </summary>
        public bool Reversed { get; set; }

        /// <summary>
        /// Gets the tick label settings.
        /// </summary>
        public ITickLabelSettings TickLabels { get; }

        /// <summary>
        /// Gets or sets a flag that determines whether the chart element is visible.
        /// </summary>
        public override bool Visible
        {
            get => base.Visible || MajorTickMarks.Visible || MinorTickMarks.Visible || TickLabels.Visible;

            set
            {
                MajorTickMarks?.Visible = value;
                MinorTickMarks?.Visible = value;
                TickLabels?.Visible = value;

                base.Visible = value;
            }
        }
    }
}
