using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Charts.Overlays;
using StarLab.Shared;
using StarLab.UI.Core;

namespace StarLab.UI.Workspace.Documents.Charts.Overlays
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the settings panel used to manage the chart overlays.
    /// </summary>
    public partial class OverlayManagerView : ChildView, IOverlayManagerView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsView)); // The logger that will be used for writing log messages.

        private IOverlayManagerViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="OverlayManagerView"> class.
        /// </summary>
        public OverlayManagerView()
            : base(ViewIDs.OverlayManager, SplitViewPanels.Panel1)
        {
            InitializeComponent();

            Name = ViewNames.OverlayManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="caption"></param>
        /// <param name="shown"></param>
        /// <exception cref="NotImplementedException"></exception>
        public void AddOverlay(string name, string caption, bool shown)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public override void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(ExceptionMessages.PresenterAlreadyAttached);

            this.presenter = (IOverlayManagerViewPresenter)presenter;

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
    }
}
