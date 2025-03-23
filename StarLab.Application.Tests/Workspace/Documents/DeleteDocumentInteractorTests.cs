using StarLab.Application.Workspace;

namespace StarLab.Application.Workspace.Documents;

/// <summary>
/// A class for performing unit tests on the <see cref="DeleteDocumentInteractor"/> class.
/// </summary>
public class DeleteDocumentInteractorTests : InteractorTests
{
    /// <summary>
    /// Test that the <see cref="DeleteDocumentInteractor.Execute"/> method correctly deletes a document.
    /// </summary>
    [Test]
    public void TestDeleteDocument()
    {
        var port = Substitute.For<IWorkspaceOutputPort>();

        var interactor = Factory.CreateDeleteDocumentUseCase(port);

        var dto = new DTOBuilder("Workspace")
            .AddProject("Project1")
            .AddFolder("Workspace/Project1/Folder1")
            .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
            .AddDocument("2", "Document2", "Workspace/Project1/Folder1")
            .CreateWworkspace();

        port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("'Document1' will be deleted permanently."),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.OK);

        interactor.Execute(dto, "1");

        port.Received().RemoveDocument(Arg.Is("1"));

        port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
            ws.Projects[0].Documents.Count == 1 &&
            ws.Projects[0].Documents[0].ID == "2"));
    }

    /// <summary>
    /// Test that the <see cref="DeleteDocumentInteractor.Execute"/> method does nothing if the target document does not exist.
    /// </summary>
    [Test]
    public void TestDeleteNonExistentDocument()
    {
        var port = Substitute.For<IWorkspaceOutputPort>();

        var interactor = Factory.CreateDeleteDocumentUseCase(port);

        var dto = new DTOBuilder("Workspace")
            .AddProject("Project1")
            .AddFolder("Workspace/Project1/Folder1")
            .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
            .CreateWworkspace();

        interactor.Execute(dto, "2");

        port.DidNotReceive().ShowMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<InteractionType>(), Arg.Any<InteractionResponses>());

        port.DidNotReceive().RemoveDocument(Arg.Any<string>());

        port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
    }
}
