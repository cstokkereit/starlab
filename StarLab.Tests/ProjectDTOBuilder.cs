using StarLab.Application.Workspace;

namespace StarLab.Tests
{
    /// <summary>
    /// A helper class that uses the Builder Pattern to construct a <see cref="ProjectDTO"/> that can be used in unit tests.
    /// </summary>
    public class ProjectDTOBuilder
    {
        private ProjectDTO project; // The DTO being constructed.

        /// <summary>
        /// Initialises a new instance of the <see cref="ProjectDTOBuilder"/> class.
        /// </summary>
        public ProjectDTOBuilder()
        {
            project = new ProjectDTO();
        }

        /// <summary>
        /// Sets the database properties for the <see cref="ProjectDTO"/>.
        /// </summary>
        /// <param name="host">The host name.</param>
        /// <param name="port">The port number.</param>
        /// <param name="name">The database name.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="ProjectDTO"/>.</returns>
        public ProjectDTOBuilder SetDatabase(string host, int port, string name)
        {
            project.Database = new DatabaseDTO
            {
                Host = host,
                Port = port,
                Name = name
            };

            return this;
        }

        /// <summary>
        /// Sets the name property of the <see cref="ProjectDTO"/>.
        /// </summary>
        /// <param name="name">The project name.</param>
        /// <returns>This instance so that other methods can be called to continue constructing the <see cref="ProjectDTO"/>.</returns>
        public ProjectDTOBuilder SetName(string name)
        {
            project.Name = name;
            return this;
        }

        /// <summary>
        /// Returns the current <see cref="ProjectDTO"/> and clears the state of the builder so that it can be used again.
        /// </summary>
        /// <returns>The specified <see cref="ProjectDTO"/>.</returns>
        public ProjectDTO CreateProject()
        {
            var retval = project;
            project = new ProjectDTO();
            return retval;
        }
    }
}
