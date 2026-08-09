using StarLab.Tests;

namespace StarLab.Application.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddDocumentInteractor"/> class.
    /// </summary>
    public class AddDocumentInteractorTests : ApplicationTests
    {
        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method correctly adds a document to a folder in a project in the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestAddDocumentToFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = "Table1",
                Path = "Workspace/Project1/Folder1",
                Type = "Table",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Table1" &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Type == "Table" &&
                ws.Projects[0].Documents[0].View == "View1"));

            port.Received().OpenDocument(Arg.Is("1"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method correctly adds a document to a project in the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestAddDocumentToProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = "Chart1",
                Path = "Workspace/Project1",
                Type = "Chart",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Chart1" &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1" &&
                ws.Projects[0].Documents[0].Type == "Chart" &&
                ws.Projects[0].Documents[0].View == "View1"));

            port.Received().OpenDocument(Arg.Is("1"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method shows an error message if a document with the same name already exists.
        /// </summary>
        [Test]
        public void TestAddDocumentWhenDocumentWithSameNameExists()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("1", "Chart1", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "2",
                Name = "Chart1",
                Path = "Workspace/Project1/Folder1",
                Type = "Chart",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Any<string>(),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method generates a default name if the chart name is an empty string.
        /// </summary>
        [Test]
        public void TestAddChartWhenNameIsAnEmptyString()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = string.Empty,
                Path = "Workspace/Project1/Folder1",
                Type = "Chart",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Chart" &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Type == "Chart" &&
                ws.Projects[0].Documents[0].View == "View1"));

            port.Received().OpenDocument(Arg.Is("1"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method generates a default name if the chart name is an empty string and a chart with the default name already exists.
        /// </summary>
        [Test]
        public void TestAddChartWhenNameIsAnEmptyStringAndChartAlreadyExists()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddChart("1", "View1", "Chart", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "2",
                Name = string.Empty,
                Path = "Workspace/Project1/Folder1",
                Type = "Chart",
                View = "View2"
            };

            interactor.Execute(workspace, document);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Chart" &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Type == "Chart" &&
                ws.Projects[0].Documents[0].View == "View1" &&
                ws.Projects[0].Documents[1].ID == "2" &&
                ws.Projects[0].Documents[1].Name == "Chart (2)" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[1].Type == "Chart" &&
                ws.Projects[0].Documents[1].View == "View2"));

            port.Received().OpenDocument(Arg.Is("2"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method shows an error message if the document name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestAddDocumentWhenNameIsInvalid()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = "Document1/",
                Path = "Workspace/Project1/Folder1",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Any<string>(),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method shows an error message if the document type is not recognised.
        /// </summary>
        [Test]
        public void TestAddDocumentWhenTypeIsInvalid()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = "Document1/",
                Path = "Workspace/Project1/Folder1",
                Type = "InvalidType",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Any<string>(),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method generates a default name if the table name is an empty string.
        /// </summary>
        [Test]
        public void TestAddTableWhenNameIsAnEmptyString()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "1",
                Name = string.Empty,
                Path = "Workspace/Project1/Folder1",
                Type = "Table",
                View = "View1"
            };

            interactor.Execute(workspace, document);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 1 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Table" &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Type == "Table" &&
                ws.Projects[0].Documents[0].View == "View1"));

            port.Received().OpenDocument(Arg.Is("1"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentInteractor.Execute"/> method generates a default name if the table name is an empty string and a table with the default name already exists.
        /// </summary>
        [Test]
        public void TestAddTableWhenNameIsAnEmptyStringAndTableAlreadyExists()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateAddDocumentUseCase(port);

            var workspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddTable("1", "View1", "Table", "Workspace/Project1/Folder1")
                .CreateWorkspace();

            var document = new DocumentDTO
            {
                ID = "2",
                Name = string.Empty,
                Path = "Workspace/Project1/Folder1",
                Type = "Table",
                View = "View2"
            };

            interactor.Execute(workspace, document);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Table" &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[0].Type == "Table" &&
                ws.Projects[0].Documents[0].View == "View1" &&
                ws.Projects[0].Documents[1].ID == "2" &&
                ws.Projects[0].Documents[1].Name == "Table (2)" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Documents[1].Type == "Table" &&
                ws.Projects[0].Documents[1].View == "View2"));

            port.Received().OpenDocument(Arg.Is("2"));
        }
    }
}
