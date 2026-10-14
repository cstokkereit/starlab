namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A POCO that provides all of the information required to execute the UpdateChartUseCaseArgs.
    /// </summary>
    public readonly struct UpdateChartUseCaseArgs
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="RenameDocumentUseCaseArgs"/> struct.
        /// </summary>
        /// <param name="workspace">A <see cref="WorkspaceDTO"/> that holds the details of the workspace.</param>
        /// <param name="document">The document ID.</param>
        /// <param name="chart">A <see cref="ChartDTO"/> that holds the details of the chart.</param>
        public UpdateChartUseCaseArgs(WorkspaceDTO workspace, string document, ChartDTO chart)
        {
            Workspace = workspace;
            DocumentID = document;
            Chart = chart;
        }

        public readonly ChartDTO Chart;

        public readonly string DocumentID;

        public readonly WorkspaceDTO Workspace;
    }
}
