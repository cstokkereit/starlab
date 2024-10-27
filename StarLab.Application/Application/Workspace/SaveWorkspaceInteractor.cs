using AutoMapper;

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
            try
            {
                if (!string.IsNullOrEmpty(dto.FileName))
                    serialisationService.SerialiseWorkspace(dto, dto.FileName);
            }
            catch (Exception ex)
            {
                OutputPort.ShowErrorMessage(ex.Message);
            }
        }
    }
}
