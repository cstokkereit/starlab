using log4net;
using StarLab.Application.Configuration;
using StarLab.Commands;

namespace StarLab.Application.Workspace.Documents.Charts
{
    public partial class ChartSettingsView : UserControl, IChartSettingsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsView));

        private readonly IChartSettingsViewPresenter presenter;

        private readonly SplitViewPanels panel;

        public ChartSettingsView(IContentConfiguration config, IViewConfiguration parent, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.CHART_SETTINGS;

            panel = (SplitViewPanels)config.Panel;

            presenter = (IChartSettingsViewPresenter)factory.CreatePresenter(parent, this);
        }

        public IChildViewController Controller => (IChildViewController)presenter;

        public SplitViewPanels Panel => panel;

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

        public void Initialise(IApplicationController controller)
        {
            // Do Nothing
        }

        public void SetMinimumSize(Size size)
        {
            MinimumSize = size;
        }
    }
}
