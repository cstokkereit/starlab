using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents
{
    public sealed class DocumentViewPresenter : Presenter, IDockableViewPresenter, IDocumentController
    {
        private readonly IDocumentView view;

        private readonly IDocument document;

        public DocumentViewPresenter(IDocumentView view, IDocument document, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(commands, useCaseFactory, configuration, mapper, events)
        {
            this.document = document;
            this.view = view;

            Location = Constants.DOCUMENT;
        }

        public string Location { get; set; }

        public override string Name => $"Document({document.ID}) Controller";


        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            view.AddToolbarButton(name, tooltip, image, command);
        }

        public void HideSplitContent(string name)
        {
            view.HideSplitContent(name);
        }

        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                AttachEventHandlers();
            }
        }

        public void Show(IView view)
        {
            throw new NotImplementedException();
        }

        public DialogResult ShowMessage(string caption, string message, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            throw new NotImplementedException();
        }

        public void ShowMessage(string caption, string message, MessageBoxIcon icon)
        {
            throw new NotImplementedException();
        }

        public string ShowOpenFileDialog(string title, string filter)
        {
            throw new NotImplementedException();
        }

        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            throw new NotImplementedException();
        }

        public void ShowSplitContent(string name)
        {
            view.ShowSplitContent(name);
        }

        private void AttachEventHandlers()
        {
            document.NameChanged += OnNameChanged;
        }

        private void OnNameChanged(object? sender, string name)
        {
            view.Name = name;
            view.Text = name;
        }
    }
}
