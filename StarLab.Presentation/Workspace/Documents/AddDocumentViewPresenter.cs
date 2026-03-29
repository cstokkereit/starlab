using log4net;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IAddDocumentView"/>.
    /// </summary>
    public class AddDocumentViewPresenter : ChildViewPresenter<IAddDocumentView, IViewController>, IAddDocumentViewPresenter, IChildViewController, IAddDocumentOutputPort
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AddDocumentViewPresenter)); // The logger that will be used for writing log messages.

        private readonly IAddDocumentUseCaseService useCases; // A facade that aggregates the available use cases.

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IAddDocumentView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="useCases">An <see cref="IAddDocumentUseCaseService"/> that will be used to execute the use cases.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public AddDocumentViewPresenter(IAddDocumentView view, ICommandManager commands, IAddDocumentUseCaseService useCases, IApplicationSettings settings, IEventAggregator events)
            : base(view, commands, settings, events) 
        {
            this.useCases = useCases ?? throw new ArgumentNullException(nameof(useCases));

            view.Attach(this);
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~AddDocumentViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Initiates the add document use case.
        /// </summary>
        public void AddDocument()
        {
            //if (InteractionContext is AddDocumentInteractionContext context)
            //{
            //    var document = new DocumentDTO
            //    {
            //        Name = View.DocumentName,
            //        Path = context.Path,
            //        View = View.DocumentType
            //    };

            //    var interactor = UseCaseFactory.CreateAddDocumentUseCase(this);

            //    interactor.Execute(Mapper.Map<WorkspaceDTO>(context.Workspace), document);
            //}
        }

        /// <summary>
        /// Closes the parent dialog without initiating the add document use case.
        /// </summary>
        public void Cancel()
        {
            //ParentController.Close();
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
            if (!Initialised)
            {
                base.Initialise(controller);

                View.AttachAddButtonCommand(GetCommand(Actions.AddDocument));
                View.AttachCancelButtonCommand(GetCommand(Actions.Cancel));

                AddImages();
                AddDocumentTypes();
            }
        }

        /// <summary>
        /// Opens the specified document.
        /// </summary>
        /// <param name="id">The document ID.</param>
        public void OpenDocument(string id)
        {
            //if (AppController.GetController(Controllers.ApplicationViewController) is IApplicationOutputPort port) port.OpenDocument(id);
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            //if (AppController.GetController(Controllers.ApplicationViewController) is IApplicationOutputPort port) port.UpdateWorkspace(dto);

            //ParentController.Close();
        }

        /// <summary>
        /// Releases any resources used by the <see cref="AddDocumentViewPresenter"/> object.
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
        /// Populates the list of available document types.
        /// </summary>
        private void AddDocumentTypes()
        {
            //view.ClearChartTypes();

            View.AddDocument("HRDiagram", "Colour-Magnitude Diagram", "HRDiagram");
            //view.AddChart("TwoColourDiagram", "Two-Colour Diagram", "HRDiagram");
            //view.AddChart("ScatterPlot", "Scatter Plot", "HRDiagram");
            //view.AddChart("BarChart", "Bar Chart", "HRDiagram");

            //view.SelectDefaultItem();
        }

        /// <summary>
        /// Adds the images that represent the available document types.
        /// </summary>
        private void AddImages()
        {
            //view.ClearImages();

            //TODO - Image should be contained within the plug-in

            View.AddImage("HRDiagram", ImageResources.HRDiagram);
        }
    }
}
