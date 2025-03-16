using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;

namespace StarLab.Application
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddDocumentInteractor"/> class.
    /// </summary>
    public class AddDocumentInteractorTests : InteractorTests
    {
        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method correctly adds a document to a project in the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestAddDocument()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var interactor = Factory.CreateAddDocumentUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWworkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = "Document1",
                Path = "Workspace/Project1/Folder1",
                View = "View1"
            };
            
            interactor.Execute(workspace, document);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].View == "View1"));

            port.Received().OpenDocument(Arg.Is("1"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method shows an error message if a document with the same name already exists.
        /// </summary>
        [Test]
        public void TestAddDocumentWhenDocumentWithSameNameExists()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var interactor = Factory.CreateAddDocumentUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .CreateWworkspace();

            var document = new DocumentDTO
            {
                ID = "2",
                Name = "Document1",
                Path = "Workspace/Project1/Folder1",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is("A document with the name 'Document1' already exists at this location.\r\nPlease provide a unique name for the document."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method shows an error message if the document name is an empty string.
        /// </summary>
        [Test]
        public void TestAddDocumentWhenNameIsAnEmptyString()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var interactor = Factory.CreateAddDocumentUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWworkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = string.Empty,
                Path = "Workspace/Project1/Folder1",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is("The document name cannot be an empty string."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method shows an error message if the document name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestAddDocumentWhenNameIsInvalid()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var interactor = Factory.CreateAddDocumentUseCase(port);

            var workspace = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWworkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = "Document1/",
                Path = "Workspace/Project1/Folder1",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is("Document names cannot include any of the following:\r\n\r\n                               \\ / : * ? ' \" < > |\r\n\r\nPlease enter a valid name."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }
    }
}
