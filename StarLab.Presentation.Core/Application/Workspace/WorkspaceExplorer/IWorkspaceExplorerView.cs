using StarLab.Commands;

namespace StarLab.Application.Workspace.WorkspaceExplorer
{
    public interface IWorkspaceExplorerView : IControlView, IFormContent<IFormController>
    {
        int AddImage(Image image);

        void AddDocumentNode(string key, string parentKey, string text, int imageIndex);

        void AddFolderNode(string key, string parentKey, string text, int imageIndex, int selectedImageIndex);

        void AddRootNode(string key, string text, int imageIndex);

        void AddToolbarButton(string name, string tooltip, Image image, ICommand command);

        void Clear();

        void CollapseNode(string key);

        IMenuManager CreateDocumentMenuManager(string document);

        IMenuManager CreateFolderMenuManager(string folder);

        IMenuManager CreateWorkspaceMenuManager();

        string DefaultLocation { get; }

        void EditNodeLabel(string key);

        void ExpandNode(string key);

        string GetSelectedNode();

        void SelectNode(string key);

        void UpdateNodeState(string key, int imageIndex, int selectedImageIndex);
    }
}
