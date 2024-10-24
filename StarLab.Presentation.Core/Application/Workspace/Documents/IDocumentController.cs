namespace StarLab.Application.Workspace.Documents
{
    public interface IDocumentController : IViewController, IToolbarManager
    {
        void HideSplitContent(string name);

        void ShowSplitContent(string name);
    }
}
