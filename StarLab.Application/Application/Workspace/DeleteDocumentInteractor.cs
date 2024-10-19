using AutoMapper;
using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    internal class DeleteDocumentInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IDeleteItemUseCase
    {
        public DeleteDocumentInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string key)
        {
            var workspace = new Model.Workspace(dto);

            workspace.DeleteDocument(key);

            Mapper.Map(workspace.Documents, dto.Documents);
            Mapper.Map(workspace.Folders, dto.Folders);

            OutputPort.UpdateWorkspace(dto);
        }
    }
}
