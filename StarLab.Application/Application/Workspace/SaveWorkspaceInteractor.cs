using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that saves the current workspace to a file.
    /// </summary>
    internal class SaveWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, ISaveWorkspaceUseCase
    {
        private readonly ISerialisationProvider serialiser; // Used to serialise the workspace to a file.

        /// <summary>
        /// Initialises a new instance of the <see cref="SaveWorkspaceInteractor"/> class.
        /// </summary>
        /// <param name="serialiser">An <see cref="ISerialisationProvider"/> that will be used to serialise the <see cref="WorkspaceDTO"/>.</param>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the ouputs of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public SaveWorkspaceInteractor(ISerialisationProvider serialiser, IWorkspaceOutputPort outputPort, IMapper mapper)
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
            try
            {
                if (!string.IsNullOrEmpty(dto.FileName)) serialiser.SerialiseWorkspace(dto, dto.FileName);
            }
            catch (Exception e)
            {
                OutputPort.ShowMessage(Resources.StarLab, e.Message, InteractionType.Error, InteractionResponses.OK);
            }
        }
    }
}
