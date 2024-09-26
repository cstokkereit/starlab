using AutoMapper;
using StarLab.Application.DataTransfer;

namespace StarLab.Application.Workspace
{
    internal class SaveWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, ISaveWorkspaceUseCase
    {
        private readonly ISerialisationService serialisationService;

        public SaveWorkspaceInteractor(ISerialisationService serialisationService, IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            this.serialisationService = serialisationService;
        }

        public void Execute(WorkspaceDTO dto)
        {
            throw new NotImplementedException();

            //dto.FileName = "D:\\Users\\Colin\\Documents\\WIP\\StarLab\\Workspaces\\Workspace-2.slw";

            //serialisationService.SerialiseWorkspace(dto, dto.FileName);
        }
    }
}
