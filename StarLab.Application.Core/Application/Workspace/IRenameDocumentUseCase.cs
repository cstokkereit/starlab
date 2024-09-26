using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    public interface IRenameDocumentUseCase
    {
        void Execute(WorkspaceDTO dto, string id, string name);
    }
}
