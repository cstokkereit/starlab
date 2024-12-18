using AutoMapper;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// 
    /// </summary>
    internal class AddDocumentInteractor : WorkspaceInteractor, IAddDocumentUseCase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="outputPort"></param>
        /// <param name="mapper"></param>
        public AddDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dtoWorkspace"></param>
        /// <param name="dtoDocument"></param>
        public void Execute(WorkspaceDTO dtoWorkspace, DocumentDTO dtoDocument)
        {
            var workspace = new Workspace(dtoWorkspace);
            var document = new Document(dtoDocument);
            workspace.AddDocument(document);

            UpdateWorkspace(workspace, dtoWorkspace.Projects);

            OutputPort.UpdateWorkspace(dtoWorkspace);
            OutputPort.OpenDocument(document.ID);
        }
    }
}
