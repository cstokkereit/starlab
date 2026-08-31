using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Options;
using StarLab.Shared;
using StarLab.Shared.Properties;
using StarLab.UI.Core;

namespace StarLab.UI.Options
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the Options dialog.
    /// </summary>
    public partial class OptionsView : ChildView, IOptionsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionsView)); // The logger that will be used for writing log messages.

        private IOptionsViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="OptionsView"> class.
        /// </summary>
        public OptionsView()
            : base(ViewIDs.Options)
        {
            InitializeComponent();

            Name = ViewNames.Options;
        }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public override void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (IOptionsViewPresenter)presenter;

            log.Debug(string.Format(LogEntries.PresenterAttached, $"{presenter.GetType().Name}({Name})"));
        }

        /// <summary>
        /// Detaches the presenter that controls the view.
        /// </summary>
        public override void Detach()
        {
            if (presenter != null)
            {
                var entry = $"{presenter.GetType().Name}({Name})";

                presenter = null;

                log.Debug(string.Format(LogEntries.PresenterDetached, entry));
            }
        }
    }
}
