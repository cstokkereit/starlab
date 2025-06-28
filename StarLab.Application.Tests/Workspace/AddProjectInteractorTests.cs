namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddProjectInteractor"/> class.
    /// </summary>
    public class AddProjectInteractorTests : InteractorTests
    {
        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method correctly adds a project to the workspace.
        /// </summary>
        [Test]
        public void TestAddProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateAddProjectUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWworkspace();

            var project = new ProjectDTO
            {
                Name = "Project2"
            };

            interactor.Execute(workspace, project);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 0 &&
                ws.Projects[1].Folders.Count == 0 &&
                ws.Projects[1].Documents.Count == 0));
        }

        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method shows an error message if a project with the same name already exists.
        /// </summary>
        [Test]
        public void TestAddProjectWhenProjectWithSameNameExists()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateAddProjectUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .CreateWworkspace();

            var project = new ProjectDTO
            {
                Name = "Project1"
            };

            interactor.Execute(workspace, project);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is("A project with the name 'Project1' already exists at this location.\r\nPlease provide a unique name for the project."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method shows an error message if the project name is an empty string.
        /// </summary>
        [Test]
        public void TestAddProjectWhenNameIsAnEmptyString()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateAddProjectUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .CreateWworkspace();

            var project = new ProjectDTO
            {
                Name = string.Empty
            };

            interactor.Execute(workspace, project);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is("The project name cannot be an empty string."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddProjectInteractor.Execute"/> method shows an error message if the project name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestAddProjectWhenNameIsInvalid()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateAddProjectUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .CreateWworkspace();

            var project = new ProjectDTO
            {
                Name = "Project1/"
            };

            interactor.Execute(workspace, project);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is("Project names cannot include any of the following:\r\n\r\n                               \\ / : * ? ' \" < > |\r\n\r\nPlease enter a valid name."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }
    }
}
