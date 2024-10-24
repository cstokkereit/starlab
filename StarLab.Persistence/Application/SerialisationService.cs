using AutoMapper;
using StarLab.Application.Workspace;
using System.Xml.Serialization;

namespace StarLab.Application
{
    public class SerialisationService : ISerialisationService
    {
        private const string WORKSPACE_EXTENSION = ".slw";

        private readonly IMapper mapper;

        public SerialisationService(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public WorkspaceDTO DeserialiseWorkspace(string filename)
        {
            Workspace.Workspace? workspace = null;

            if (!string.IsNullOrEmpty(filename) && Path.GetExtension(filename) == WORKSPACE_EXTENSION)
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Workspace.Workspace));
                    workspace = serializer.Deserialize(stream) as Workspace.Workspace;
                }
            }

            if (workspace == null) throw new Exception();

            return mapper.Map<Workspace.Workspace, WorkspaceDTO>(workspace);
        }

        public void SerialiseWorkspace(WorkspaceDTO dto, string filename)
        {
            Workspace.Workspace? workspace = mapper.Map<WorkspaceDTO, Workspace.Workspace>(dto);

            using (var stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.SetLength(0);
                XmlSerializer serializer = new XmlSerializer(typeof(Workspace.Workspace));
                serializer.Serialize(stream, workspace);
            }
        }
    }
}
