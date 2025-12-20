namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the chart axes while the chart is being configured.
    /// </summary>
    internal class AxesSettings : TextElementSettings, IAxesSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AxesSettings"> class.
        /// </summary>
        /// <param name="x1">An <see cref="IAxis"/> that specifies the initial state of the bottom axis.</param>
        /// <param name="x2">An <see cref="IAxis"/> that specifies the initial state of the top axis.</param>
        /// <param name="y1">An <see cref="IAxis"/> that specifies the initial state of the left axis.</param>
        /// <param name="y2">An <see cref="IAxis"/> that specifies the initial state of the right axis.</param>
        public AxesSettings(IAxis x1, IAxis x2, IAxis y1, IAxis y2)
            : base(GetColour(x1, x2, y1, y2), GetFont(x1, x2, y1, y2), GetVisible(x1, x2, y1, y2))
        {
            X1 = new AxisSettings(x1);
            X2 = new AxisSettings(x2);
            Y1 = new AxisSettings(y1);
            Y2 = new AxisSettings(y2);
        }

        /// <summary>
        /// Gets the settings for the bottom axis.
        /// </summary>
        public IAxisSettings X1 { get; }

        /// <summary>
        /// Gets the settings for the top axis.
        /// </summary>
        public IAxisSettings X2 { get; }

        /// <summary>
        /// Gets the settings for the left axis.
        /// </summary>
        public IAxisSettings Y1 { get; }

        /// <summary>
        /// Gets the settings for the right axis.
        /// </summary>
        public IAxisSettings Y2 { get; }

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
                if (X1 != null) X1.Colour = value;
                if (X2 != null) X2.Colour = value;
                if (Y1 != null) Y1.Colour = value;
                if (Y2 != null) Y2.Colour = value;

                base.Colour = value;
            }
        }

        /// <summary>
        /// Gets or sets the font for the axes.
        /// </summary>
        public override IFont Font
        { 
            get
            {
                return base.Font;
            }
            
            set
            {
                if (X1 != null) X1.Label.Font = value;
                if (X2 != null) X2.Label.Font = value;
                if (Y1 != null) Y1.Label.Font = value;
                if (Y2 != null) Y2.Label.Font = value;

                base.Font = value;
            }
        }

        /// <summary>
        /// Gets or sets a flag that determines whether the axes are visible.
        /// </summary>
        public override bool Visible
        {
            get => base.Visible;

            set
            {
                if (X1 != null) X1.Visible = value;
                if (X2 != null) X2.Visible = value;
                if (Y1 != null) Y1.Visible = value;
                if (Y2 != null) Y2.Visible = value;

                base.Visible = value;
            }
        }

        /// <summary>
        /// Gets the colour applied to the greatest number of axes.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private static string GetColour(IAxis x1, IAxis x2, IAxis y1, IAxis y2)
        {
            var colours = new List<string>();

            if (x1.Visible) colours.Add(x1.Colour);
            if (x2.Visible) colours.Add(x2.Colour);
            if (y1.Visible) colours.Add(y1.Colour);
            if (y2.Visible) colours.Add(y2.Colour);

            return colours.Count == 0 ? string.Empty : colours.GroupBy(a => a).OrderByDescending(b => b.Count()).First().Key;
        }

        /// <summary>
        /// Gets the font applied to the greatest number of axis labels.
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private static IFont GetFont(IAxis x1, IAxis x2, IAxis y1, IAxis y2)
        {
            var fonts = new List<IFont>();

            if (x1.Visible) fonts.Add(x1.Label.Font);
            if (x2.Visible) fonts.Add(x2.Label.Font);
            if (y1.Visible) fonts.Add(y1.Label.Font);
            if (y2.Visible) fonts.Add(y2.Label.Font);

            return fonts.Count > 0 ? fonts[0] : new Font(); // TODO - Temporary
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="x2"></param>
        /// <param name="y1"></param>
        /// <param name="y2"></param>
        /// <returns></returns>
        private static bool GetVisible(IAxis x1, IAxis x2, IAxis y1, IAxis y2)
        {
            return x1.Visible || x2.Visible || y1.Visible || y2.Visible;
        }
    }
}
