using StarLab.Application.Workspaces;

namespace StarLab.Application.UseCases
{
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly ISerialisationService serialisationService;

        public UseCaseFactory(ISerialisationService serialisationService)
        {
            this.serialisationService = serialisationService;
        }

        public IOpenWorkspaceUseCase CreateOpenWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {


            return new OpenWorkspaceInteractor(serialisationService, outputPort);
        }

        public ISaveWorkspaceUseCase CreateSaveWorkspaceUseCase()
        {

            return new SaveWorkspaceInteractor(serialisationService);
        }
    }
}
