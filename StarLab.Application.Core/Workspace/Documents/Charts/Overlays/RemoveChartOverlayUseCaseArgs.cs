namespace StarLab.Application.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the RemoveChartOverlayUseCase.
    /// </summary>
    public readonly struct RemoveChartOverlayUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RemoveChartOverlayUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="chart">A <see cref="ChartDTO"/> that holds the details of the chart.</param>
        /// <param name="overlay">The overlay name.</param>
        public RemoveChartOverlayUseCaseArgs(ChartDTO chart, string overlay)
        {
            Overlay = overlay;
            Chart = chart;
        }

        public readonly ChartDTO Chart;

        public readonly string Overlay;
    }
        
}
