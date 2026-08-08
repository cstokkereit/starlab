using AutoMapper;
using StarLab.Application;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// A service that executes the use cases that implement table document functionality.
    /// </summary>
    public class TableUseCaseService : UseCaseService, ITableUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="TableUseCaseService"/>.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public TableUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }
    }
}
