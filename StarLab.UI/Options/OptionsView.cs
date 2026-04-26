using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Options;
using StarLab.Shared;
using StarLab.Shared.Properties;

namespace StarLab.UI.Options
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the Options dialog.
    /// </summary>
    public partial class OptionsView : UserControl, IOptionsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionsView)); // The logger that will be used for writing log messages.

        private IOptionsViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="OptionsView"> class.
        /// </summary>
        public OptionsView()
        {
            InitializeComponent();

            Name = Views.Options;
        }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public string ID => Name;

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

            this.presenter = (IOptionsViewPresenter)presenter;

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
    }
}
