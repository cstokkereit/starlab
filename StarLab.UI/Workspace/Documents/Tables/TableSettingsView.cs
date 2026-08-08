using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Tables;
using StarLab.Shared;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.UI.Workspace.Documents.Tables
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the behaviour that is specific to the settings panel used to configure a table.
    /// </summary>
    public partial class TableSettingsView : UserControl, ITableSettingsView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TableSettingsView)); // The logger that will be used for writing log messages.

        private ITableSettingsViewPresenter? presenter; // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="TableSettingsView"> class.
        /// </summary>
        public TableSettingsView()
        {
            InitializeComponent();

            ID = ViewIDs.TableSettings;
            Name = ViewNames.TableSettings;
        }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public ViewID ID { get; }

        /// <summary>
        /// Gets the panel that will contain the view.
        /// </summary>
        public SplitViewPanels Panel => SplitViewPanels.Panel1;

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (ITableSettingsViewPresenter)presenter;

            log.Debug(string.Format(LogEntries.PresenterAttached, $"{presenter.GetType().Name}({Name})"));
        }

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
            // TODO
        }
    }
}
