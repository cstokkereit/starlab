using StarLab.Commands;

namespace StarLab.Presentation.Workspaces.WorkspaceExplorer
{
    public interface IWorkspaceExplorerView : IControlView
    {
        void AddImage(string key, Image image);

        void AddDocumentNode(string key, string parentKey, string text, string imageKey);

        void AddFolderNode(string key, string parentKey, string text, string imageKey);

        void AddRootNode(string key, string text, string imageKey);

        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);

        void Clear();

        void Collapse(string key);

        IMenuManager CreateDocumentMenuManager();

        IMenuManager CreateFolderMenuManager();

        IMenuManager CreateWorkspaceMenuManager();

        void EditNodeLabel(string key);

        void Expand(string key);

        string GetSelectedNode();
    }
}
