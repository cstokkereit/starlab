using AutoMapper;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    internal class SaveWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, ISaveWorkspaceUseCase
    {
        private readonly ISerialisationProvider serialisationService;

        public SaveWorkspaceInteractor(ISerialisationProvider serialisationService, IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            this.serialisationService = serialisationService;
        }

        public void Execute(WorkspaceDTO dto)
        {
            try
            {
                if (!string.IsNullOrEmpty(dto.FileName)) serialisationService.SerialiseWorkspace(dto, dto.FileName);
            }
            catch (Exception e)
            {
                OutputPort.ShowMessage(Resources.StarLab, e.Message, InteractionType.Error, InteractionResponses.OK);
            }
        }
    }
}
