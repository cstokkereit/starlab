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
        public void TestAddDocument()
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
    }

    // TODO
    // Adding documents to projects - AddDocumentInteractor and tests
}
