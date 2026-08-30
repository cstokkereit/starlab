using StarLab.Tests;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddFolderInteractor"/> class.
    /// </summary>
    public class ClipboardInteractorTests : ApplicationTests
    {
        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly pastes a copied document to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCopyAndPasteADocument()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents[1].Name == "Document1" &&
                ws.Projects[0].Documents[1].ID != "B997452E-AC89-40B5-B304-525F93CCC0A1"));
        }

        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly renames and pastes the copied document to the same location within the workspace hierarchy as the original.
        /// </summary>
        [Test]
        public void TestCopyAndPasteADocumentWhenSourceAndDestinationAreSameFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1", "Workspace/Project1/Folder1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[1].Name == "Document1 - Copy" &&
                ws.Projects[0].Documents[1].ID != "B997452E-AC89-40B5-B304-525F93CCC0A1"));
        }

        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly pastes a copied folder to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCopyAndPasteAFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 4 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder3"));
        }

        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly pastes a copied folder and its child folders to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCopyAndPasteAFolderWithChildFolders()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 7 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder1/Folder12" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[4].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Folders[5].Path == "Workspace/Project1/Folder2/Folder1/Folder11" &&
                ws.Projects[0].Folders[6].Path == "Workspace/Project1/Folder2/Folder1/Folder12"));
        }

        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly pastes a copied folder and its documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCopyAndPasteAFolderWithDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents.Count == 4 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[2].Name == "Document1" &&
                ws.Projects[0].Documents[2].ID != "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[3].Name == "Document2" &&
                ws.Projects[0].Documents[3].ID != "B997452E-AC89-40B5-B304-525F93CCC0A2"));
        }

        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly pastes a copied folder containing child folders and documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCopyAndPasteFoldersWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder2/Folder21")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A5", "Document5", "Workspace/Project1/Folder2/Folder21")
                .AddFolder("Workspace/Project1/Folder3")
                .AddFolder("Workspace/Project1/Folder3/Folder31")
                .AddFolder("Workspace/Project1/Folder3/Folder32")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A6", "Document6", "Workspace/Project1/Folder3/Folder32")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder3/Folder32"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 11 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder1/Folder12" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[4].Path == "Workspace/Project1/Folder2/Folder21" &&
                ws.Projects[0].Folders[5].Path == "Workspace/Project1/Folder3" &&
                ws.Projects[0].Folders[6].Path == "Workspace/Project1/Folder3/Folder31" &&
                ws.Projects[0].Folders[7].Path == "Workspace/Project1/Folder3/Folder32" &&
                ws.Projects[0].Folders[8].Path == "Workspace/Project1/Folder3/Folder32/Folder1" &&
                ws.Projects[0].Folders[9].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder11" &&
                ws.Projects[0].Folders[10].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder12" &&
                ws.Projects[0].Documents.Count == 10 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Documents[2].Name == "Document3" &&
                ws.Projects[0].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder1/Folder12" &&
                ws.Projects[0].Documents[3].Name == "Document4" &&
                ws.Projects[0].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4" &&
                ws.Projects[0].Documents[4].Path == "Workspace/Project1/Folder2/Folder21" &&
                ws.Projects[0].Documents[4].Name == "Document5" &&
                ws.Projects[0].Documents[4].ID == "B997452E-AC89-40B5-B304-525F93CCC0A5" &&
                ws.Projects[0].Documents[5].Path == "Workspace/Project1/Folder3/Folder32" &&
                ws.Projects[0].Documents[5].Name == "Document6" &&
                ws.Projects[0].Documents[5].ID == "B997452E-AC89-40B5-B304-525F93CCC0A6" &&
                ws.Projects[0].Documents[6].Path == "Workspace/Project1/Folder3/Folder32/Folder1" &&
                ws.Projects[0].Documents[6].Name == "Document1" &&
                ws.Projects[0].Documents[6].ID != "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[7].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder11" &&
                ws.Projects[0].Documents[7].Name == "Document2" &&
                ws.Projects[0].Documents[7].ID != "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[0].Documents[8].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder11" &&
                ws.Projects[0].Documents[8].Name == "Document3" &&
                ws.Projects[0].Documents[8].ID != "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[0].Documents[9].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder12" &&
                ws.Projects[0].Documents[9].Name == "Document4" &&
                ws.Projects[0].Documents[9].ID != "B997452E-AC89-40B5-B304-525F93CCC0A4"));
        }

        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly pastes a copied folder and its documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCopyAndPasteAFolderFromOneProjectToAnother()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .AddProject("Project2")
                .AddFolder("Workspace/Project2/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project2/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project2/Folder2")
                .AddFolder("Workspace/Project2/Folder3")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 4 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder1/Folder12" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1/Folder12" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[1].Folders.Count == 5 &&
                ws.Projects[1].Folders[0].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Folders[1].Path == "Workspace/Project2/Folder3" &&
                ws.Projects[1].Folders[2].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Folders[3].Path == "Workspace/Project2/Folder1/Folder11" &&
                ws.Projects[1].Folders[4].Path == "Workspace/Project2/Folder1/Folder12" &&
                ws.Projects[1].Documents.Count == 4 &&
                ws.Projects[1].Documents[0].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Documents[0].Name == "Document3" &&
                ws.Projects[1].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[1].Documents[1].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Documents[1].Name == "Document4" &&
                ws.Projects[1].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4" &&
                ws.Projects[1].Documents[2].Path == "Workspace/Project2/Folder1/Folder11" &&
                ws.Projects[1].Documents[2].Name == "Document1" &&
                ws.Projects[1].Documents[2].ID != "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[1].Documents[3].Path == "Workspace/Project2/Folder1/Folder12" &&
                ws.Projects[1].Documents[3].Name == "Document2" &&
                ws.Projects[1].Documents[3].ID != "B997452E-AC89-40B5-B304-525F93CCC0A2"));
        }

        /// <summary>
        /// Test that the <see cref="CopyAndPasteInteractor.Execute"/> method correctly pastes a copied folder and its documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCopyAndPasteAFolderFromOneProjectToAFolderInAnotherProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .AddProject("Project2")
                .AddFolder("Workspace/Project2/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project2/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project2/Folder1")
                .AddFolder("Workspace/Project2/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCopyAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project2/Folder1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 4 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder1/Folder12" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1/Folder11" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1/Folder12" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[1].Folders.Count == 5 &&
                ws.Projects[1].Folders[0].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Folders[1].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[1].Folders[2].Path == "Workspace/Project2/Folder1/Folder1/Folder11" &&
                ws.Projects[1].Folders[3].Path == "Workspace/Project2/Folder1/Folder1/Folder12" &&
                ws.Projects[1].Folders[4].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Documents.Count == 4 &&
                ws.Projects[1].Documents[0].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Documents[0].Name == "Document3" &&
                ws.Projects[1].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[1].Documents[1].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Documents[1].Name == "Document4" &&
                ws.Projects[1].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4" &&
                ws.Projects[1].Documents[2].Path == "Workspace/Project2/Folder1/Folder1/Folder11" &&
                ws.Projects[1].Documents[2].Name == "Document1" &&
                ws.Projects[1].Documents[2].ID != "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[1].Documents[3].Path == "Workspace/Project2/Folder1/Folder1/Folder12" &&
                ws.Projects[1].Documents[3].Name == "Document2" &&
                ws.Projects[1].Documents[3].ID != "B997452E-AC89-40B5-B304-525F93CCC0A2"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method correctly pastes a cut document to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCutAndPasteADocument()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method refuses to paste the cut document to the same location within the workspace hierarchy as the original.
        /// </summary>
        [Test]
        public void TestCutAndPasteADocumentFailsIfSourceAndDestinationAreSameFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1", "Workspace/Project1/Folder1"));

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is("Cannot move 'Document1'. The destination folder is the same as the source folder."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method does nothing when an existing document with the same name already exists and the paste operation is cancelled.
        /// </summary>
        [Test]
        public void TestCutAndPasteADocumentWhenADocumentWithTheSameNameAlreadyExistsAndOperationCancelled()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document1", "Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            port.ShowMessage(Arg.Is("StarLab"),
                             Arg.Is("A document with the name 'Document1' already exists. Do you want to replace it?"),
                             Arg.Is(InteractionType.Error),
                             Arg.Is(InteractionResponses.YesNoCancel)).Returns(InteractionResult.Cancel);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1", "Workspace/Project1/Folder2"));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method deletes the cut document when the option to replace an existing document with the same name is not chosen.
        /// </summary>
        [Test]
        public void TestCutAndPasteADocumentWhenADocumentWithTheSameNameAlreadyExistsAndResponseIsNo()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document1", "Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            port.ShowMessage(Arg.Is("StarLab"),
                             Arg.Is("A document with the name 'Document1' already exists. Do you want to replace it?"),
                             Arg.Is(InteractionType.Error),
                             Arg.Is(InteractionResponses.YesNoCancel)).Returns(InteractionResult.No);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method replaces an existing document with the same name as the cut document when the option to replace the existing document is chosen.
        /// </summary>
        [Test]
        public void TestCutAndPasteADocumentWhenADocumentWithTheSameNameAlreadyExistsAndResponseIsYes()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document1", "Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            port.ShowMessage(Arg.Is("StarLab"),
                             Arg.Is("A document with the name 'Document1' already exists. Do you want to replace it?"),
                             Arg.Is(InteractionType.Error),
                             Arg.Is(InteractionResponses.YesNoCancel)).Returns(InteractionResult.Yes);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method correctly moves a cut folder to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder3"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method does nothing when an existing folder with the same name already exists and the paste operation is cancelled.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolderWhenAFolderWithTheSameNameAlreadyExistsAndOperationCancelled()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder3")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder2/Folder3")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            port.ShowMessage(Arg.Is("StarLab"),
                             Arg.Is("This folder already contains a folder called 'Folder3'.\r\n\r\nIf documents in the existing folder have the same names as documents in the folder you are copying, do you want to replace the existing documents?"),
                             Arg.Is(InteractionType.Error),
                             Arg.Is(InteractionResponses.YesNoCancel)).Returns(InteractionResult.Cancel);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1/Folder3", "Workspace/Project1/Folder2"));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method deletes the cut folder when the option to replace an existing folder with the same name is not chosen.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolderWhenAFolderWithTheSameNameAlreadyExistsAndResponseIsNo()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project1/Folder1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .AddFolder("Workspace/Project1/Folder3/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A5", "Document1", "Workspace/Project1/Folder3/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A6", "Document3", "Workspace/Project1/Folder3/Folder1")
                .AddFolder("Workspace/Project1/Folder3/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A7", "Document1", "Workspace/Project1/Folder3/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A8", "Document3", "Workspace/Project1/Folder3/Folder1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            port.ShowMessage(Arg.Is("StarLab"),
                             Arg.Is("This folder already contains a folder called 'Folder1'.\r\n\r\nIf documents in the existing folder have the same names as documents in the folder you are copying, do you want to replace the existing documents?"),
                             Arg.Is(InteractionType.Error),
                             Arg.Is(InteractionResponses.YesNoCancel)).Returns(InteractionResult.No);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder3"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder3" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents.Count == 6 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A5" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Documents[1].Name == "Document3" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A6" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Documents[2].Name == "Document2" &&
                ws.Projects[0].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents[3].Name == "Document1" &&
                ws.Projects[0].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CCC0A7" &&
                ws.Projects[0].Documents[4].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents[4].Name == "Document3" &&
                ws.Projects[0].Documents[4].ID == "B997452E-AC89-40B5-B304-525F93CCC0A8" &&
                ws.Projects[0].Documents[5].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents[5].Name == "Document4" &&
                ws.Projects[0].Documents[5].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method replaces an existing folder with the same name as the cut folder when the option to replace the existing folder is chosen.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolderWhenAFolderWithTheSameNameAlreadyExistsAndResponseIsYes()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project1/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project1/Folder1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .AddFolder("Workspace/Project1/Folder3/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A5", "Document1", "Workspace/Project1/Folder3/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A6", "Document3", "Workspace/Project1/Folder3/Folder1")
                .AddFolder("Workspace/Project1/Folder3/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A7", "Document1", "Workspace/Project1/Folder3/Folder1/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A8", "Document3", "Workspace/Project1/Folder3/Folder1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            port.ShowMessage(Arg.Is("StarLab"),
                             Arg.Is("This folder already contains a folder called 'Folder1'.\r\n\r\nIf documents in the existing folder have the same names as documents in the folder you are copying, do you want to replace the existing documents?"),
                             Arg.Is(InteractionType.Error),
                             Arg.Is(InteractionResponses.YesNoCancel)).Returns(InteractionResult.Yes);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder3"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder3" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents.Count == 6 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document3" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A6" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Documents[1].Name == "Document1" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder3/Folder1" &&
                ws.Projects[0].Documents[2].Name == "Document2" &&
                ws.Projects[0].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents[3].Name == "Document1" &&
                ws.Projects[0].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CCC0A7" &&
                ws.Projects[0].Documents[4].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents[4].Name == "Document3" &&
                ws.Projects[0].Documents[4].ID == "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[0].Documents[5].Path == "Workspace/Project1/Folder3/Folder1/Folder2" &&
                ws.Projects[0].Documents[5].Name == "Document4" &&
                ws.Projects[0].Documents[5].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method correctly pastes a cut folder and its child folders to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolderWithChildFolders()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 4 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder2/Folder1/Folder11" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder2/Folder1/Folder12"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method correctly pastes a cut folder and its documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolderWithDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method correctly pastes a cut folder containing child folders and documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCutAndPasteFoldersWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder2/Folder21")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A5", "Document5", "Workspace/Project1/Folder2/Folder21")
                .AddFolder("Workspace/Project1/Folder3")
                .AddFolder("Workspace/Project1/Folder3/Folder31")
                .AddFolder("Workspace/Project1/Folder3/Folder32")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A6", "Document6", "Workspace/Project1/Folder3/Folder32")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project1/Folder3/Folder32"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 8 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder21" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder3" &&
                ws.Projects[0].Folders[3].Path == "Workspace/Project1/Folder3/Folder31" &&
                ws.Projects[0].Folders[4].Path == "Workspace/Project1/Folder3/Folder32" &&
                ws.Projects[0].Folders[5].Path == "Workspace/Project1/Folder3/Folder32/Folder1" &&
                ws.Projects[0].Folders[6].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder11" &&
                ws.Projects[0].Folders[7].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder12" &&
                ws.Projects[0].Documents.Count == 6 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2/Folder21" &&
                ws.Projects[0].Documents[0].Name == "Document5" &&
                ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A5" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder3/Folder32" &&
                ws.Projects[0].Documents[1].Name == "Document6" &&
                ws.Projects[0].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A6" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder3/Folder32/Folder1" &&
                ws.Projects[0].Documents[2].Name == "Document1" &&
                ws.Projects[0].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder11" &&
                ws.Projects[0].Documents[3].Name == "Document2" &&
                ws.Projects[0].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2" &&
                ws.Projects[0].Documents[4].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder11" &&
                ws.Projects[0].Documents[4].Name == "Document3" &&
                ws.Projects[0].Documents[4].ID == "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[0].Documents[5].Path == "Workspace/Project1/Folder3/Folder32/Folder1/Folder12" &&
                ws.Projects[0].Documents[5].Name == "Document4" &&
                ws.Projects[0].Documents[5].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method correctly pastes a cut folder and its documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolderFromOneProjectToAnother()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .AddProject("Project2")
                .AddFolder("Workspace/Project2/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project2/Folder2")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project2/Folder2")
                .AddFolder("Workspace/Project2/Folder3")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project2"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 0 &&
                ws.Projects[1].Folders.Count == 5 &&
                ws.Projects[1].Folders[0].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Folders[1].Path == "Workspace/Project2/Folder3" &&
                ws.Projects[1].Folders[2].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Folders[3].Path == "Workspace/Project2/Folder1/Folder11" &&
                ws.Projects[1].Folders[4].Path == "Workspace/Project2/Folder1/Folder12" &&
                ws.Projects[1].Documents.Count == 4 &&
                ws.Projects[1].Documents[0].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Documents[0].Name == "Document3" &&
                ws.Projects[1].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[1].Documents[1].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Documents[1].Name == "Document4" &&
                ws.Projects[1].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4" &&
                ws.Projects[1].Documents[2].Path == "Workspace/Project2/Folder1/Folder11" &&
                ws.Projects[1].Documents[2].Name == "Document1" &&
                ws.Projects[1].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[1].Documents[3].Path == "Workspace/Project2/Folder1/Folder12" &&
                ws.Projects[1].Documents[3].Name == "Document2" &&
                ws.Projects[1].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2"));
        }

        /// <summary>
        /// Test that the <see cref="CutAndPasteInteractor.Execute"/> method correctly pastes a cut folder and its documents to the specified location within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCutAndPasteAFolderFromOneProjectToAFolderInAnotherProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder11")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1/Folder11")
                .AddFolder("Workspace/Project1/Folder1/Folder12")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1/Folder12")
                .AddFolder("Workspace/Project1/Folder2")
                .AddProject("Project2")
                .AddFolder("Workspace/Project2/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A3", "Document3", "Workspace/Project2/Folder1")
                .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A4", "Document4", "Workspace/Project2/Folder1")
                .AddFolder("Workspace/Project2/Folder2")
                .CreateWorkspace();

            var interactor = factory.CreateCutAndPasteUseCase(port);

            interactor.Execute(new ClipboardUseCaseArgs(workspace, "Workspace/Project1/Folder1", "Workspace/Project2/Folder1"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Documents.Count == 0 &&
                ws.Projects[1].Folders.Count == 5 &&
                ws.Projects[1].Folders[0].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Folders[1].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[1].Folders[2].Path == "Workspace/Project2/Folder1/Folder1/Folder11" &&
                ws.Projects[1].Folders[3].Path == "Workspace/Project2/Folder1/Folder1/Folder12" &&
                ws.Projects[1].Folders[4].Path == "Workspace/Project2/Folder2" &&
                ws.Projects[1].Documents.Count == 4 &&
                ws.Projects[1].Documents[0].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Documents[0].Name == "Document3" &&
                ws.Projects[1].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A3" &&
                ws.Projects[1].Documents[1].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[1].Documents[1].Name == "Document4" &&
                ws.Projects[1].Documents[1].ID == "B997452E-AC89-40B5-B304-525F93CCC0A4" &&
                ws.Projects[1].Documents[2].Path == "Workspace/Project2/Folder1/Folder1/Folder11" &&
                ws.Projects[1].Documents[2].Name == "Document1" &&
                ws.Projects[1].Documents[2].ID == "B997452E-AC89-40B5-B304-525F93CCC0A1" &&
                ws.Projects[1].Documents[3].Path == "Workspace/Project2/Folder1/Folder1/Folder12" &&
                ws.Projects[1].Documents[3].Name == "Document2" &&
                ws.Projects[1].Documents[3].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2"));
        }
    }
}
