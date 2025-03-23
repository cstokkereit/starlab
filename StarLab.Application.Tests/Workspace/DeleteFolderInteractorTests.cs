namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DeleteFolderInteractor"/> class.
    /// </summary>
    public class DeleteFolderInteractorTests : InteractorTests
    {
        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method correctly deletes an empty folder.
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
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddDocument("21", "Document2", "Workspace/Project1/Folder2")
                .AddDocument("22", "Document3", "Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .AddDocument("3", "Document4", "Workspace/Project1/Folder3")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("The folder 'Folder2' and all of its contents will be deleted permanently."),
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
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method does not delete a folder if the action is cancelled.
        /// </summary>
        [Test]
        public void TestDeleteFolderWithDocumentsCancelled()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddDocument("1", "Document1", "Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder2")
                .AddDocument("21", "Document2", "Workspace/Project1/Folder2")
                .AddDocument("22", "Document3", "Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder3")
                .AddDocument("3", "Document4", "Workspace/Project1/Folder3")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("The folder 'Folder2' and all of its contents will be deleted permanently."),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.Cancel);

            interactor.Execute(dto, "Workspace/Project1/Folder2");

            port.DidNotReceive().RemoveDocument(Arg.Is("21"));

            port.DidNotReceive().RemoveDocument(Arg.Is("22"));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
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
                .AddFolder("Workspace/Project1/Folder1/Folder1")
                .AddDocument("111", "Document1", "Workspace/Project1/Folder1/Folder1")
                .AddDocument("112", "Document2", "Workspace/Project1/Folder1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder2")
                .AddDocument("121", "Document3", "Workspace/Project1/Folder1/Folder2")
                .AddDocument("122", "Document4", "Workspace/Project1/Folder1/Folder2")
                .AddFolder("Workspace/Project1/Folder2")
                .AddFolder("Workspace/Project1/Folder2/Folder1")
                .AddDocument("211", "Document5", "Workspace/Project1/Folder2/Folder1")
                .AddDocument("212", "Document6", "Workspace/Project1/Folder2/Folder1")
                .AddFolder("Workspace/Project1/Folder2/Folder2")
                .AddDocument("221", "Document7", "Workspace/Project1/Folder2/Folder2")
                .AddDocument("222", "Document8", "Workspace/Project1/Folder2/Folder2")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("The folder 'Folder1' and all of its contents will be deleted permanently."),
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

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method does nothing if the target folder does not exist.
        /// </summary>
        [Test]
        public void TestDeleteNonExistentFolder()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .CreateWworkspace();

            interactor.Execute(dto, "Workspace/Project1/Folder2");

            port.DidNotReceive().ShowMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<InteractionType>(), Arg.Any<InteractionResponses>());

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="DeleteProjectInteractor.Execute"/> method correctly deletes an empty project.
        /// </summary>
        [Test]
        public void TestDeleteEmptyProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .CreateWworkspace();

            interactor.Execute(dto, "Workspace/Project1");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws => ws.Projects.Count == 0));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method gets confirmation and then correctly deletes a project and its documents.
        /// </summary>
        [Test]
        public void TestDeleteProjectWithDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddDocument("1", "Document1", "Workspace/Project1")
                .AddProject("Project2")
                .AddDocument("21", "Document2", "Workspace/Project2")
                .AddDocument("22", "Document3", "Workspace/Project2")
                .AddProject("Project3")
                .AddDocument("3", "Document4", "Workspace/Project3")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("The project 'Project2' and all of its contents will be deleted permanently."),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.OK);

            interactor.Execute(dto, "Workspace/Project2");

            port.Received().RemoveDocument(Arg.Is("21"));

            port.Received().RemoveDocument(Arg.Is("22"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Name == "Project1" &&
                ws.Projects[1].Name == "Project3"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method does not delete a project if the action is cancelled.
        /// </summary>
        [Test]
        public void TestDeleteProjectWithDocumentsCancelled()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddDocument("1", "Document1", "Workspace/Project1")
                .AddProject("Project2")
                .AddDocument("21", "Document2", "Workspace/Project2")
                .AddDocument("22", "Document3", "Workspace/Project2")
                .AddProject("Project3")
                .AddDocument("3", "Document4", "Workspace/Project3")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("The project 'Project2' and all of its contents will be deleted permanently."),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.Cancel);

            interactor.Execute(dto, "Workspace/Project2");

            port.DidNotReceive().RemoveDocument(Arg.Is("21"));

            port.DidNotReceive().RemoveDocument(Arg.Is("22"));

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method gets confirmation and then correctly deletes a project, its folders and documents.
        /// </summary>
        [Test]
        public void TestDeleteProjectWithChildFoldersAndDocuments()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .AddFolder("Workspace/Project1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder1")
                .AddDocument("1111", "Document1", "Workspace/Project1/Folder1/Folder1")
                .AddDocument("1112", "Document2", "Workspace/Project1/Folder1/Folder1")
                .AddFolder("Workspace/Project1/Folder1/Folder2")
                .AddDocument("1121", "Document3", "Workspace/Project1/Folder1/Folder2")
                .AddDocument("1122", "Document4", "Workspace/Project1/Folder1/Folder2")
                .AddProject("Project2")
                .AddFolder("Workspace/Project2/Folder1")
                .AddFolder("Workspace/Project2/Folder1/Folder1")
                .AddDocument("2111", "Document5", "Workspace/Project2/Folder1/Folder1")
                .AddDocument("2112", "Document6", "Workspace/Project2/Folder1/Folder1")
                .AddFolder("Workspace/Project2/Folder1/Folder2")
                .AddDocument("2121", "Document7", "Workspace/Project2/Folder1/Folder2")
                .AddDocument("2122", "Document8", "Workspace/Project2/Folder1/Folder2")
                .CreateWworkspace();

            port.ShowMessage(Arg.Any<string>(),
                             Arg.Is("The project 'Project1' and all of its contents will be deleted permanently."),
                             Arg.Is(InteractionType.Warning),
                             Arg.Is(InteractionResponses.OKCancel)).Returns(InteractionResult.OK);

            interactor.Execute(dto, "Workspace/Project1");

            port.Received().RemoveDocument(Arg.Is("1111"));
            port.Received().RemoveDocument(Arg.Is("1112"));
            port.Received().RemoveDocument(Arg.Is("1121"));
            port.Received().RemoveDocument(Arg.Is("1122"));

            port.DidNotReceive().RemoveDocument(Arg.Is("2111"));
            port.DidNotReceive().RemoveDocument(Arg.Is("2112"));
            port.DidNotReceive().RemoveDocument(Arg.Is("2121"));
            port.DidNotReceive().RemoveDocument(Arg.Is("2122"));

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 1 &&
                ws.Projects[0].Folders.Count == 3 &&
                ws.Projects[0].Folders[0].Path == "Workspace/Project2/Folder1" &&
                ws.Projects[0].Folders[1].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[0].Folders[2].Path == "Workspace/Project2/Folder1/Folder2" &&
                ws.Projects[0].Documents.Count == 4 &&
                ws.Projects[0].Documents[0].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[0].Documents[0].ID == "2111" &&
                ws.Projects[0].Documents[1].Path == "Workspace/Project2/Folder1/Folder1" &&
                ws.Projects[0].Documents[1].ID == "2112" &&
                ws.Projects[0].Documents[2].Path == "Workspace/Project2/Folder1/Folder2" &&
                ws.Projects[0].Documents[2].ID == "2121" &&
                ws.Projects[0].Documents[3].Path == "Workspace/Project2/Folder1/Folder2" &&
                ws.Projects[0].Documents[3].ID == "2122"));
        }

        /// <summary>
        /// Test that the <see cref="DeleteFolderInteractor.Execute"/> method does nothing if the target project does not exist.
        /// </summary>
        [Test]
        public void TestDeleteNonExistentProject()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateDeleteFolderUseCase(port);

            var dto = new DTOBuilder("Workspace")
                .AddProject("Project1")
                .CreateWworkspace();

            interactor.Execute(dto, "Workspace/Project2");

            port.DidNotReceive().ShowMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<InteractionType>(), Arg.Any<InteractionResponses>());

            port.DidNotReceive().UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }
    }
}
