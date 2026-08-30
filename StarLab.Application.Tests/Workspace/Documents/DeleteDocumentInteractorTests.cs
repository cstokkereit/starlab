using StarLab.Tests;

namespace StarLab.Application.Workspace.Documents;

/// <summary>
/// A class for performing unit tests on the <see cref="DeleteDocumentInteractor"/> class.
/// </summary>
public class DeleteDocumentInteractorTests : ApplicationTests
{
    /// <summary>
    /// Test that the <see cref="DeleteDocumentInteractor.Execute"/> method correctly deletes a document from a folder within the project hierarchy.
    /// </summary>
    [Test]
    public void TestDeleteDocumentFromFolder()
    {
        var port = Substitute.For<IWorkspaceOutputPort>();

        var interactor = factory.CreateDeleteDocumentUseCase(port);

        var workspace = new WorkspaceDtoBuilder("Workspace")
            .AddProject("Project1")
            .AddFolder("Workspace/Project1/Folder1")
            .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1/Folder1")
            .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1/Folder1")
            .CreateWorkspace();

        port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("'Document1' will be deleted permanently."),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.OK);

        interactor.Execute(new DeleteDocumentUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1"));

        port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
            ws.Projects[0].Documents.Count == 1 &&
            ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2"));
    }

    /// <summary>
    /// Test that the <see cref="DeleteDocumentInteractor.Execute"/> method correctly deletes a document from the project folder.
    /// </summary>
    [Test]
    public void TestDeleteDocumentFromProject()
    {
        var port = Substitute.For<IWorkspaceOutputPort>();

        var interactor = factory.CreateDeleteDocumentUseCase(port);

        var workspace = new WorkspaceDtoBuilder("Workspace")
            .AddProject("Project1")
            .AddFolder("Workspace/Project1/Folder1")
            .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A1", "Document1", "Workspace/Project1")
            .AddChart("B997452E-AC89-40B5-B304-525F93CCC0A2", "Document2", "Workspace/Project1")
            .CreateWorkspace();

        port.ShowMessage(Arg.Any<string>(),
                         Arg.Is("'Document1' will be deleted permanently."),
                         Arg.Is(InteractionType.Warning),
                         Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.OK);

        interactor.Execute(new DeleteDocumentUseCaseArgs(workspace, "B997452E-AC89-40B5-B304-525F93CCC0A1"));

        port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
            ws.Projects[0].Documents.Count == 1 &&
            ws.Projects[0].Documents[0].ID == "B997452E-AC89-40B5-B304-525F93CCC0A2"));
    }

    /// <summary>
    /// Test that the <see cref="DeleteDocumentInteractor.Execute"/> method does nothing if the target document does not exist.
    /// </summary>
    [Test]
    public void TestDeleteNonExistentDocument()
    {
        var port = Substitute.For<IWorkspaceOutputPort>();

        var interactor = factory.CreateDeleteDocumentUseCase(port);

        var workspace = new WorkspaceDtoBuilder("Workspace")
            .AddProject("Project1")
            .AddFolder("Workspace/Project1/Folder1")
            .AddChart("1", "Document1", "Workspace/Project1/Folder1")
            .CreateWorkspace();

        interactor.Execute(new DeleteDocumentUseCaseArgs(workspace, "2"));

        port.DidNotReceive().ShowMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<InteractionType>(), Arg.Any<InteractionResponses>());

        port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
    }
}
