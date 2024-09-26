namespace StarLab.Application.Workspace
{
    public interface IWorkspaceController : IController
    {
        void CloseWorkspace();

        void NewWorkspace();

        void OpenWorkspace();

        void RenameDocument(string id, string name);

        void RenameFolder(string key, string name);

        void SaveWorkspace();
    }
}
