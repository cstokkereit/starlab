using AutoMapper;

namespace StarLab.Application.Workspace
{
    internal class OpenWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IOpenWorkspaceUseCase
    {
        private readonly ISerialisationService serialisationService;

        public OpenWorkspaceInteractor(ISerialisationService serialisationService, IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            this.serialisationService = serialisationService;
        }

        public void Execute(string filename)
        {
            var dto = serialisationService.DeserialiseWorkspace(filename);

            // Handle errors etc, return result? success failure

            OutputPort.UpdateWorkspace(dto);
        }
    }
}
