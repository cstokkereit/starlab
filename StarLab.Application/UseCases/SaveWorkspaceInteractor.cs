using StarLab.Application.DataTransfer;
using StarLab.Application.Workspaces;

namespace StarLab.Application.UseCases
{
    internal class SaveWorkspaceInteractor : Interactor<WorkspaceDTO>, ISaveWorkspaceUseCase
    {
        private readonly ISerialisationService serialisationService;

        public SaveWorkspaceInteractor(ISerialisationService serialisationService)
        {
            this.serialisationService = serialisationService;
        }

        public override void Execute(WorkspaceDTO dto)
        {
            dto.FileName = "D:\\Users\\Colin\\Documents\\WIP\\StarLab\\Workspaces\\Workspace-2.slw";

            serialisationService.SerialiseWorkspace(dto, dto.FileName);
        }
    }
}
