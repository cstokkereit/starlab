using log4net;
using StarLab.Application;
using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Properties;
using StarLab.Shared;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IAddDocumentView"/>.
    /// </summary>
    public class AddDocumentViewPresenter : ChildViewPresenter<IAddDocumentView, IDialogController>, IAddDocumentViewPresenter, IChildViewController, ISubscriber<WorkspaceChangedEventArgs>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AddDocumentViewPresenter)); // The logger that will be used for writing log messages.

        private readonly Dictionary<string, IDocumentDefinition> definitions = []; // A dictionary containing the available document definitions indexed by name.

        private readonly IAddDocumentUseCaseService useCaseService; // A service that executes the use cases that implement the functionality.

        private IWorkspace workspace; // The current workspace.

        private string path; // The path to the folder within the workspace hierarchy that will contain the new document.

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IAddDocumentView"/> controlled by this presenter.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public AddDocumentViewPresenter(IAddDocumentView view, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            useCaseService = services.GetService<IAddDocumentUseCaseService>();

            view.Attach(this);

            workspace = new EmptyWorkspace();
            
            path = string.Empty;

            AddImages();
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~AddDocumentViewPresenter()
        {
            Dispose(false);
        }
        
        /// <summary>
        /// Adds the selected document to the workspace.
        /// </summary>
        /// <param name="name">The name of the document.</param>
        /// <param name="definitionName">The name of the document definition.</param>
        public void AddDocument(string name, string definitionName)
        {
            ArgumentException.ThrowIfNullOrEmpty(definitionName, nameof(definitionName));
            ArgumentNullException.ThrowIfNull(name, nameof(name));

            var definition = definitions[definitionName];

            var document = new DocumentDTO
            {
                Name = name,
                Path = path,
                Type = definition.Type.ToString(),
                View = definition.View
            };

            try
            {
                useCaseService.AddDocument(workspace, document);
            }
            catch (DocumentExistsException)
            {
                AppController.ShowMessage(MessageBuilder.DocumentAlreadyExists(document.Name), InteractionType.Error, InteractionResponses.OK);
            }
            catch (InvalidNameException)
            {
                AppController.ShowMessage(MessageBuilder.DocumentNameInvalid(document.Name), InteractionType.Error, InteractionResponses.OK);
            }
            catch (Exception e)
            {
                AppController.ShowMessage(MessageBuilder.DocumentCouldNotBeCreated, InteractionType.Error, InteractionResponses.OK);

                log.Error(e.Message, e);
            }
        }

        /// <summary>
        /// Releases all resources used by the <see cref="AddDocumentViewPresenter"/> object.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public override void Initialise(IApplicationController controller)
        {
            ArgumentNullException.ThrowIfNull(controller, nameof(controller));

            if (Initialised) throw new InvalidOperationException(ExceptionMessages.PresenterAlreadyInitialised(GetType()));

            base.Initialise(controller);

            View.AttachAddButtonCommand(CreateCommand(Actions.Close, ParentController.Close));

            View.AttachCancelButtonCommand(GetCommand(Actions.Close));

            View.Initialise();

            log.Debug(LogEntries.PresenterInitialised(GetType()));
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            ArgumentNullException.ThrowIfNull(args, nameof(args));

            workspace = args.Workspace;
        }

        /// <summary>
        /// Runs the child view.
        /// </summary>
        /// <param name="args">An <see cref="INamedArguments"/> that contains information required to run the view.</param>
        public override void Run(INamedArguments args)
        {
            ArgumentNullException.ThrowIfNull(args, nameof(args));

            definitions.Clear();

            AddDocumentTypes(args.GetArgument<DocumentTypes>(Constants.Type));

            path = args.GetArgument<string>(Constants.Path);
        }

        /// <summary>
        /// Releases any resources used by the <see cref="AddDocumentViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                View.Detach();
            }
        }

        /// <summary>
        /// Populates the list of available document types.
        /// </summary>
        private void AddDocumentTypes(DocumentTypes type)
        {
            var definitions = GetDocumentDefinitions(type);

            View.ClearDocumentTypes();

            foreach(var definition in definitions)
            {
                if (type == DocumentTypes.Any || definition.Type == type)
                {
                    View.AddDocumentType(definition.Name, $"  {definition.DisplayName}", definition.Image);
                    this.definitions.Add(definition.Name, definition);
                }
            }
        }

        /// <summary>
        /// Adds the images that represent the available document types.
        /// </summary>
        private void AddImages()
        {
            View.ClearImages();

            View.AddImage("ColourColourDiagram32X32", Resources.ColourColourDiagram32X32);
            View.AddImage("ColourMagnitudeDiagram32X32", Resources.ColourMagnitudeDiagram32X32);
            View.AddImage("Table32X32", Resources.Table32X32);
        }

        /// <summary>
        /// Gets the document definitions that match the specified type.
        /// </summary>
        /// <param name="type">The type of document definitions to retrieve.</param>
        /// <returns>An <see cref="IEnumerable{IDocumentDefinition}"/> containing the matching document definitions.</returns>
        private IEnumerable<IDocumentDefinition> GetDocumentDefinitions(DocumentTypes type)
        {
            var definitions = new List<IDocumentDefinition>();

            foreach (var definition in SessionContext.Configuration.DocumentDefinitions)
            {
                if (definition.Type == type || type == DocumentTypes.Any)
                {
                    definitions.Add(definition);
                }
            }

            return definitions;
        }
    }
}
