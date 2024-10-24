using AutoMapper;

namespace StarLab.Application.Workspace
{
    internal class DeleteFolderInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IDeleteItemUseCase
    {
        public DeleteFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string key)
        {
            var workspace = new Workspace(dto);

            workspace.DeleteFolder(key);

            Mapper.Map(workspace.Documents, dto.Documents);
            Mapper.Map(workspace.Folders, dto.Folders);

            // TODO If folder tree contains documents further down - will need to UpdateWorkspace

            OutputPort.UpdateFolders(dto);
        }
    }
}
