using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    public interface IWorkspaceOutputPort : IOutputPort
    {
        void UpdateDocument(DocumentDTO dto);

        void UpdateWorkspace(WorkspaceDTO dto);
    }
}
