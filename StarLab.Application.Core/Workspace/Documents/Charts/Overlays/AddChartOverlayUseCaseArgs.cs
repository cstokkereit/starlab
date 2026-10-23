namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the AddChartOverlayUseCase.
    /// </summary>
    public readonly struct AddChartOverlayUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddChartOverlayUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="chart">A <see cref="ChartDTO"/> that holds the details of the chart.</param>
        /// <param name="overlay">The overlay name.</param>
        /// <param name="font">The default font.</param>
        /// <param name="fontSize">The default font size.</param>
        /// <param name="colour">The default colour.</param>
        public AddChartOverlayUseCaseArgs(ChartDTO chart, string overlay, string font, int fontSize, string colour)
        {
            FontSize = fontSize;
            Overlay = overlay;
            Colour = colour;
            Chart = chart;
            Font = font;
        }

        public readonly ChartDTO Chart;

        public readonly string Colour;

        public readonly string Font;

        public readonly int FontSize;

        public readonly string Overlay;

        
    }
}
