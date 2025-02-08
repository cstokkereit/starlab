using AutoMapper;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A use case that removes a document from the workspace hierarchy.
    /// </summary>
    internal class DeleteDocumentInteractor : WorkspaceInteractor, IDeleteItemUseCase
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="DeleteDocumentInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
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
            var workspace = new Workspace(dto);

            workspace.DeleteDocument(key);

            OutputPort.DeleteDocuments(GetDocumentDTOs(dto, [key]));
            UpdateProjects(workspace, dto.Projects);
            OutputPort.UpdateWorkspace(dto);
        }
    }
}
