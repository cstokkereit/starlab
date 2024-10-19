using AutoMapper;
using StarLab.Application;
using StarLab.Application.DataTransfer;
using StarLab.Serialisation.Model;
using System.Xml.Serialization;

namespace StarLab.Serialisation
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
            Workspace? workspace = null;

            if (!string.IsNullOrEmpty(filename) && Path.GetExtension(filename) == WORKSPACE_EXTENSION)
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Workspace));
                    workspace = serializer.Deserialize(stream) as Workspace;
                }
            }

            if (workspace == null) throw new Exception();

            return mapper.Map<Workspace, WorkspaceDTO>(workspace);
        }

        public void SerialiseWorkspace(WorkspaceDTO dto, string filename)
        {
            Workspace? workspace = mapper.Map<WorkspaceDTO, Workspace>(dto);

            using (var stream = new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write))
            {
                stream.SetLength(0);
                XmlSerializer serializer = new XmlSerializer(typeof(Workspace));
                serializer.Serialize(stream, workspace);
            }
        }
    }
}
