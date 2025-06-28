using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using System.Xml.Serialization;

namespace StarLab.Serialisation
{
    /// <summary>
    /// Serialises and deserialises data transfer objects (DTOs).
    /// </summary>
    public class SerialisationProvider : ISerialisationProvider
    {
        private readonly IMapper mapper; // Maps POCOs to DTOs and vice versa.

        /// <summary>
        /// Initialises a new instance of the <see cref="SerialisationProvider"/> class.
        /// </summary>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map POCOs to DTOs and vice versa.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SerialisationProvider(IMapper mapper)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <summary>
        /// Deserialises the specified source file.
        /// </summary>
        /// <param name="filename">The name of the source file.</param>
        /// <returns>The deserialised <see cref="WorkspaceDTO"/>.</returns>
        public WorkspaceDTO DeserialiseWorkspace(string filename)
        {
            Workspace.Workspace? workspace = null;

            if (!string.IsNullOrEmpty(filename) && Path.GetExtension(filename) == Constants.WorkspaceExtension)
            {
                using (var stream = new FileStream(filename, FileMode.Open, FileAccess.Read))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(Workspace.Workspace));
                    workspace = serializer.Deserialize(stream) as Workspace.Workspace;
                }
            }

            if (workspace == null) throw new Exception(); // TODO - Exception message

            return mapper.Map<Workspace.Workspace, WorkspaceDTO>(workspace);
        }

        /// <summary>
        /// Serialises the <see cref="WorkspaceDTO"/> provided to the specified destination file.
        /// </summary>
        /// <param name="workspace">The <see cref="WorkspaceDTO"/> to be serialised.</param>
        /// <param name="filename">The name of the destination file.</param>
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
