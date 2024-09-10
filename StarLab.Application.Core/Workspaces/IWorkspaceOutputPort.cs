using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspaces
{
    public interface IWorkspaceOutputPort
    {
        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
