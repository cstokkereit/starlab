using AutoMapper;
using log4net;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    internal class OpenWorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>, IOpenWorkspaceUseCase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OpenWorkspaceInteractor)); // The logger that will be used for writing log messages.

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
                OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.FileNotFoundMessage, filename), InteractionType.Error, InteractionResponses.OK);
            }
            catch (Exception e)
            {
                var message = e.InnerException != null ? e.InnerException.Message : string.Empty;
                OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.WorkspaceCouldNotBeOpenedMessage, message), InteractionType.Error, InteractionResponses.OK);
                log.Error(e.Message, e);
            }
        }
    }
}
