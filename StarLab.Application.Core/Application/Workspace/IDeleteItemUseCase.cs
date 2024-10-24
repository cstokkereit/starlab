namespace StarLab.Application.Workspace
{
    public interface IDeleteItemUseCase
    {
        void Execute(WorkspaceDTO dto, string key);
    }
}
