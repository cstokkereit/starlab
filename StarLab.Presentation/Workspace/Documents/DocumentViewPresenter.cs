﻿using AutoMapper;
using StarLab.Application;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDocumentView"/>.
    /// </summary>
    public sealed class DocumentViewPresenter : Presenter, IDockableViewPresenter, IDocumentController
    {
        private readonly IDocumentView view; // The view controlled by the presenter.

        private IDocument document; // The document that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDocumentView"/> controlled by this presenter.</param>
        /// <param name="document">The <see cref="IDocument"/> that the view represents.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="Configuration.IApplicationConfiguration"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public DocumentViewPresenter(IDocumentView view, IDocument document, ICommandManager commands, IUseCaseFactory factory, Configuration.IApplicationConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(commands, factory, configuration, mapper, events)
        {
            this.document = document;
            this.view = view;

            Location = Constants.Document;
        }

        /// <summary>
        /// Gets or sets the current location of the view.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Gets the name of the controller.
        /// </summary>
        public override string Name => ControllerNames.GetDocumentControllerName(document.ID);

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"> to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            view.AddToolbarButton(name, tooltip, image, command);
        }

        /// <summary>
        /// Closes the document window.
        /// </summary>
        public void Close()
        {
            view.HideOnClose = false;
            view.Close();
        }

        /// <summary>
        /// Hides the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be hidden.</param>
        public void HideSplitContent(string name)
        {
            view.HideSplitContent(name);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (!Initialised)
            {
                base.Initialise(controller);

                view.Initialise(controller);
            }
        }

        /// <summary>
        /// Shows the <see cref="IView"/> provided.
        /// </summary>
        /// <param name="view">The <see cref="IView"/> to be shown.</param>
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
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
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

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Displays a <see cref="SaveFileDialog"/> with the specified options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Shows the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be shown.</param>
        public void ShowSplitContent(string name)
        {
            view.ShowSplitContent(name);
        }

        /// <summary>
        /// Updates the <see cref="IDocument"/> that the document window represents.
        /// </summary>
        /// <param name="document">The new <see cref="IDocument"/>.</param>
        public void UpdateDocument(IDocument document)
        {
            this.document = document;

            view.Name = document.Name;
            view.Text = document.Name;
        }
    }
}
