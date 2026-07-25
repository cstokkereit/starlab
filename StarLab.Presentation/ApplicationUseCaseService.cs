using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Workspace;

namespace StarLab.Presentation
{
    /// <summary>
    /// A service that executes the use cases that implement the application functionality.
    /// </summary>
    public class ApplicationUseCaseService : UseCaseService, IApplicationUseCaseService
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationUseCaseService"/>.
        /// </summary>
        /// <param name="factory">An <see cref="IUseCaseFactory"/> that will be used to create use case interactors.</param>
        /// <param name="mapper">An <see cref="IMapper"/> that will be used to map model objects to data transfer objects and vice versa.</param>
        public ApplicationUseCaseService(IUseCaseFactory factory, IMapper mapper)
            : base(factory, mapper) { }

        /// <summary>
        /// Executes the AddFolder use case.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> being modified.</param>
        /// <param name="key">The key that identifies the parent folder.</param>
        public void AddFolder(IWorkspace workspace, string key)
        {
            ArgumentNullException.ThrowIfNull(workspace, nameof(workspace));
            ArgumentException.ThrowIfNullOrEmpty(key, nameof(key));

            var interactor = Factory.CreateAddFolderUseCase(ApplicationController.GetOutputPort<IWorkspaceOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace), key);
        }

        /// <summary>
        /// Executes the OpenWorkspace use case.
        /// </summary>
        /// <param name="filename">The fully qualified path to the workspace file.</param>
        public void OpenWorkspace(string filename)
        {
            var interactor = Factory.CreateOpenWorkspaceUseCase(ApplicationController.GetOutputPort<IApplicationOutputPort>());

            interactor.Execute(filename);
        }

        /// <summary>
        /// Executes the SaveWorkspace use case.
        /// </summary>
        /// <param name="workspace">The current workspace.</param>
        public void SaveWorkspace(IWorkspace workspace)
        {
            var interactor = Factory.CreateSaveWorkspaceUseCase(ApplicationController.GetOutputPort<IApplicationOutputPort>());

            interactor.Execute(Mapper.Map<WorkspaceDTO>(workspace));
        }
    }
}
