using AutoMapper;
using StarLab.Application.Data;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that .
    /// </summary>
    internal class UpdateChartInteractor : UseCaseInteractor<IChartOutputPort>, IUseCase<UpdateChartUseCaseArgs>
    {
        private readonly IDatabaseManager databases; //

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplyChartSettingsInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        /// <param name="databases">An <see cref="IDatabaseManager"/> that will be used to access the data.</param>
        public UpdateChartInteractor(IChartOutputPort outputPort, IMapper mapper, IDatabaseManager databases)
            : base(outputPort, mapper)
        {
            this.databases = databases ?? throw new ArgumentNullException(nameof(databases));
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="args">The <see cref="UpdateChartUseCaseArgs"/> that provide all of the information required to execute the use case.</param>
        public void Execute(UpdateChartUseCaseArgs args)
        {
            databases.OpenConnection(args.Host, args.Port);

            var database = databases.GetDatabase(args.DatabaseName);


            // Will need to return the dataset returned by the query - async?
        }
    }
}
