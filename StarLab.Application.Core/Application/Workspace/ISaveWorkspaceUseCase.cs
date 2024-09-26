using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    public interface ISaveWorkspaceUseCase
    {
        void Execute(WorkspaceDTO dto);
    }
}
