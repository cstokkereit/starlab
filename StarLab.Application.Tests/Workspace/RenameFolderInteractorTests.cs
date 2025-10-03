using StarLab.Tests;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="RenameFolderInteractor"/> class.
    /// </summary>
    public class RenameFolderInteractorTests : InteractorTests
    {
        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method correctly renames a folder.
        /// </summary>
        [Test]
        public void TestRenameFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            interactor.Execute(dto, "Workspace/Project1/Folder1", "Folder2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2"));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method throws an exception if the new folder name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameFolderToEmptyStringThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace/Project1/Folder1", string.Empty));

            Assert.That(e.Message, Is.EqualTo("The folder name cannot be an empty string."));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method throws an exception if a folder with the new name already exists.
        /// </summary>
        [Test]
        public void TestRenameFolderToExistingNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace/Project1/Folder1", "Folder2"));

            Assert.That(e.Message, Is.EqualTo("Cannot rename 'Folder1' to 'Folder2' because a folder with that name already exists."));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method throws an exception if the new folder name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestRenameFolderToInvalidNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace/Project1/Folder1", "Folder1/"));

            Assert.That(e.Message, Is.EqualTo("Folder names cannot include any of the following:\r\n\r\n                               \\ / : * ? ' \" < > |\r\n\r\nPlease enter a valid name."));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method correctly renames a folder and its child folders.
        /// </summary>
        [Test]
        public void TestRenameFolderWithChildFolders()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .CreateWorkspace();

            interactor.Execute(dto, "Workspace/Project1/Folder1", "Folder2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder2/Folder12"));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method correctly renames a folder and its documents.
        /// </summary>
        [Test]
        public void TestRenameFolderWithDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .AddDocument("2", "Document2", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            interactor.Execute(dto, "Workspace/Project1/Folder1", "Folder2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents[1].Name == "Document2"));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method correctly renames a folder, its child folders and documents.
        /// </summary>
        [Test]
        public void TestRenameFolderWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder1/Folder11/Folder111")
                .AddFolder("Workspace/Project1/Folder1/Folder11/Folder112")
                .AddFolder("Workspace/Project1/Folder1/Folder12/Folder121")
                .AddFolder("Workspace/Project1/Folder1/Folder12/Folder122")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .AddDocument("2", "Document2", "Workspace/Project1/Folder1/Folder11")
                .AddDocument("3", "Document3", "Workspace/Project1/Folder1/Folder12")
                .AddDocument("4", "Document4", "Workspace/Project1/Folder1/Folder11/Folder111")
                .AddDocument("5", "Document5", "Workspace/Project1/Folder1/Folder11/Folder112")
                .AddDocument("6", "Document6", "Workspace/Project1/Folder1/Folder12/Folder121")
                .AddDocument("7", "Document7", "Workspace/Project1/Folder1/Folder12/Folder122")
                .CreateWorkspace();

            interactor.Execute(dto, "Workspace/Project1/Folder1", "Folder2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 7 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder2/Folder11/Folder111" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder2/Folder11/Folder112" &&
                ws.Projects[0].Folders[4].Path == "Workspace/Project1/Folder2/Folder12" &&
                ws.Projects[0].Folders[5].Path == "Workspace/Project1/Folder2/Folder12/Folder121" &&
                ws.Projects[0].Folders[6].Path == "Workspace/Project1/Folder2/Folder12/Folder122" &&
                ws.Projects[0].Documents.Count == 7 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder2/Folder11" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder2/Folder11/Folder111" &&
                ws.Projects[0].Documents[2].Name == "Document4" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder2/Folder11/Folder112" &&
                ws.Projects[0].Documents[3].Name == "Document5" &&
                ws.Projects[0].Documents[4].Path == "Workspace/Project1/Folder2/Folder12" &&
                ws.Projects[0].Documents[4].Name == "Document3" &&
                ws.Projects[0].Documents[5].Path == "Workspace/Project1/Folder2/Folder12/Folder121" &&
                ws.Projects[0].Documents[5].Name == "Document6" &&
                ws.Projects[0].Documents[6].Path == "Workspace/Project1/Folder2/Folder12/Folder122" &&
                ws.Projects[0].Documents[6].Name == "Document7"));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method correctly renames a child folder, its child folders and documents.
        /// </summary>
        [Test]
        public void TestRenameChildFolderWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder1/Folder11/Folder111")
                .AddFolder("Workspace/Project1/Folder1/Folder11/Folder112")
                .AddFolder("Workspace/Project1/Folder1/Folder12/Folder121")
                .AddFolder("Workspace/Project1/Folder1/Folder12/Folder122")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .AddDocument("2", "Document2", "Workspace/Project1/Folder1/Folder11")
                .AddDocument("3", "Document3", "Workspace/Project1/Folder1/Folder12")
                .AddDocument("4", "Document4", "Workspace/Project1/Folder1/Folder11/Folder111")
                .AddDocument("5", "Document5", "Workspace/Project1/Folder1/Folder11/Folder112")
                .AddDocument("6", "Document6", "Workspace/Project1/Folder1/Folder12/Folder121")
                .AddDocument("7", "Document7", "Workspace/Project1/Folder1/Folder12/Folder122")
                .CreateWorkspace();

            interactor.Execute(dto, "Workspace/Project1/Folder1/Folder12", "Folder2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 7 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder1/Folder11/Folder111" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder1/Folder11/Folder112" &&
                ws.Projects[0].Folders[4].Path == "Workspace/Project1/Folder1/Folder2" &&
                ws.Projects[0].Folders[5].Path == "Workspace/Project1/Folder1/Folder2/Folder121" &&
                ws.Projects[0].Folders[6].Path == "Workspace/Project1/Folder1/Folder2/Folder122" &&
                ws.Projects[0].Documents.Count == 7 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder1/Folder11/Folder111" &&
                ws.Projects[0].Documents[2].Name == "Document4" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder1/Folder11/Folder112" &&
                ws.Projects[0].Documents[3].Name == "Document5" &&
                ws.Projects[0].Documents[4].Path == "Workspace/Project1/Folder1/Folder2" &&
                ws.Projects[0].Documents[4].Name == "Document3" &&
                ws.Projects[0].Documents[5].Path == "Workspace/Project1/Folder1/Folder2/Folder121" &&
                ws.Projects[0].Documents[5].Name == "Document6" &&
                ws.Projects[0].Documents[6].Path == "Workspace/Project1/Folder1/Folder2/Folder122" &&
                ws.Projects[0].Documents[6].Name == "Document7"));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method correctly renames a project.
        /// </summary>
        [Test]
        public void TestRenameProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddProject("Project2")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder2/Folder21")
                .AddFolder("Workspace/Project1/Folder1/Folder11/Folder111")
                .AddFolder("Workspace/Project1/Folder1/Folder11/Folder112")
                .AddFolder("Workspace/Project2/Folder1")
                .AddFolder("Workspace/Project2/Folder2")
                .AddFolder("Workspace/Project2/Folder1/Folder11")
                .AddFolder("Workspace/Project2/Folder2/Folder21")
                .AddFolder("Workspace/Project2/Folder1/Folder11/Folder111")
                .AddFolder("Workspace/Project2/Folder1/Folder11/Folder112")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1/Folder11/Folder111")
                .AddDocument("2", "Document2", "Workspace/Project1/Folder1/Folder11/Folder111")
                .AddDocument("3", "Document3", "Workspace/Project1/Folder1/Folder11/Folder112")
                .AddDocument("4", "Document4", "Workspace/Project1/Folder1/Folder11/Folder112")
                .AddDocument("5", "Document5", "Workspace/Project2/Folder1/Folder11/Folder111")
                .AddDocument("6", "Document6", "Workspace/Project2/Folder1/Folder11/Folder111")
                .AddDocument("7", "Document7", "Workspace/Project2/Folder1/Folder11/Folder112")
                .AddDocument("8", "Document8", "Workspace/Project2/Folder1/Folder11/Folder112")
                .CreateWorkspace();

            interactor.Execute(dto, "Workspace/Project2", "Project3");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 6 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder1/Folder11/Folder111" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder1/Folder11/Folder112" &&
                ws.Projects[0].Folders[4].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[5].Path == "Workspace/Project1/Folder2/Folder21" &&
                ws.Projects[0].Documents.Count == 4 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1/Folder11/Folder111" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1/Folder11/Folder111" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder1/Folder11/Folder112" &&
                ws.Projects[0].Documents[2].Name == "Document3" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder1/Folder11/Folder112" &&
                ws.Projects[0].Documents[3].Name == "Document4" &&
                ws.Projects[1].Folders.Count == 6 &&
                ws.Projects[1].Folders[0].Path == "Workspace/Project3/Folder1" &&
                ws.Projects[1].Folders[1].Path == "Workspace/Project3/Folder1/Folder11" &&
                ws.Projects[1].Folders[2].Path == "Workspace/Project3/Folder1/Folder11/Folder111" &&
                ws.Projects[1].Folders[3].Path == "Workspace/Project3/Folder1/Folder11/Folder112" &&
                ws.Projects[1].Folders[4].Path == "Workspace/Project3/Folder2" &&
                ws.Projects[1].Folders[5].Path == "Workspace/Project3/Folder2/Folder21" &&
                ws.Projects[1].Documents.Count == 4 &&
                ws.Projects[1].Documents[0].Path == "Workspace/Project3/Folder1/Folder11/Folder111" &&
                ws.Projects[1].Documents[0].Name == "Document5" &&
                ws.Projects[1].Documents[1].Path == "Workspace/Project3/Folder1/Folder11/Folder111" &&
                ws.Projects[1].Documents[1].Name == "Document6" &&
                ws.Projects[1].Documents[2].Path == "Workspace/Project3/Folder1/Folder11/Folder112" &&
                ws.Projects[1].Documents[2].Name == "Document7" &&
                ws.Projects[1].Documents[3].Path == "Workspace/Project3/Folder1/Folder11/Folder112" &&
                ws.Projects[1].Documents[3].Name == "Document8"));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method throws an exception if the new project name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameProjectToEmptyStringThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace/Project1", string.Empty));

            Assert.That(e.Message, Is.EqualTo("The project name cannot be an empty string."));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method throws an exception if a project with the new name already exists.
        /// </summary>
        [Test]
        public void TestRenameProjectToExistingNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddProject("Project2")
                .CreateWorkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace/Project1", "Project2"));

            Assert.That(e.Message, Is.EqualTo("Cannot rename 'Project1' to 'Project2' because a project with that name already exists."));
        }

        /// <summary>
        /// Test that the <see cref="RenameFolderInteractor.Execute"/> method throws an exception if the new project name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestRenameProjectToInvalidNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameFolderUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .CreateWorkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace/Project1", "Project1/"));

            Assert.That(e.Message, Is.EqualTo("Project names cannot include any of the following:\r\n\r\n                               \\ / : * ? ' \" < > |\r\n\r\nPlease enter a valid name."));
        }
    }
}
