using StarLab.Tests;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="RenameDocumentInteractor"/> class.
    /// </summary>
    public class RenameDocumentInteractorTests : ApplicationTests
    {
        /// <summary>
        /// Test that the <see cref="RenameDocumentInteractor.Execute"/> method correctly renames a document.
        /// </summary>
        [Test]
        public void TestRenameDocument()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameDocumentUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("1", "Document1", "Workspace/Project1/Folder1")
                .AddChart("2", "Document2", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            interactor.Execute(dto, "1", "Document3");

            port.Received().UpdateDocument(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Name == "Document3" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[1].Name == "Document2"), Arg.Is("1"));
        }

        /// <summary>
        /// Test that the <see cref="RenameDocumentInteractor.Execute"/> method throws an exception if the new document name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameDocumentToEmptyStringThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameDocumentUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("1", "Document1", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            Assert.Throws<Exception>(() => interactor.Execute(dto, "1", string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="RenameDocumentInteractor.Execute"/> method throws an exception if a document with the new name already exists.
        /// </summary>
        [Test]
        public void TestRenameDocumentToExistingNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameDocumentUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("1", "Document1", "Workspace/Project1/Folder1")
                .AddChart("2", "Document2", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            Assert.Throws<Exception>(() => interactor.Execute(dto, "1", "Document2"));
        }

        /// <summary>
        /// Test that the <see cref="RenameDocumentInteractor.Execute"/> method throws an exception if the new document name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestRenameDocumentToInvalidNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameDocumentUseCase(port);

            var dto = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("1", "Document1", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            Assert.Throws<Exception>(() => interactor.Execute(dto, "1", "Document1/"));
        }
    }
}
