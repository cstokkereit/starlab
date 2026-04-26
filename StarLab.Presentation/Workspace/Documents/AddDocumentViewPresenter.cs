using log4net;
using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Configuration;
using StarLab.Shared;
using Stratosoft.Commands;
using System.Diagnostics;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

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

        private IWorkspace? workspace; // The current workspace.

        private string? path; // The path to the folder within the workspace hierarchy that will contain the new document.

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
            var view = definitions[definitionName].View;

            Debug.Assert(!string.IsNullOrEmpty(path));
            Debug.Assert(!string.IsNullOrEmpty(view));
            Debug.Assert(workspace != null);

            var document = new DocumentDTO
            {
                Name = name,
                Path = path,
                View = view
            };

            useCaseService.AddDocument(workspace, document);
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
            if (Initialised) throw new InvalidOperationException(string.Format(StringResources.AlreadyInitialised, nameof(AddDocumentViewPresenter)));

            base.Initialise(controller);

            View.AttachAddButtonCommand(CreateCommand(Actions.Close, () => ParentController.Close()));

            View.AttachCancelButtonCommand(GetCommand(Actions.Close));

            View.Initialise();

            log.Debug(string.Format(LogEntries.Initialised, nameof(AddDocumentViewPresenter)));
        }

        /// <summary>
        /// Event handler for the WorkspaceChangedEvent event.
        /// </summary>
        /// <param name="args">A <see cref="WorkspaceChangedEventArgs"/> that provides context for the event.</param>
        public void OnEvent(WorkspaceChangedEventArgs args)
        {
            workspace = args.Workspace;
        }

        /// <summary>
        /// Initiates the workflow managed by the presenter.
        /// </summary>
        /// <param name="context">An <see cref="IWorkflowContext"/> that contains the information required to execute the workflow.</param>
        /// <exception cref="ArgumentException"></exception>
        public override void Run(IWorkflowContext context)
        {
            if (context is AddDocumentWorkflowContext config)
            {
                definitions.Clear();

                AddDocumentTypes(config.Type);

                path = config.Path;
            }
            else
            {
                throw new ArgumentException(string.Format(StringResources.UnexpectedArgumentType, typeof(AddDocumentWorkflowContext), context.GetType()), nameof(context));
            }
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
                View.AddDocumentType(definition.Name, $"  {definition.DisplayName}", definition.Image);
                this.definitions.Add(definition.Name, definition);
            }

           
            //view.AddChart("ScatterPlot", "Scatter Plot", "HRDiagram");
            //view.AddChart("BarChart", "Bar Chart", "HRDiagram");
        }

        /// <summary>
        /// Adds the images that represent the available document types.
        /// </summary>
        private void AddImages()
        {
            View.ClearImages();

            View.AddImage("ColourColourDiagram32X32", ImageResources.ColourColourDiagram32X32);
            View.AddImage("ColourMagnitudeDiagram32X32", ImageResources.ColourMagnitudeDiagram32X32);
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
