using AutoMapper;
using log4net;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    internal class OpenWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IOpenWorkspaceUseCase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OpenWorkspaceInteractor));

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
            catch (FileNotFoundException)
            {
                OutputPort.ShowErrorMessage(string.Format(Resources.FileNotFoundMessage, filename));
            }
            catch (Exception e)
            {
                var message = e.InnerException != null ? e.InnerException.Message : string.Empty;
                OutputPort.ShowErrorMessage(string.Format(Resources.WorkspaceCouldNotBeOpenedMessage, message));
                log.Error(e.Message, e);
            }
        }
    }
}
