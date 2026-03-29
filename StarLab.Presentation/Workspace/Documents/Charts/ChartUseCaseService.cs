using AutoMapper;
using StarLab.Application;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A service that executes the use cases that implement chart document functionality.
    /// </summary>
    public class ChartUseCaseService : UseCaseService, IChartUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ChartUseCaseService"/>.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public ChartUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }
    }
}
