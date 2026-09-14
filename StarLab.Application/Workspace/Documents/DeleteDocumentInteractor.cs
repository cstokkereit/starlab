using AutoMapper;
using log4net;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that removes a document from the workspace hierarchy.
    /// </summary>
    internal class DeleteDocumentInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IUseCase<DeleteDocumentUseCaseArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DeleteDocumentInteractor)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="DeleteDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public DeleteDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="RenameWorkspaceUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(DeleteDocumentUseCaseArgs args)
        {
            args.Workspace.ActiveDocument = string.Empty;

            var workspace = new Workspace(args.Workspace);

            var id = new DocumentID(args.DocumentID);

            var document = workspace.GetDocument(id);

            workspace.DeleteDocument(id);

            OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
        }
    }
}
