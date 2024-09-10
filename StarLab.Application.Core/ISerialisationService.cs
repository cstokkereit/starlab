using StarLab.Application.DataTransfer;

namespace StarLab.Application
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISerialisationService
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
