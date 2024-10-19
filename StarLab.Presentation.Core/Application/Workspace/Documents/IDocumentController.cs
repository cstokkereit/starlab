namespace StarLab.Application.Workspace.Documents
{
    public interface IDocumentController : IController, IDialogController, IToolbarManager
    {
        void HideSplitContent(string name);

        void ShowSplitContent(string name);
    }
}
