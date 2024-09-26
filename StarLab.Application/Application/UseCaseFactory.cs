using AutoMapper;
using StarLab.Application.Workspace;

namespace StarLab.Application
{
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly ISerialisationService serialisationService;

        private readonly IMapper mapper;

        public UseCaseFactory(IMapper mapper, ISerialisationService serialisationService)
        {
            this.serialisationService = serialisationService;
            this.mapper = mapper;
        }

        public IOpenWorkspaceUseCase CreateOpenWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new OpenWorkspaceInteractor(serialisationService, outputPort, mapper);
        }

        public IRenameDocumentUseCase CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameDocumentInteractor(outputPort, mapper);
        }

        public IRenameDocumentUseCase CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameFolderInteractor(outputPort, mapper);
        }

        public ISaveWorkspaceUseCase CreateSaveWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {

            return new SaveWorkspaceInteractor(serialisationService, outputPort, mapper);
        }
    }
}
