//using StarLab.Presentation;

//namespace StarLab.Application.Help
//{
//    public partial class OldAboutView : View, IAboutView
//    {
//        private readonly IAboutViewPresenter presenter;

//        public OldAboutView(IPresenterFactory presenterFactory)
//        {
//            InitializeComponent();

//            presenter = (IAboutViewPresenter)presenterFactory.CreatePresenter(this);
//        }

//        public IViewController Controller => presenter;

//        public override void Initialise(IApplicationController controller)
//        {
//            base.Initialise(controller);

//            presenter.Initialise(controller);
//        }

//        public void SetCompanyName(string companyName)
//        {
//            labelCompanyName.Text = companyName;
//        }

//        public void SetCopyright(string copyright)
//        {
//            labelCopyright.Text = copyright;
//        }

//        public void SetDescription(string description)
//        {
//            textDescription.Text = description;
//        }

//        public void SetLogo(Image image)
//        {
//            pictureLogo.Image = image;
//        }

//        public void SetProductName(string productName)
//        {
//            labelProductName.Text = productName;
//        }

//        public void SetVersion(string version)
//        {
//            labelVersion.Text = version;
//        }
//    }
//}
