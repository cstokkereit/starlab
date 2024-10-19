using StarLab.Presentation;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    public partial class ChartSettingsView : UserControl, IChartSettingsView
    {
        private readonly IChartSettingsViewPresenter presenter;

        public ChartSettingsView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = (IChartSettingsViewPresenter)presenterFactory.CreatePresenter(this);

            Name = Views.CHART_SETTINGS;
        }

        public void Initialise(IApplicationController controller, IDocumentController parentController)
        {
            presenter.Initialise(controller, parentController);
        }

        public void Initialise(IApplicationController controller)
        {
            presenter.Initialise(controller);
        }

        public void AttachCancelButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonCancel);
            }
        }

        public void AttachOKButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonOK);
            }
        }

        public void SetMinimumSize(Size size)
        {
            MinimumSize = size;
        }
    }
}
