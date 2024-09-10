using StarLab.Commands;
using StarLab.Presentation;
using StarLab.Presentation.Charts;

namespace StarLab.UI.Charts
{
    public partial class ChartSettingsView : ControlView, IChartSettingsView, ISplitViewContent
    {
        private readonly IChartSettingsViewPresenter presenter;

        public ChartSettingsView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            presenter = (IChartSettingsViewPresenter)presenterFactory.CreatePresenter(this);
        }

        #region IChartSettingsView Members

        public override void Initialise(IApplicationController controller)
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

        #endregion

        #region ISplitViewContent Members

        public void AttachCommands(IApplicationController controller, ISplitViewController viewController)
        {
            presenter.AttachCommands(controller, viewController);
        }

        #endregion

        public void SetMinimumSize(Size size)
        {
            MinimumSize = size;
        }
    }
}
