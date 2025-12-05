namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// TODO
    /// </summary>
    internal class TickLabelSettings : ITickLabelSettings
    {
        /// <summary>
        /// TODO
        /// </summary>
        /// <param name="tickLabels"></param>
        public TickLabelSettings(ITickLabels tickLabels) 
        {
            Font = new FontSettings(tickLabels.Font);

            BackColour = tickLabels.BackColour;
            ForeColour = tickLabels.ForeColour;
            Rotation = tickLabels.Rotation;
            Visible = tickLabels.Visible;
        }

        public string BackColour { get; set; }

        public IFontSettings Font { get; private set; }

        public string ForeColour { get; set; }

        public int Rotation { get; set; }

        public bool Visible { get; set; }
    }
}
