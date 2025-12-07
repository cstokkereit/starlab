namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis scale while the chart is being configured.
    /// </summary>
    internal class ScaleSettings : IScaleSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ScaleSettings"/> class.
        /// </summary>
        /// <param name="scale">An <see cref="IScale"/> that specifies the initial state of the scale.</param>
        public ScaleSettings(IScale scale)
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
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour
        {
            get
            {
                return GetColour(x => x.BackColour);
            }

            set
            {
                MajorTickMarks.BackColour = value;
                MinorTickMarks.BackColour = value;
                TickLabels.BackColour = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour
        {
            get
            {
                return GetColour(x => x.ForeColour);
            }

            set
            {
                MajorTickMarks.ForeColour = value;
                MinorTickMarks.ForeColour = value;
                TickLabels.ForeColour = value;
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
        public bool Visible
        {
            get => MajorTickMarks.Visible || MinorTickMarks.Visible || TickLabels.Visible;

            set
            {
                MajorTickMarks.Visible = value;
                MinorTickMarks.Visible = value;
                TickLabels.Visible = value;
            }
        }

        /// <summary>
        /// Gets the colour applied to the greatest number of scale elements.
        /// </summary>
        /// <param name="Colour">A function that determines which colour setting is used.</param>
        /// <returns>A <see cref="string"/> that specifies the colour applicable to the greatest number of scale elements.</returns>
        private string GetColour(Func<IColourSettings, string> Colour)
        {
            var colours = new List<string>();

            if (MajorTickMarks.Visible) colours.Add(Colour(MajorTickMarks));
            if (MinorTickMarks.Visible) colours.Add(Colour(MinorTickMarks));
            if (TickLabels.Visible) colours.Add(Colour(TickLabels));

            return colours.Count == 0 ? string.Empty : colours.GroupBy(a => a).OrderByDescending(b => b.Count()).First().Key;
        }
    }
}
