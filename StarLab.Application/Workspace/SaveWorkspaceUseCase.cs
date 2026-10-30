using AutoMapper;
using log4net;
using StarLab.Shared;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that saves the current workspace to a file.
    /// </summary>
    internal class SaveWorkspaceUseCase : UseCase<IApplicationOutputPort>, IUseCase<WorkspaceDTO>
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(SaveWorkspaceUseCase)); // The logger that will be used for writing log messages.

        private readonly ISerialisationService serialiser; // Used to serialise the workspace to a file.

        /// <summary>
        /// Initialises a new instance of the <see cref="SaveWorkspaceUseCase"/> class.
        /// </summary>
        /// <param name="serialiser">An <see cref="ISerialisationService"/> that will be used to serialise the <see cref="WorkspaceDTO"/>.</param>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">Maps model objects to data transfer objects and vice versa.</param>
        public SaveWorkspaceUseCase(ISerialisationService serialiser, IApplicationOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            this.serialiser = serialiser;
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> that specifies the current state of the workspace.</param>
        public void Execute(WorkspaceDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            log.Debug(LogEntries.OpeningWorkspace(dto.FileName));

            if (!string.IsNullOrEmpty(dto.FileName)) serialiser.SerialiseWorkspace(dto, dto.FileName);
        }
    }
}
