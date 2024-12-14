using AutoMapper;
using StarLab.Application.Configuration;
using StarLab.Commands;
using StarLab.Properties;
using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents
{
    public class AddDocumentViewPresenter : ChildViewPresenter<IAddDocumentView, IDialogController>, IAddDocumentViewPresenter, IChildViewController
    {
        private string path = string.Empty;

        private IWorkspace? workspace;

        public AddDocumentViewPresenter(IAddDocumentView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfigurationService configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public void AddDocument()
        {
            if (InteractionContext is AddDocumentInteractionContext context && AppController.GetWorkspaceController() is IWorkspaceOutputPort port)
            {
                try
                {
                    var document = new Document(View.DocumentName, context.Path, View.DocumentType);

                    var interactor = UseCaseFactory.CreateAddDocumentUseCase(port);

                    interactor.Execute(Mapper.Map<IWorkspace, WorkspaceDTO>(context.Workspace), Mapper.Map<IDocument, DocumentDTO>(document));

                    var view = AppController.GetView(document);
                    AppController.Show(view);

                    ParentController?.Close();
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void Cancel()
        {
            ParentController?.Close();
        }

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

        public override void Run(IInteractionContext context)
        {
            Debug.Assert(ParentController != null);

            base.Run(context);

            ParentController.Show();
        }

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

        private void AddImages()
        {
            //view.ClearImages();

            //TODO - Image should be contained within the plug-in

            View.AddImage("HRDiagram", Resources.HRDiagram);
        }
    }
}
