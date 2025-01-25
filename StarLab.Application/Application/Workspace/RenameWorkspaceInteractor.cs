using AutoMapper;
using StarLab.Shared.Properties;
using System.Diagnostics;

namespace StarLab.Application.Workspace
{
    internal class RenameWorkspaceInteractor : WorkspaceInteractor, IRenameWorkspaceUseCase
    {
        private readonly ISerialisationProvider serialisationService;

        public RenameWorkspaceInteractor(ISerialisationProvider serialisationService, IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper)
        {
            this.serialisationService = serialisationService;
        }

        public void Execute(WorkspaceDTO dto, string name)
        {
            if (IsValid(name))
            {
                Debug.Assert(!string.IsNullOrEmpty(dto.FileName));

                var filename = dto.FileName;

                dto.FileName = Path.Join(Path.GetDirectoryName(dto.FileName), $"{name}.{Constants.WORKSPACE_EXTENSION}");

                if (!File.Exists(dto.FileName))
                {
                    try
                    {
                        serialisationService.SerialiseWorkspace(dto, dto.FileName);
                        File.Delete(filename);
                    }
                    catch (Exception e)
                    {
                        OutputPort.ShowMessage(Resources.StarLab, e.Message, InteractionType.Error, InteractionResponses.OK);
                        dto.FileName = filename;
                    }
                    finally
                    {
                        OutputPort.UpdateWorkspace(dto);
                    }
                }
                else
                {
                    throw CreateTargetExistsException(Path.GetFileName(filename), Path.GetFileName(dto.FileName), Resources.Workspace);
                }
            }
            else
            {
                throw CreateInvalidNameException(name, Resources.Workspace);
            }
        }
    }
}
