using AutoMapper;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// An abstract base class that provides functions required by workspace UseCases. TODO
    /// </summary>
    public abstract class WorkspaceUseCase : UseCase<IWorkspaceOutputPort>
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="WorkspaceUseCase"/> class.
        /// </summary>
        /// <param name="outputPort">Updates the UI in response to the execution of the use case.</param>
        /// <param name="mapper">Maps model objects to data transfer objects and vice versa.</param>
        protected WorkspaceUseCase(IWorkspaceOutputPort outputPort, IMapper mapper)
            : base(outputPort, mapper) { }

        /// <summary>
        /// Checks the name provided to make sure that it does not contain any illegal characters.
        /// </summary>
        /// <param name="name">A name that may contain illegal characters.</param>
        /// <returns><c>true</c> if the name does not contain illegal characters; <c>false</c> otherwise.</returns>
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
