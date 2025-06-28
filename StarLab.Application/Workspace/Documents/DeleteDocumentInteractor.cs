using AutoMapper;
using log4net;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that removes a document from the workspace hierarchy.
    /// </summary>
    internal class DeleteDocumentInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IDeleteItemUseCase
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
        /// <param name="dtoWorkspace">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        /// <param name="key">The key that identifies the document being removed.</param>
        public void Execute(WorkspaceDTO dto, string key)
        {
            dto.ActiveDocument = string.Empty;

            try
            {
                var workspace = new Workspace(dto);

                var document = workspace.GetDocument(key);

                if (ConfirmAction(string.Format(Resources.DeletionWarning, document.Name)))
                {
                    workspace.DeleteDocument(key);

                    OutputPort.UpdateWorkspace(Mapper.Map<WorkspaceDTO>(workspace));
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message, e);
            }
        }
    }
}
