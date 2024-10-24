using AutoMapper;

namespace StarLab.Application.Workspace.Documents
{
    internal class DeleteDocumentInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IDeleteItemUseCase
    {
        public DeleteDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string key)
        {
            var workspace = new Workspace(dto);

            workspace.DeleteDocument(key);

            Mapper.Map(workspace.Documents, dto.Documents);
            Mapper.Map(workspace.Folders, dto.Folders);

            OutputPort.UpdateWorkspace(dto);
        }
    }
}
