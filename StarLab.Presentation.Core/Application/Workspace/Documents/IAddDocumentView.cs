using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    public interface IAddDocumentView : IControlView, IFormContent<IDialogController>
    {
        string DocumentName { get; set; }

        string DocumentType { get; }

        void AddDocument(string key, string text, string imageKey);

        void AddImage(string key, Image image);

        void AttachAddButtonCommand(ICommand command);

        void AttachCancelButtonCommand(ICommand command);
    }
}
