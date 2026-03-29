using log4net;
using StarLab.Application;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Shared.Properties;
using StarLab.Shared.Resources;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IDocumentView"/>.
    /// </summary>
    public sealed class DocumentViewPresenter : Presenter<IDocumentView>, IDockableViewPresenter, IDocumentController, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DocumentViewPresenter)); // The logger that will be used for writing log messages.

        private readonly List<IChildViewController> controllers = new List<IChildViewController>(); // A list containing the child controllers.

        private IDocument document; // The document that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="DocumentViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IDocumentView"/> controlled by this presenter.</param>
        /// <param name="document">The <see cref="IDocument"/> that the view represents.</param>
        /// <param name="controllers">An <see cref="IEnumerable{IChildViewController}"/> that contains the child controllers.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public DocumentViewPresenter(IDocumentView view, IDocument document, IEnumerable<IChildViewController> controllers, ICommandManager commands, IApplicationSettings settings, IEventAggregator events)
            : base(view, commands, settings, events)
        {
            this.document = document;

            ID = Controllers.GetControllerID(view);

            View.Attach(this);

            foreach (var controller in controllers)
            {
                controller.RegisterController(this);
                this.controllers.Add(controller);
            }

            Location = Constants.Document;
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~DocumentViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets an <see cref="IEnumerable{IChildViewController}"/> containing the child controllers.
        /// </summary>
        public IEnumerable<IChildViewController> ChildControllers => controllers;

        /// <summary>
        /// Gets the controller ID.
        /// </summary>
        public override string ID { get; }

        /// <summary>
        /// Gets or sets the current location of the view.
        /// </summary>
        public string Location { get; set; }

        /// <summary>
        /// Adds a button to the tool bar.
        /// </summary>
        /// <param name="name">The name of the button.</param>
        /// <param name="tooltip">The tooltip text.</param>
        /// <param name="image">The <see cref="Image"> to use for the button.</param>
        /// <param name="command">The <see cref="ICommand"> to invoke when the button is clicked.</param>
        public void AddToolbarButton(string name, string tooltip, Image image, ICommand command)
        {
            View.AddToolbarButton(name, tooltip, image, command);
        }

        /// <summary>
        /// Closes the document window.
        /// </summary>
        public void Close()
        {
            View.HideOnClose = false;
            View.Close();
        }

        /// <summary>
        /// Releases all resources used by the <see cref="DocumentViewPresenter"/> object.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets the specified controller.
        /// </summary>
        /// <typeparam name="TController">The type of the required controller.</typeparam>
        /// <returns>The specified controller.</returns>
        public TController GetController<TController>()
        {
            foreach (var controller in controllers)
            {
                if (controller is TController required) return required;
            }

            throw new Exception(string.Format(Resources.UnknownType, typeof(TController)));
        }

        /// <summary>
        /// Hides the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be hidden.</param>
        public void HideSplitContent(string name)
        {
            View.HideSplitContent(name);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            if (Initialised) throw new InvalidOperationException(string.Format(Resources.AlreadyInitialised, nameof(DocumentViewPresenter)));

            base.Initialise(controller);

            foreach (var childController in controllers)
            {
                childController.Initialise(controller);
            }

            UpdateChildControllers();

            log.Debug(string.Format(LogEntries.Initialised, $"{nameof(DocumentViewPresenter)}({ID})"));
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            UpdateDocument(args.Workspace.GetDocument(document.ID));
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
        /// Displays a <see cref="MessageBox"/> with the specified caption, message, message type and available responses.
        /// </summary>
        /// <param name="caption">The message box caption.</param>
        /// <param name="message">The message text.</param>
        /// <param name="type">An <see cref="InteractionType"/> that specifies the type of message being displayed.</param>
        /// <param name="responses">An <see cref="InteractionResponses"/> that specifies the available responses.</param>
        /// <returns>An <see cref="InteractionResult"/> that identifies the chosen response.</returns>
        public InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses)
        {
            return View.ShowMessage(caption, message, type, responses);
        }

        /// <summary>
        /// Displays an <see cref="OpenFileDialog"/> with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowOpenFileDialog(string title, string filter)
        {
            return View.ShowOpenFileDialog(title, filter);
        }

        /// <summary>
        /// Displays a save file dialog with the specified owner and options.
        /// </summary>
        /// <param name="title">The dialog title.</param>
        /// <param name="filter">The file name filter.</param>
        /// <param name="extension">The default file extension.</param>
        /// <returns>The filename selected in the dialog.</returns>
        public string ShowSaveFileDialog(string title, string filter, string extension)
        {
            return View.ShowSaveFileDialog(title, filter, extension);
        }

        /// <summary>
        /// Shows the specified split content.
        /// </summary>
        /// <param name="name">The name of the content to be shown.</param>
        public void ShowSplitContent(string name)
        {
            View.ShowSplitContent(name);
        }

        /// <summary>
        /// Updates the <see cref="IDocument"/> that the document window represents.
        /// </summary>
        /// <param name="document">The new <see cref="IDocument"/>.</param>
        public void UpdateDocument(IDocument document)
        {
            UpdateChildControllers();

            View.SetName(document.Name);

            this.document = document;
        }

        /// <summary>
        /// Notifies the presenter that the view has been activated.
        /// </summary>
        public void ViewActivated()
        {
            Events.Publish(new ActiveViewChangedEventArgs(View));
        }

        /// <summary>
        /// Releases all resources used by the <see cref="DocumentViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                View.Detach();
            }
        }

        /// <summary>
        /// Updates the chart controllers.
        /// </summary>
        /// <param name="document">An <see cref="IChartDocument"/> that contains the chart configuration.</param>
        private void UpdateChartControllers(IChartDocument document)
        {
            var chartController = GetController<IChartController>();
            chartController.UpdateChart(document.Chart);

            var settingsController = GetController<IChartSettingsController>();
            settingsController.UpdateSettings(document);
        }

        /// <summary>
        /// Updates the child controllers.
        /// </summary>
        private void UpdateChildControllers()
        {
            if (document is IChartDocument chartDocument)
            {
                UpdateChartControllers(chartDocument);
            }
        }
    }
}
