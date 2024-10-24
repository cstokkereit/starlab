namespace StarLab.Application.Help
{
    public partial class AboutView : UserControl, IAboutView
    {
        private readonly IAboutViewPresenter presenter;

        public AboutView(IPresenterFactory factory)
        {
            InitializeComponent();

            Name = Views.ABOUT;

            presenter = (IAboutViewPresenter)factory.CreatePresenter(this);
        }

        public void Initialise(IApplicationController controller, IFormController parentController)
        {
            presenter.Initialise(controller);
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
