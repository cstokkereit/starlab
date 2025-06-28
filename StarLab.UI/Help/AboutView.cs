using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Help;

namespace StarLab.UI.Help
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the About dialog.
    /// </summary>
    public partial class AboutView : UserControl, IAboutView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AboutView)); // The logger that will be used for writing log messages.

        private readonly IAboutViewPresenter presenter; // The presenter that controls the view.

        private readonly SplitViewPanels panel; // The panel that will contain the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutView"> class.
        /// </summary>
        /// <param name="definition">An <see cref="IViewDefinition"/> that holds the information required to construct this view.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        public AboutView(IViewDefinition definition, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.About;

            panel = (SplitViewPanels)definition.Panel;

            presenter = (IAboutViewPresenter)factory.CreatePresenter(this);
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"> that controls this view.
        /// </summary>
        public IChildViewController Controller => (IChildViewController)presenter;

        /// <summary>
        /// Gets the panel that will contain the view.
        /// </summary>
        public SplitViewPanels Panel => panel;

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            throw new NotImplementedException(); 
        }

        /// <summary>
        /// Sets the company name.
        /// </summary>
        /// <param name="name">The company name.</param>
        public void SetCompanyName(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the copyright text.
        /// </summary>
        /// <param name="copyright">The copyright text.</param>
        public void SetCopyright(string copyright)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the description text.
        /// </summary>
        /// <param name="description">The description text.</param>
        public void SetDescription(string description)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Sets the logo image.
        /// </summary>
        /// <param name="image">The logo image.</param>
        public void SetLogo(Image image)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the product name.
        /// </summary>
        /// <param name="name">The product name.</param>
        public void SetProductName(string name)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Sets the version number.
        /// </summary>
        /// <param name="version">The version number.</param>
        public void SetVersion(string version)
        {
            throw new NotImplementedException();
        }
    }
}
