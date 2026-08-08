
using AutoMapper;
using StarLab.Application;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// A service that executes the use cases that implement the table settings panel functionality.
    /// </summary>
    public class TableSettingsUseCaseService : UseCaseService, ITableSettingsUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TableSettingsUseCaseService"/> class.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public TableSettingsUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }

        /// <summary>
        /// Executes the UpdateTable use case.
        /// </summary>
        /// <param name="id">The ID of the table view controller.</param>
        /// <param name="table">A <see cref="ITableSettings"/> that specifies the current state of the table.</param>
        public void UpdateTable(DocumentID id, ITableSettings table)
        {

        }

        /// <summary>
        /// Executes the UpdateDocument use case.
        /// </summary>
        /// <param name="workspace">A <see cref="IWorkspace"/> that specifies the current state of the workspace.</param>
        /// <param name="id">The ID of the table view controller.</param>
        /// <param name="table">A <see cref="ITableSettings"/> that specifies the current state of the table.</param>
        public void UpdateDocument(IWorkspace workspace, DocumentID id, ITableSettings table)
        {

        }
    }
}
