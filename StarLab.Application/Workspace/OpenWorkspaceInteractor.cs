using AutoMapper;
using log4net;
using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A use case that loads a workspace from a file.
    /// </summary>
    internal class OpenWorkspaceInteractor : UseCaseInteractor<IApplicationOutputPort>, IOpenWorkspaceUseCase
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(OpenWorkspaceInteractor)); // The logger that will be used for writing log messages.

        private readonly ISerialisationProvider serialiser; // Used to deserialise the workspace to a file.

        /// <summary>
        /// Initialises a new instance of the <see cref="SaveWorkspaceInteractor"/> class.
        /// </summary>
        /// <param name="serialiser">An <see cref="ISerialisationProvider"/> that will be used to deserialise the <see cref="WorkspaceDTO"/>.</param>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public OpenWorkspaceInteractor(ISerialisationProvider serialiser, IApplicationOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            this.serialiser = serialiser;
        }

        /// <summary>
        /// Executes the use case.
        /// </summary>
        /// <param name="filename">The name of the file that defines the workspace.</param>
        public void Execute(string filename)
        {
            try
            {
                var dto = serialiser.DeserialiseWorkspace(filename);

                dto.FileName = filename;

                OutputPort.SetWorkspace(dto);
            }
            catch (FileNotFoundException)
            {
                OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.FileNotFound, filename), InteractionType.Error, InteractionResponses.OK);
            }
            catch (Exception e)
            {
                OutputPort.ShowMessage(Resources.StarLab, string.Format(Resources.WorkspaceCouldNotBeOpened, filename), InteractionType.Error, InteractionResponses.OK);
                log.Error(e.Message, e);
            }
        }
    }
}
