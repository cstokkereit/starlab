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
            try
            {
                var dto = serialisationService.DeserialiseWorkspace(filename);

                dto.FileName = filename;

                OutputPort.UpdateWorkspace(dto);
            }
            catch (FileNotFoundException e1)
            {
                OutputPort.ShowErrorMessage(e1.Message);
            }
            catch (Exception e2)
            {
                OutputPort.ShowErrorMessage(e2.Message);
            }
        }
    }
}
