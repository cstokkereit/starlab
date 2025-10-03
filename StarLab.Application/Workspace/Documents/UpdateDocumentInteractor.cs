using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that updates the document following a change to the chart.
    /// </summary>
    internal class UpdateDocumentInteractor : UseCaseInteractor<IApplicationOutputPort>, IUseCase<WorkspaceDTO, string, ChartDTO>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public UpdateDocumentInteractor(IApplicationOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="id">The ID of the document that contains the chart.</param>
        /// <param name="dtoChart">A <see cref="ChartDTO"/> that specifies the current state of the chart.</param>
        public void Execute(WorkspaceDTO dtoWorkspace, string id, ChartDTO dtoChart)
        {
            ArgumentNullException.ThrowIfNull(nameof(dtoWorkspace));
            ArgumentNullException.ThrowIfNull(nameof(dtoChart));

            var workspace = new Workspace(dtoWorkspace);

            var document = workspace.GetDocument(id);

            document.Chart = new Chart(dtoChart);

            OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
        }
    }
}
