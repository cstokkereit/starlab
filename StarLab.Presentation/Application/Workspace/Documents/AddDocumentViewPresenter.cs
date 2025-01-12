using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using StarLab.Properties;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// Controls the behaviour of an <see cref="IAddDocumentView"/>.
    /// </summary>
    public class AddDocumentViewPresenter : ChildViewPresenter<IAddDocumentView, IDialogController>, IAddDocumentViewPresenter, IChildViewController
    {
        private string path = string.Empty; // The path to the workspace folder.

        private IWorkspace? workspace; // TODO - needed?

        /// <summary>
        /// Initialises a new instance of the <see cref="AddDocumentViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="IAddDocumentView"/> controlled by this presenter.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="configuration">The <see cref="IConfigurationService"/> that will be used to get configuration information.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public AddDocumentViewPresenter(IAddDocumentView view, ICommandManager commands, IUseCaseFactory factory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, factory, configuration, mapper, events) { }

        /// <summary>
        /// Initiates the add document use case.
        /// </summary>
        public void AddDocument()
        {
            if (InteractionContext is AddDocumentInteractionContext context && AppController.GetWorkspaceController() is IWorkspaceOutputPort port)
            {
                try
                {
                    var document = new Document(View.DocumentName, context.Path, View.DocumentType);

                    var interactor = UseCaseFactory.CreateAddDocumentUseCase(port);

                    interactor.Execute(Mapper.Map<IWorkspace, WorkspaceDTO>(context.Workspace), Mapper.Map<IDocument, DocumentDTO>(document));

                    ParentController.Close();
                }
                catch (Exception ex)
                {
                    // TODO
                }
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

                View.AttachAddButtonCommand(GetCommand(Actions.ADD_DOCUMENT));
                View.AttachCancelButtonCommand(GetCommand(Actions.CANCEL));

                AddImages();
                AddDocumentTypes();
            }
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
        /// Populates the list of available document types.
        /// </summary>
        private void AddDocumentTypes()
        {
            //view.ClearChartTypes();

            //TODO - Should be configured or discovered -> plugin

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

            View.AddImage("HRDiagram", Resources.HRDiagram);
        }
    }
}
