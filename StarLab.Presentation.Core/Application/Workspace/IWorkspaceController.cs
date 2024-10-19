namespace StarLab.Application.Workspace
{
    public interface IWorkspaceController : IController
    {
        void CloseActiveDocument();

        void CloseWorkspace();

        void DeleteDocument(string id);

        void DeleteFolder(string path);

        void AddFolder(string path);

        void NewWorkspace();

        void OpenWorkspace();

        void RenameDocument(string id, string name);

        void RenameFolder(string key, string name);

        void SaveWorkspace();
    }
}
