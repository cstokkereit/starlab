using AutoMapper;
using StarLab.Commands;
using StarLab.Properties;
using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents
{
    public class AddDocumentViewPresenter : ChildViewPresenter<IAddDocumentView, IDialogController>, IAddDocumentViewPresenter, IChildViewController
    {
        public AddDocumentViewPresenter(IAddDocumentView view, ICommandManager commands, IUseCaseFactory useCaseFactory, IConfiguration configuration, IMapper mapper, IEventAggregator events)
            : base(view, commands, useCaseFactory, configuration, mapper, events) { }

        public void AddDocument()
        {
            var name = View.DocumentName;

            var ws = AppController.GetWorkspaceController();

            DocumentBuilder builder = new DocumentBuilder();

            //builder.CreateDocument()

            // TODO - Interactor

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

        public void Cancel()
        {
            ParentController?.Close();
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
