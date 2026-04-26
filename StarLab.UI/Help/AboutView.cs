using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Help;
using StarLab.Shared;
using StarLab.Shared.Properties;

namespace StarLab.UI.Help
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the About dialog.
    /// </summary>
    public partial class AboutView : UserControl, IAboutView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AboutView)); // The logger that will be used for writing log messages.

        private IAboutViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutView"> class.
        /// </summary>
        public AboutView()
        {
            InitializeComponent();

            Name = ID;
        }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => Views.About;

        /// <summary>
        /// Gets the panel that will contain the view.
        /// </summary>
        public SplitViewPanels Panel => SplitViewPanels.Any;

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (IAboutViewPresenter)presenter;

            log.Debug(string.Format(LogEntries.PresenterAttached, $"{presenter.GetType().Name}({Name})"));
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public void Detach()
        {
            if (presenter != null)
            {
                var entry = $"{presenter.GetType().Name}({Name})";

                presenter = null;

                log.Debug(string.Format(LogEntries.PresenterDetached, entry));
            }
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        public void Initialise()
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
