using StarLab.Tests;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DeleteFolderInteractor"/> class.
    /// </summary>
    public class DeleteFolderInteractorTests : ApplicationTests
    {
        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method deletes an empty folder.
        /// </summary>
        [Test]
        public void TestDeleteEmptyFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .CreateWorkspace();

            interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder3"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method deletes a folder and its documents.
        /// </summary>
        [Test]
        public void TestDeleteFolderWithDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC021", "Document2", "Workspace/Project1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC022", "Document3", "Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document4", "Workspace/Project1/Folder3")
                .CreateWorkspace();

            interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder3"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method deletes a folder, its child folders and documents.
        /// </summary>
        [Test]
        public void TestDeleteFolderWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC111", "Document1", "Workspace/Project1/Folder1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC112", "Document2", "Workspace/Project1/Folder1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC121", "Document3", "Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC122", "Document4", "Workspace/Project1/Folder1/Folder2")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder2/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC211", "Document5", "Workspace/Project1/Folder2/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC212", "Document6", "Workspace/Project1/Folder2/Folder1")
                .AddFolder("Workspace/Project1/Folder2/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC221", "Document7", "Workspace/Project1/Folder2/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC222", "Document8", "Workspace/Project1/Folder2/Folder2")
                .CreateWorkspace();

            interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project1/Folder1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder2/Folder2" &&
                ws.Projects[0].Documents.Count == 4 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC211" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC212" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder2/Folder2" &&
                ws.Projects[0].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CCC221" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder2/Folder2" &&
                ws.Projects[0].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CCC222"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method throws an exception if the target folder does not exist.
        /// </summary>
        [Test]
        public void TestDeleteNonExistentFolderThrowsException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            Assert.Throws<KeyNotFoundException>(() => interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project1/Folder2")));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="DeleteProjectInteractor.Execute"/> method deletes an empty project.
        /// </summary>
        [Test]
        public void TestDeleteEmptyProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws => ws.Projects.Count == 0));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method deletes a project and its documents.
        /// </summary>
        [Test]
        public void TestDeleteProjectWithDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1")
                .AddProject("Project2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC021", "Document2", "Workspace/Project2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC022", "Document3", "Workspace/Project2")
                .AddProject("Project3")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document4", "Workspace/Project3")
                .CreateWorkspace();

            interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Name == "Project1" &&
                ws.Projects[1].Name == "Project3"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method deletes a project, its folders and documents.
        /// </summary>
        [Test]
        public void TestDeleteProjectWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC1111", "Document1", "Workspace/Project1/Folder1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC1112", "Document2", "Workspace/Project1/Folder1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC1121", "Document3", "Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC1122", "Document4", "Workspace/Project1/Folder1/Folder2")
                .AddProject("Project2")
                .AddFolder("Workspace/Project2/Folder1")
                .AddFolder("Workspace/Project2/Folder1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC2111", "Document5", "Workspace/Project2/Folder1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC2112", "Document6", "Workspace/Project2/Folder1/Folder1")
                .AddFolder("Workspace/Project2/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC2121", "Document7", "Workspace/Project2/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CC2122", "Document8", "Workspace/Project2/Folder1/Folder2")
                .CreateWorkspace();

            interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project2/Folder1/Folder2" &&
                ws.Projects[0].Documents.Count == 4 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CC2111" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CC2112" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project2/Folder1/Folder2" &&
                ws.Projects[0].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CC2121" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project2/Folder1/Folder2" &&
                ws.Projects[0].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CC2122"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method throws an exception if the target project does not exist.
        /// </summary>
        [Test]
        public void TestDeleteNonExistentProjectThrowsException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateDeleteFolderUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            Assert.Throws<KeyNotFoundException>(() => interactor.Execute(new DeleteFolderUseCaseArgs(workspace, "Workspace/Project2")));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }
    }
}
