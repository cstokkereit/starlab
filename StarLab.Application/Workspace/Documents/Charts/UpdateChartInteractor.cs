using AutoMapper;

namespace StarLab.Application.Workspace.Documents.Charts
{
    /// <summary>
    /// A use case that .
    /// </summary>
    internal class UpdateChartInteractor : UseCaseInteractor<IChartOutputPort>, IUseCase<WorkspaceDTO>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApplyChartSettingsInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IAddDocumentOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public UpdateChartInteractor(IChartOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            // Will need a DataProvider, create a connection and open a database, then execute the query and return the results to the output port.
            // The details of the database connection and collection will br rpovided by the project
            // 
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        public void Execute(WorkspaceDTO dto)
        {
            // Will need to return the dataset returned by the query

            throw new NotImplementedException();
        }
    }
}
