using AutoMapper;

namespace StarLab.Application.Workspace.Documents
{
    internal class AddDocumentInteractor : WorkspaceInteractor, IAddDocumentUseCase
    {
        public AddDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dtoWorkspace, DocumentDTO dtoDocument)
        {
            var workspace = new Workspace(dtoWorkspace);
            var document = new Document(dtoDocument);
            workspace.AddDocument(document);

            UpdateWorkspace(workspace, dtoWorkspace.Projects);

            OutputPort.UpdateWorkspace(dtoWorkspace);
        }
    }
}
