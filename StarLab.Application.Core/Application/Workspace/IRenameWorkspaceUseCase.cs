namespace StarLab.Application.Workspace
{
    public interface IRenameWorkspaceUseCase
    {
        void Execute(WorkspaceDTO dto, string name);
    }
}
