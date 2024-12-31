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

                view.Initialise(controller);
            }
        }

        public void Show(IView view)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the button that was clicked.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionResponses responses)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays a <see cref="MessageBox"/> with the specified options.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message)
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
