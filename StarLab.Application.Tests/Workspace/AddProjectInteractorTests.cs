using StarLab.Tests;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddProjectInteractor"/> class.
    /// </summary>
    public class AddProjectInteractorTests : ApplicationTests
    {
        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method correctly adds a project to the workspace.
        /// </summary>
        [Test]
        public void TestAddProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddProjectUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var project = new ProjectDTO
            {
                Name = "Project2"
            };

            interactor.Execute(new AddProjectUseCaseArgs(workspace, project));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 0 &&
                ws.Projects[1].Folders.Count == 0 &&
                ws.Projects[1].Documents.Count == 0));
        }

        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method throws an exception if a project with the same name already exists.
        /// </summary>
        [Test]
        public void TestAddProjectThrowsExceptionWhenProjectWithSameNameExists()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddProjectUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            var project = new ProjectDTO
            {
                Name = "Project1"
            };

            Assert.Throws<FolderExistsException>(() => interactor.Execute(new AddProjectUseCaseArgs(workspace, project)));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method throws an exception if the project name is an empty string.
        /// </summary>
        [Test]
        public void TestAddProjectWhenNameIsAnEmptyStringThrowsException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddProjectUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            var project = new ProjectDTO
            {
                Name = string.Empty
            };

            Assert.Throws<InvalidNameException>(() => interactor.Execute(new AddProjectUseCaseArgs(workspace, project)));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method throws an exception if the project name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestAddProjectThrowsExceptionWhenNameIsInvalid()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddProjectUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            var project = new ProjectDTO
            {
                Name = "Project1/"
            };

            Assert.Throws<InvalidNameException>(() => interactor.Execute(new AddProjectUseCaseArgs(workspace, project)));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }
    }
}
