using log4net;
using StarLab.Application.Configuration;

namespace StarLab.Application.Options
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the Options dialog.
    /// </summary>
    public partial class OptionsView : UserControl, IOptionsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OptionsView)); // The logger that will be used for writing log messages.

        private readonly IOptionsViewPresenter presenter; // The presenter that controls the view.

        private readonly SplitViewPanels panel; // The panel that will contain the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="OptionsView"> class.
        /// </summary>
        /// <param name="configuration">An <see cref="IChildViewConfiguration"/> that holds the configuration information required to construct this view.</param>
        /// <param name="parent">An <see cref="IViewConfiguration"/> that holds the configuration information that was used to construct the parent view.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        public OptionsView(IChildViewConfiguration configuration, IViewConfiguration parent, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.OPTIONS;

            panel = (SplitViewPanels)configuration.Panel;

            presenter = (IOptionsViewPresenter)factory.CreatePresenter(parent, this);
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
    }
}
