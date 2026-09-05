using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Help;
using StarLab.Shared;
using StarLab.Shared.Properties;
using StarLab.UI.Core;

namespace StarLab.UI.Help
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the About dialog.
    /// </summary>
    public partial class AboutView : ChildView, IAboutView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(AboutView)); // The logger that will be used for writing log messages.

        private IAboutViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="AboutView"> class.
        /// </summary>
        public AboutView()
            : base(ViewIDs.About)
        {
            InitializeComponent();

            Name = ViewNames.About;
        }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public override void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (IAboutViewPresenter)presenter;

            log.Debug(LogEntries.PresenterAttached(presenter.GetType()));
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public override void Detach()
        {
            if (presenter != null)
            {
                var type = presenter.GetType();

                presenter = null;

                log.Debug(LogEntries.PresenterDetached(type));
            }
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
