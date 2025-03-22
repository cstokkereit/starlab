using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents.Charts;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the settings panel used to configure a chart.
    /// </summary>
    public partial class ChartSettingsView : UserControl, IChartSettingsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(ChartSettingsView)); // The logger that will be used for writing log messages.

        private readonly IChartSettingsViewPresenter presenter; // The presenter that controls the view.

        private readonly SplitViewPanels panel; // The panel that will contain the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartSettingsView"> class.
        /// </summary>
        /// <param name="configuration">An <see cref="IChildViewConfiguration"/> that holds the configuration information required to construct this view.</param>
        /// <param name="parent">An <see cref="IViewConfiguration"/> that holds the configuration information that was used to construct the parent view.</param>
        /// <param name="factory">An <see cref="IViewFactory"/> that will be used to create the presenter and child view.</param>
        public ChartSettingsView(IChildViewConfiguration configuration, IViewConfiguration parent, IViewFactory factory)
        {
            InitializeComponent();

            Name = Views.CHART_SETTINGS;

            panel = (SplitViewPanels)configuration.Panel;

            presenter = (IChartSettingsViewPresenter)factory.CreatePresenter(parent, this);
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
        /// Attaches the <see cref="ICommand"/> provided to the Cancel button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the Cancel button is clicked.</param>
        public void AttachCancelButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonCancel);
            }
        }

        /// <summary>
        /// Attaches the <see cref="ICommand"/> provided to the OK button.
        /// </summary>
        /// <param name="command">The <see cref="ICommand"/> that will be executed when the OK button is clicked.</param>
        public void AttachOKButtonCommand(ICommand command)
        {
            if (command is IComponentCommand componentCommand)
            {
                componentCommand.AddInstance(buttonOK);
            }
        }

        /// <summary>
        /// Initialises the view.
        /// </summary>
        /// <param name="controller">The <see cref="IApplicationController"/>.</param>
        public void Initialise(IApplicationController controller)
        {
            // Do Nothing
        }

        /// <summary>
        /// Sets the minimum size for the control.
        /// </summary>
        /// <param name="size">A <see cref="Size"/> that specifies the minimum height and width.</param>
        public void SetMinimumSize(Size size)
        {
            MinimumSize = size;
        }
    }
}
