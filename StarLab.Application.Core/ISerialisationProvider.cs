using StarLab.Application.Workspace;

namespace StarLab.Application
{
    /// <summary>
    /// Serialises and deserialises data transfer objects (DTOs).
    /// </summary>
    public interface ISerialisationProvider
    {
        /// <summary>
        /// Deserialises the specified source file.
        /// </summary>
        /// <param name="filename">The name of the source file.</param>
        /// <returns>The deserialised <see cref="WorkspaceDTO"/>.</returns>
        WorkspaceDTO DeserialiseWorkspace(string filename);

        /// <summary>
        /// Serialises the <see cref="WorkspaceDTO"/> provided to the specified destination file.
        /// </summary>
        /// <param name="workspace">The <see cref="WorkspaceDTO"/> to be serialised.</param>
        /// <param name="filename">The name of the destination file.</param>
        void SerialiseWorkspace(WorkspaceDTO workspace, string filename);
    }
}
