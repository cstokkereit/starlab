namespace StarLab.Application.Workspace.Documents
{
    public interface IAddDocumentViewPresenter : IChildViewPresenter
    {
        void AddDocument();

        void Cancel();
    }
}
