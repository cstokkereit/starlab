using AutoMapper;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    public class UseCaseFactory : IUseCaseFactory
    {
        private readonly ISerialisationProvider serialisationService;

        private readonly IMapper mapper;

        public UseCaseFactory(IMapper mapper, ISerialisationProvider serialisationService)
        {
            this.serialisationService = serialisationService;
            this.mapper = mapper;
        }

        public IAddDocumentUseCase CreateAddDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddDocumentInteractor(outputPort, mapper);
        }

        public IAddFolderUseCase CreateAddFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new AddFolderInteractor(outputPort, mapper);
        }

        public IDeleteItemUseCase CreateDeleteDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteDocumentInteractor(outputPort, mapper);
        }

        public IDeleteItemUseCase CreateDeleteFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new DeleteFolderInteractor(outputPort, mapper);
        }

        public IOpenWorkspaceUseCase CreateOpenWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new OpenWorkspaceInteractor(serialisationService, outputPort, mapper);
        }

        public IRenameItemUseCase CreateRenameDocumentUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameDocumentInteractor(outputPort, mapper);
        }

        public IRenameItemUseCase CreateRenameFolderUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameFolderInteractor(outputPort, mapper);
        }

        public IRenameWorkspaceUseCase CreateRenameWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new RenameWorkspaceInteractor(serialisationService, outputPort, mapper);
        }

        public ISaveWorkspaceUseCase CreateSaveWorkspaceUseCase(IWorkspaceOutputPort outputPort)
        {
            return new SaveWorkspaceInteractor(serialisationService, outputPort, mapper);
        }
    }
}
