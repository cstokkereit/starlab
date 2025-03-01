using StarLab.Application.Workspace;

namespace StarLab.Application
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DeleteFolderInteractor"/> class.
    /// </summary>
    public class DeleteFolderInteractorTests : InteractorTests
    {
        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method correctly deletes a folder.
        /// </summary>
        [Test]
        public void TestDeleteEmptyFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .CreateWworkspace();

            interactor.Execute(dto, "Workspace/Project1/Folder2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder3"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method gets confirmation and then correctly deletes a folder and its documents.
        /// </summary>
        [Test]
        public void TestDeleteFolderWithDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .AddDocument("21", "Document2", "Workspace/Project1/Folder2")
                .AddDocument("22", "Document3", "Workspace/Project1/Folder2")
                .AddDocument("3", "Document4", "Workspace/Project1/Folder3")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Any<string>(),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.OK);

            interactor.Execute(dto, "Workspace/Project1/Folder2");

            port.Received().RemoveDocument(Arg.Is("21"));

            port.Received().RemoveDocument(Arg.Is("22"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder3"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method does not delete a folder and its documents if the action is cancelled.
        /// </summary>
        [Test]
        public void TestDeleteFolderWithDocumentsCancelled()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .AddDocument("21", "Document2", "Workspace/Project1/Folder2")
                .AddDocument("22", "Document3", "Workspace/Project1/Folder2")
                .AddDocument("3", "Document4", "Workspace/Project1/Folder3")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Any<string>(),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.Cancel);

            interactor.Execute(dto, "Workspace/Project1/Folder2");

            port.DidNotReceive().RemoveDocument(Arg.Is("21"));

            port.DidNotReceive().RemoveDocument(Arg.Is("22"));

            port.DidNotReceive().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 2 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder3"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method gets confirmation and then correctly deletes a folder, its child folders and documents.
        /// </summary>
        [Test]
        public void TestDeleteFolderWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder2")
                .AddFolder("Workspace/Project1/Folder2/Folder1")
                .AddFolder("Workspace/Project1/Folder2/Folder2")
                .AddDocument("111", "Document1", "Workspace/Project1/Folder1/Folder1")
                .AddDocument("112", "Document2", "Workspace/Project1/Folder1/Folder1")
                .AddDocument("121", "Document3", "Workspace/Project1/Folder1/Folder2")
                .AddDocument("122", "Document4", "Workspace/Project1/Folder1/Folder2")
                .AddDocument("211", "Document5", "Workspace/Project1/Folder2/Folder1")
                .AddDocument("212", "Document6", "Workspace/Project1/Folder2/Folder1")
                .AddDocument("221", "Document7", "Workspace/Project1/Folder2/Folder2")
                .AddDocument("222", "Document8", "Workspace/Project1/Folder2/Folder2")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Any<string>(),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.OK);

            interactor.Execute(dto, "Workspace/Project1/Folder1");

            port.Received().RemoveDocument(Arg.Is("111"));
            port.Received().RemoveDocument(Arg.Is("112"));
            port.Received().RemoveDocument(Arg.Is("121"));
            port.Received().RemoveDocument(Arg.Is("122"));

            port.DidNotReceive().RemoveDocument(Arg.Is("211"));
            port.DidNotReceive().RemoveDocument(Arg.Is("212"));
            port.DidNotReceive().RemoveDocument(Arg.Is("221"));
            port.DidNotReceive().RemoveDocument(Arg.Is("222"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project1/Folder2" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project1/Folder2/Folder2" &&
                ws.Projects[0].Documents.Count == 4 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[0].ID == "211" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project1/Folder2/Folder1" &&
                ws.Projects[0].Documents[1].ID == "212" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project1/Folder2/Folder2" &&
                ws.Projects[0].Documents[2].ID == "221" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project1/Folder2/Folder2" &&
                ws.Projects[0].Documents[3].ID == "222"));
        }
    }
}
