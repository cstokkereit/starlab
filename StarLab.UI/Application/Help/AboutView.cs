using log4net;
using StarLab.Application.Configuration;
using System.Windows.Forms;

namespace StarLab.Application.Help
{
    /// <summary>
    /// 
    /// </summary>
    public partial class AboutView : UserControl, IAboutView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AboutView));

        private readonly IAboutViewPresenter presenter;

        private readonly SplitViewPanels panel;

        public AboutView(IContentConfiguration config, IViewConfiguration parent, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.ABOUT;

            panel = (SplitViewPanels)config.Panel;

            presenter = (IAboutViewPresenter)factory.CreatePresenter(parent, this);
        }

        public IChildViewController Controller => (IChildViewController)presenter;

        public SplitViewPanels Panel => panel;

        public void AttachPresenter(IPresenter presenter)
        {
            throw new NotImplementedException();
        }

        public void SetCompanyName(string companyName)
        {
            throw new NotImplementedException();
        }

        public void SetCopyright(string copyright)
        {
            throw new NotImplementedException();
        }

        public void SetDescription(string description)
        {
            throw new NotImplementedException();
        }

        public void SetLogo(Image image)
        {
            throw new NotImplementedException();
        }

        public void SetProductName(string productName)
        {
            throw new NotImplementedException();
        }

        public void SetVersion(string version)
        {
            throw new NotImplementedException();
        }
    }
}
