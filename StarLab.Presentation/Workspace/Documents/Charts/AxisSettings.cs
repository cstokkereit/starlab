namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis while the chart is being configured.
    /// </summary>
    internal class AxisSettings : IAxisSettings
    {
        private string backColour; // The background colour.

        private string foreColour; // The foreground colour.

        /// <summary>
        /// Initialises a new instance of the <see cref="AxisSettings"/> class.
        /// </summary>
        /// <param name="axis">An <see cref="IAxis"/> that specifies the initial state of the axis.</param>
        public AxisSettings(IAxis axis)
        {
            Label = new LabelSettings(axis.Label);

            backColour = axis.BackColour;
            foreColour = axis.ForeColour;

            Visible = axis.Visible;
        }

        /// <summary>
        /// Gets or sets the background colour.
        /// </summary>
        public string BackColour 
        {
            get => backColour;

            set
            {
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
                Label.ForeColour = value;
                foreColour = value;
            }
        }

        /// <summary>
        /// Gets the label settings.
        /// </summary>
        public ILabelSettings Label { get; private set; }

        /// <summary>
        /// Gets or sets a flag that determines whether the axis is visible.
        /// </summary>
        public bool Visible { get; set; }
    }
}
