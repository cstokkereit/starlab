namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the PlotChartUseCase.
    /// </summary>
    public readonly struct PlotChartUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="PlotChartUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="chart">A <see cref="ChartDTO"/> that holds the details of the chart.</param>
        /// <param name="document">Thew document ID.</param>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        /// <param name="database">The database name.</param>
        public PlotChartUseCaseArgs(ChartDTO chart, string document, string host, int port, string database)
        {
            DatabaseName = database;
            DocumentID = document;
            Chart = chart;
            Host = host;
            Port = port;
        }

        public readonly ChartDTO Chart;

        public readonly string DatabaseName;

        public readonly string DocumentID;

        public readonly string Host;

        public readonly int Port;
    }
}
