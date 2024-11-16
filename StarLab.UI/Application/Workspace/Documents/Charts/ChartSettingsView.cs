using log4net;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    public partial class ChartSettingsView : UserControl, IChartSettingsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsView));

        private readonly IChartSettingsViewPresenter presenter;

        public ChartSettingsView(IPresenterFactory presenterFactory)
        {
            InitializeComponent();

            try
            {
                presenter = (IChartSettingsViewPresenter)presenterFactory.CreatePresenter(this);
            }
            catch (Exception ex)
            {
                log.Fatal(ex.Message, ex);
                throw;
            }
            
            Name = Views.CHART_SETTINGS;
        }

        public IChildViewController Controller => (IChildViewController)presenter;

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
