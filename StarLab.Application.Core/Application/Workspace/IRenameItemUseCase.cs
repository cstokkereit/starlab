using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    public interface IRenameItemUseCase
    {
        void Execute(WorkspaceDTO dto, string key, string name);
    }
}
