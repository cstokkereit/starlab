using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that updates the document following a change to the chart.
    /// </summary>
    internal class UpdateDocumentInteractor : UseCaseInteractor<IApplicationOutputPort>, IUseCase<UpdateDocumentUseCaseArgs>
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
        /// <param name="args">The <see cref="UpdateDocumentUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(UpdateDocumentUseCaseArgs args)
        {
            var workspace = new Workspace(args.Workspace);

            var document = workspace.GetDocument(new DocumentID(args.DocumentID));

            document.Chart = new Chart(args.Chart);

            OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
        }
    }
}
