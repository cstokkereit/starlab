namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of anm axis scale while the chart is being configured.
    /// </summary>
    internal class ScaleSettings : IScaleSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ScaleSettings"/> class.
        /// </summary>
        /// <param name="scale">An <see cref="IScale"/> that specifies the initial state of the scale.</param>
        public ScaleSettings(IScale scale)
        {
            Reversed = scale.Reversed;
            Maximum = scale.Maximum;
            Minimum = scale.Minimum;
        }

        public bool Autoscale { get; set; }

        public ITickMarkSettings MajorTickMarks { get; }

        /// <summary>
        /// Gets or sets the maimum value.
        /// </summary>
        public double Maximum { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        public double Minimum { get; set; }

        public ITickMarkSettings MinorTickMarks { get; }

        /// <summary>
        /// Gets or sets a flag indicating that the axis scale is reversed.
        /// </summary>
        public bool Reversed { get; set; }
        
        public ITickLabelSettings TickLabels { get; }

        public bool Visible { get; set; }
    }
}
