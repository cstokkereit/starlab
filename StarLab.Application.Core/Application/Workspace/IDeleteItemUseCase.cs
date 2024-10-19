using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    public interface IDeleteItemUseCase
    {
        void Execute(WorkspaceDTO dto, string key);
    }
}
