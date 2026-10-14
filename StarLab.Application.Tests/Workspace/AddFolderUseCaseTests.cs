using StarLab.Tests;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddFolderUseCase"/> class.
    /// </summary>
    public class AddFolderUseCaseTests : ApplicationTests
    {
        /// <summary>
        /// Test that the <see cref="AddFolderuseCase.Execute"/> method correctly adds a folder to an existing folder.
        /// </summary>
        [Test]
        public void TestAddFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var useCase = factory.CreateAddFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            useCase.Execute(new AddFolderUseCaseArgs(workspace, "Workspace/Project1/Folder1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/New Folder"));

            port.Received().RenameFolder("Workspace/Project1/Folder1/New Folder");
        }

        /// <summary>
        /// Test that the <see cref="AddFolderuseCase.Execute"/> method correctly adds a folder to an existing folder that already has a new folder that has not been renamed.
        /// </summary>
        [Test]
        public void TestAddFolderWhenNewFolderExists()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var useCase = factory.CreateAddFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/New Folder")
                .CreateWorkspace();

            useCase.Execute(new AddFolderUseCaseArgs(workspace, "Workspace/Project1/Folder1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/New Folder" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder1/New Folder1"));

            port.Received().RenameFolder("Workspace/Project1/Folder1/New Folder1");
        }

        /// <summary>
        /// Test that the <see cref="AddFolderuseCase.Execute"/> method correctly adds a folder to a project.
        /// </summary>
        [Test]
        public void TestAddProjectFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var useCase = factory.CreateAddFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            useCase.Execute(new AddFolderUseCaseArgs(workspace, "Workspace/Project1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/New Folder"));

            port.Received().RenameFolder("Workspace/Project1/New Folder");
        }

        /// <summary>
        /// Test that the <see cref="AddFolderuseCase.Execute"/> method correctly adds a folder to a project that already has a new folder that has not been renamed.
        /// </summary>
        [Test]
        public void TestAddProjectFolderWhenNewFolderExists()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var useCase = factory.CreateAddFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/New Folder")
                .CreateWorkspace();

            useCase.Execute(new AddFolderUseCaseArgs(workspace, "Workspace/Project1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/New Folder" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/New Folder1"));

            port.Received().RenameFolder("Workspace/Project1/New Folder1");
        }
    }
}
