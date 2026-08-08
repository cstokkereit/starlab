using log4net;
using StarLab.Application.Workspace.Documents.Tables;
using StarLab.Presentation.Configuration;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// Controls the behaviour of an <see cref="ITableView"/>.
    /// </summary>
    public class TableViewPresenter : ChildViewPresenter<ITableView, IDocumentController>, ITableViewPresenter, ITableController, ITableOutputPort
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(TableViewPresenter)); // The logger that will be used for writing log messages.

        private readonly ITableUseCaseService useCases; // A service that executes the use cases that implement the functionality.

        private ITable? table; // The table that the view represents.

        /// <summary>
        /// Initialises a new instance of the <see cref="TableViewPresenter"> class.
        /// </summary>
        /// <param name="view">The <see cref="ITableView"/> controlled by this presenter.</param>
        /// <param name="context">An <see cref="ISessionContext"/> that provides access to the session context.</param>
        /// <param name="commands">An <see cref="ICommandManager"/> that is required for the creation of <see cref="ICommand">s.</param>
        /// <param name="services">An <see cref="IServiceRegistry"/> that provides access to the registered services.</param>
        /// <param name="events">The <see cref="IEventAggregator"/> that manages application events.</param>
        public TableViewPresenter(ITableView view, ISessionContext context, ICommandManager commands, IServiceRegistry services, IEventAggregator events)
            : base(view, context, commands, events)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(useCases));

            useCases = services.GetService<ITableUseCaseService>();

            View.Attach(this);
        }

        /// <summary>
        /// The finaliser will only called if the <see cref="Dispose"/> method has not been called.
        /// </summary>
        ~TableViewPresenter()
        {
            Dispose(false);
        }

        /// <summary>
        /// Releases all resources used by the <see cref="TableViewPresenter"/> object.
        /// </summary>
        public override void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Updates the table following a change to the document or workspace.
        /// </summary>
        /// <param name="table">An <see cref="ITable"/> that specifies the state of the table.</param>
        public void UpdateTable(ITable table)
        {
            View.UpdateTable(table);

            this.table = table;
        }

        /// <summary>
        /// Applies the new table settings to the preview.
        /// </summary>
        /// <param name="dto">A <see cref="TableDTO"/> that specifies the state of the table.</param>
        public void UpdatePreview(TableDTO dto)
        {
            View.UpdateTable(new Table(dto));
        }

        /// <summary>
        /// Reverts the preview to the old table settings.
        /// </summary>
        public void UpdatePreview()
        {
            if (table != null) View.UpdateTable(table);
        }

        /// <summary>
        /// Releases any resources used by the <see cref="TableViewPresenter"/> object.
        /// </summary>
        /// <param name="disposing">true if managed resources can be disposed of; false otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                View.Detach();
            }
        }
    }
}
