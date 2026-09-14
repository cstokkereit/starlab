using AutoMapper;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// An abstract base class that provides functions required by workspace interactors. TODO
    /// </summary>
    public abstract class WorkspaceInteractor : UseCaseInteractor<IWorkspaceOutputPort>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceInteractor"/> class.
        /// </summary>
        /// <param name="outputPort">An <see cref="IWorkspaceOutputPort"/> that updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        protected WorkspaceInteractor(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Checks the name provided to make sure that it does not contain any illegal characters.
        /// </summary>
        /// <param name="name">A name that may contain illegal characters.</param>
        /// <returns>true if the name does not contain illegal characters; false otherwise.</returns>
        protected static bool IsValid(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;

            foreach (var character in Constants.IllegalCharacters)
            {
                if (name.Contains(character)) return false;
            }

            return true;
        }
    }
}
