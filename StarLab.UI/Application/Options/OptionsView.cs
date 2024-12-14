using log4net;
using StarLab.Application.Configuration;

namespace StarLab.Application.Options
{
    public partial class OptionsView : UserControl, IOptionsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionsView));

        private readonly IOptionsViewPresenter presenter;

        private readonly SplitViewPanels panel;

        public OptionsView(IContentConfiguration config, IViewConfiguration parent, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.OPTIONS;

            panel = (SplitViewPanels)config.Panel;

            presenter = (IOptionsViewPresenter)factory.CreatePresenter(parent, this);
        }

        public IChildViewController Controller => (IChildViewController)presenter;

        public SplitViewPanels Panel => panel;
    }
}
