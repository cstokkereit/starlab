namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis while the chart is being configured.
    /// </summary>
    internal class AxisSettings : IAxisSettings
    {
        private string backColour; // The background colour.

        private string foreColour; // The foreground colour.

        private bool visible; // A flag that determines whether the axis is visible.

        /// <summary>
        /// Initialises a new instance of the <see cref="AxisSettings"/> class.
        /// </summary>
        /// <param name="axis">An <see cref="IAxis"/> that specifies the initial state of the axis.</param>
        public AxisSettings(IAxis axis)
        {
            Label = new LabelSettings(axis.Label);
            Scale = new ScaleSettings(axis.Scale);

            backColour = axis.BackColour;
            foreColour = axis.ForeColour;
            visible = axis.Visible;
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour 
        {
            get => backColour;

            set
            {
                Scale.MajorTickMarks.BackColour = value;
                Scale.MinorTickMarks.BackColour = value;
                Label.BackColour = value;
                backColour = value;
            }
        }

        /// <summary>
        /// Gets or sets the foreground colour.
        /// </summary>
        public string ForeColour
        {
            get => foreColour;

            set
            {
                Scale.MajorTickMarks.ForeColour = value;
                Scale.MinorTickMarks.ForeColour = value;
                Label.ForeColour = value;
                foreColour = value;
            }
        }

        /// <summary>
        /// Gets the label settings.
        /// </summary>
        public ILabelSettings Label { get; private set; }


        /// <summary>
        /// Gets the axis scale settings.
        /// </summary>
        public IScaleSettings Scale { get; private set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the axis is visible.
        /// </summary>
        public bool Visible
        {
            get => visible;

            set
            {
                Scale.MajorTickMarks.Visible = value;
                Scale.MinorTickMarks.Visible = value;
                Scale.TickLabels.Visible = value;
                Label.Visible = value;
                Scale.Visible = value;
                visible = value;
            }
        }
    }
}
