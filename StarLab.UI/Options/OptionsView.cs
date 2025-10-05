using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Options;
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

            if (log.IsDebugEnabled) log.Debug(string.Format(Resources.InstanceCreated, nameof(OptionsView)));
        }

        /// <summary>
        /// Gets the <see cref="IChildViewController"> that controls this view.
        /// </summary>
        public IChildViewController? Controller => (IChildViewController?)presenter;

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
            if (this.presenter != null) throw new InvalidOperationException(); // TODO

            this.presenter = (IOptionsViewPresenter)presenter;
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            throw new NotImplementedException();
        }
    }
}
