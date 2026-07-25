using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Tables;
using StarLab.Shared;
using StarLab.Shared.Properties;

namespace StarLab.UI.Workspace.Documents.Tables
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the <see cref="ITableView"/> interface used to control the behaviour that is specific to a table document.
    /// </summary>
    public partial class TableView : UserControl, ITableView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TableView)); // The logger that will be used for writing log messages.

        private ITableViewPresenter? presenter;  // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartView"> class.
        /// </summary>
        public TableView()
        {
            InitializeComponent();

            ID = ViewIDs.Table;
            Name = ViewNames.Table;
        }

        /// <summary>
        /// Gets the view ID.
        /// </summary>
        public ViewID ID { get; }

        /// <summary>
        /// Gets the preferred panel, if any, in which to display the view.
        /// </summary>
        public SplitViewPanels Panel => SplitViewPanels.Panel2;

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (ITableViewPresenter)presenter;

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
        /// Updates the state of the table.
        /// </summary>
        /// <param name="table">The <see cref="ITable"/> that specifies the new state of the table.</param>
        public void UpdateTable(ITable table)
        {
            throw new NotImplementedException();
        }
    }
}
