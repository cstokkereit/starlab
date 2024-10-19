namespace StarLab.Application.Workspace.Documents
{
    public interface IDocumentView : IDockableView, IToolbarManager
    {
        void HideSplitContent(string name);

        void ShowSplitContent(string name);
    }
}
