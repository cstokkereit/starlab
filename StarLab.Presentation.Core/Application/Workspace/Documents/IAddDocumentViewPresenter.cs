namespace StarLab.Application.Workspace.Documents
{
    public interface IAddDocumentViewPresenter : IControlViewPresenter
    {
        void AddDocument();

        void Cancel();

        void Initialise(IApplicationController controller, IDialogController parentController);
    }
}
