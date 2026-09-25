namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of an axis while the chart is being configured.
    /// </summary>
    internal class AxisSettings : ChartElementSettings, IAxisSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AxisSettings"/> class.
        /// </summary>
        /// <param name="axis">An <see cref="IAxis"/> that specifies the initial state of the axis settings.</param>
        public AxisSettings(IAxis axis)
            : base(axis)
        {
            Label = new LabelSettings(axis.Label);
            Scale = new ScaleSettings(axis.Scale);
        }

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
                Label?.Colour = value;
                Scale?.Colour = value;

                base.Colour = value;
            }
        }

        /// <summary>
        /// Gets the label settings.
        /// </summary>
        public ILabelSettings Label { get; }

        /// <summary>
        /// Gets the axis scale settings.
        /// </summary>
        public IScaleSettings Scale { get; }

        /// <summary>
        /// Gets or sets a flag that determines whether the axis is visible.
        /// </summary>
        public override bool Visible
        {
            get => base.Visible;

            set
            {
                Label?.Visible = value;
                Scale?.Visible = value;

                base.Visible = value;
            }
        }
    }
}
