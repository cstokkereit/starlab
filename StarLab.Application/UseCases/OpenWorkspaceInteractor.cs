using StarLab.Application.Workspaces;

namespace StarLab.Application.UseCases
{
    internal class OpenWorkspaceInteractor : IOpenWorkspaceUseCase
    {
        private readonly ISerialisationService serialisationService;

        private readonly IWorkspaceOutputPort outputPort;

        public OpenWorkspaceInteractor(ISerialisationService serialisationService, IWorkspaceOutputPort outputPort)
        {
            this.serialisationService = serialisationService;
            this.outputPort = outputPort;
        }

        public void Execute(string filename)
        {
            var dto = serialisationService.DeserialiseWorkspace(filename);

            // Handle errors etc, return result? success failure

            outputPort.UpdateWorkspace(dto);
        }
    }
}
