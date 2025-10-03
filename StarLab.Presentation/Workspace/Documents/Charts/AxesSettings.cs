namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Represents the current state of the chart axes while the chart is being configured.
    /// </summary>
    internal class AxesSettings : IAxesSettings
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AxesSettings"> class.
        /// </summary>
        /// <param name="x1">An <see cref="IAxis"/> that specifies the initial state of the bottom axis.</param>
        /// <param name="x2">An <see cref="IAxis"/> that specifies the initial state of the top axis.</param>
        /// <param name="y1">An <see cref="IAxis"/> that specifies the initial state of the left axis.</param>
        /// <param name="y2">An <see cref="IAxis"/> that specifies the initial state of the right axis.</param>
        public AxesSettings(IAxis x1, IAxis x2, IAxis y1, IAxis y2)
        {
            X1 = new AxisSettings(x1);
            X2 = new AxisSettings(x2);
            Y1 = new AxisSettings(y1);
            Y2 = new AxisSettings(y2);

            Font = GetFont();
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
        /// Gets the background colour.
        /// </summary>
        public string BackColour
        {
            get
            {
                return GetColour(x => x.BackColour);
            }

            set
            {
                X1.BackColour = value;
                X2.BackColour = value;
                Y1.BackColour = value;
                Y2.BackColour = value;
            }
        }

        /// <summary>
        /// Gets the font settings.
        /// </summary>
        public IFontSettings Font { get; set; }

        /// <summary>
        /// Gets the foreground colour.
        /// </summary>
        public string ForeColour
        {
            get
            {
                return GetColour(x => x.ForeColour);
            }

            set
            {
                X1.ForeColour = value;
                X2.ForeColour = value;
                Y1.ForeColour = value;
                Y2.ForeColour = value;
            }
        }

        /// <summary>
        /// Gets or sets a flag that determines whether the axes are visible.
        /// </summary>
        public bool Visible
        {
            get => X1.Visible || X2.Visible || Y1.Visible || Y2.Visible;

            set
            {
                X1.Visible = value;
                X2.Visible = value;
                Y1.Visible = value;
                Y2.Visible = value;
            }
        }

        /// <summary>
        /// Gets the colour applied to the greatest number of axes.
        /// </summary>
        /// <param name="Colour">A function that determines which colour setting is used.</param>
        /// <returns>A <see cref="string"/> that specifies the colour applicable to the greatest number of axes.</returns>
        private string GetColour(Func<IAxisSettings, string> Colour)
        {
            var colours = new List<string>();

            if (X1.Visible) colours.Add(Colour(X1));
            if (X2.Visible) colours.Add(Colour(X2));
            if (Y1.Visible) colours.Add(Colour(Y1));
            if (Y2.Visible) colours.Add(Colour(Y2));

            return colours.GroupBy(a => a).OrderByDescending(b => b.Count()).First().Key;
        }

        /// <summary>
        /// Gets the font applied to the greatest number of axis labels.
        /// </summary>
        /// <returns>An <see cref="IFontSettings"/> applicable to the greatest number of axis labels.</returns>
        private IFontSettings GetFont()
        {
            var fonts = new List<IFontSettings>();

            if (X1.Visible) fonts.Add(X1.Label.Font);
            if (X2.Visible) fonts.Add(X2.Label.Font);
            if (Y1.Visible) fonts.Add(Y1.Label.Font);
            if (Y2.Visible) fonts.Add(Y2.Label.Font);

            var family = fonts.Count > 0 ? fonts.GroupBy(a => a.Family).OrderByDescending(b => b.Count()).First().Key : string.Empty;

            var settings = X1.Label.Font;

            foreach (var font in fonts)
            {
                if (font.Family == family)
                {
                    settings = font;
                    break;
                }
            }

            return settings;
        }
    }
}
