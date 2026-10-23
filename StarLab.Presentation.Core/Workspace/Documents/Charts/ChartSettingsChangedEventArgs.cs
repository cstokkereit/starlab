namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// Provides context for the ChartSettingsChanged event.
    /// </summary>
    public class ChartSettingsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsChangedEventArgs"/> class.
        /// </summary>
        /// <param name="id">The ID of the document that contains the chart.</param>
        /// <param name="chart">The modified chart.</param>
        public ChartSettingsChangedEventArgs(DocumentID id, IChart chart)
        {
            Chart = chart;
            ID = id;
        }

        /// <summary>
        /// Gets the modified chart.
        /// </summary>
        public IChart Chart { get; }

        /// <summary>
        /// Gets the ID of the document that contains the chart.
        /// </summary>
        public DocumentID ID { get; }
    }
}
