using log4net;
using StarLab.Presentation;
using StarLab.Presentation.Workspace.Documents.Tables;
using StarLab.Shared;
using StarLab.Shared.Properties;
using StarLab.UI.Core;

namespace StarLab.UI.Workspace.Documents.Tables
{
    /// <summary>
    /// A <see cref="UserControl"/> that implements the <see cref="ITableView"/> interface used to control the behaviour that is specific to a table document.
    /// </summary>
    public partial class TableView : ChildView, ITableView
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TableView)); // The logger that will be used for writing log messages.

        private ITableViewPresenter? presenter;  // The presenter that controls the view.

        /// <summary>
        /// Initialises a new instance of the <see cref="ChartView"> class.
        /// </summary>
        public TableView()
            : base(ViewIDs.Table, SplitViewPanels.Panel2)
        {
            InitializeComponent();

            Name = ViewNames.Table;
        }

        /// <summary>
        /// Attaches the <see cref="IChildViewPresenter"/> that controls the view.
        /// </summary>
        /// <param name="presenter">The <see cref="IChildViewPresenter"/> that controls the view.</param>
        public override void Attach(IChildViewPresenter presenter)
        {
            if (this.presenter != null) throw new InvalidOperationException(Resources.PresenterAlreadyAttached);

            this.presenter = (ITableViewPresenter)presenter;

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
        /// Updates the state of the table.
        /// </summary>
        /// <param name="table">The <see cref="ITable"/> that specifies the new state of the table.</param>
        public void UpdateTable(ITable table)
        {
            throw new NotImplementedException();
        }
    }
}
