using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspaces
{
    public interface ISaveWorkspaceUseCase
    {
        void Execute(WorkspaceDTO dto);
    }
}
