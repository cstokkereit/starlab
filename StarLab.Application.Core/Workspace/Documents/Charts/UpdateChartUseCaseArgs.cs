namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the UpdateChartUseCase.
    /// </summary>
    public readonly struct UpdateChartUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="UpdateChartUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="document">Thew document ID.</param>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        /// <param name="database">The database name.</param>
        public UpdateChartUseCaseArgs(string document, string host, int port, string database)
        {
            DatabaseName = database;
            DocumentID = document;
            Host = host;
            Port = port;
        }

        public readonly string DatabaseName;

        public readonly string DocumentID;

        public readonly string Host;

        public readonly int Port;
    }
}
