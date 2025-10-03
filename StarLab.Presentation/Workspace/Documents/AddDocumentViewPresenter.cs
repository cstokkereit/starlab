using AutoMapper;
using log4net;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using Stratosoft.Commands;

using ImageResources = StarLab.Presentation.Properties.Resources;
using StringResources = StarLab.Shared.Properties.Resources;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IAddDocumentView"/>.
    /// </summary>
    public class AddDocumentViewPresenter : ChildViewPresenter<IAddDocumentView, IDialogController>, IAddDocumentViewPresenter, IChildViewController, IAddDocumentOutputPort
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AddDocumentViewPresenter)); // The logger that will be used for writing log messages.

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The <see cref="IAddDocumentView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="settings">An <see cref="IApplicationSettings"/> that provides access to the application configuration.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public AddDocumentViewPresenter(IAddDocumentView view, ICommandManager commands, IUseCaseFactory factory, IApplicationSettings settings, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, settings, mapper, events) 
        {
            log.Debug(string.Format(StringResources.InstanceCreated, nameof(AddDocumentViewPresenter)));
        }

        /// <summary>
        /// Initiates the add document use case.
        /// </summary>
        public void AddDocument()
        {
            if (InteractionContext is AddDocumentInteractionContext context)
            {
                var document = new DocumentDTO
                {
                    Name = View.DocumentName,
                    Path = context.Path,
                    View = View.DocumentType
                };

                var interactor = UseCaseFactory.CreateAddDocumentUseCase(this);

                interactor.Execute(Mapper.Map<WorkspaceDTO>(context.Workspace), document);
            }
        }

        /// <summary>
        /// Closes the parent dialog without initiating the add document use case.
        /// </summary>
        public void Cancel()
        {
            ParentController.Close();
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
            if (AppController.GetController(Controllers.ApplicationViewController) is IApplicationOutputPort port) port.OpenDocument(id);
        }

        /// <summary>
        /// Runs the controller as part of a use case.
        /// </summary>
        /// <param name="context"></param> TODO - This may not be necessary
        public override void Run(IInteractionContext context)
        {
            base.Run(context);

            View.DocumentName = string.Empty;

            ParentController.Show();
        }

        /// <summary>
        /// Updates the state of the workspace represented by the <see cref="WorkspaceDTO"/> provided.
        /// </summary>
        /// <param name="dto">The <see cref="WorkspaceDTO"/> that contains the updated workspace state.</param>
        public void UpdateWorkspace(WorkspaceDTO dto)
        {
            if (AppController.GetController(Controllers.ApplicationViewController) is IApplicationOutputPort port) port.UpdateWorkspace(dto);

            ParentController.Close();
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
