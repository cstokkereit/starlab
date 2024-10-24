namespace StarLab.Application.Workspace
{
    public interface IWorkspaceController : IViewController
    {
        void AddFolder(string path);

        void CloseActiveDocument();

        void CloseWorkspace();

        void DeleteDocument(string id);

        void DeleteFolder(string path);

        void Exit();

        void NewWorkspace();

        void OpenWorkspace();

        void RenameDocument(string id, string name);

        void RenameFolder(string key, string name);

        void SaveWorkspace();
    }
}
