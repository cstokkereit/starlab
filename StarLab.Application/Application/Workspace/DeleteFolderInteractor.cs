using AutoMapper;

namespace StarLab.Application.Workspace
{
    internal class DeleteFolderInteractor : WorkspaceInteractor, IDeleteItemUseCase
    {
        public DeleteFolderInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        public void Execute(WorkspaceDTO dto, string key)
        {
            var workspace = new Workspace(dto);

            workspace.DeleteFolder(key);
            UpdateWorkspace(workspace, dto.Projects);
            OutputPort.UpdateFolders(dto);
        }
    }
}
