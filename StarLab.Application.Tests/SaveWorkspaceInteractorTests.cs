using StarLab.Application.Workspace;

namespace StarLab.Application
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="SaveWorkspaceInteractor"/> class.
    /// </summary>
    public class SaveWorkspaceInteractorTests : InteractorTests
    {
        /// <summary>
        /// This will be run after each test.
        /// </summary>
        [TearDown]
        public override void TearDown()
        {
            try
            {
                DeleteFile(Path.Combine(Folder, "Workspace1.slw"));
            }
            catch (Exception)
            {
                Assert.Fail("TearDown Failed.");
            }

            base.TearDown();
        }

        /// <summary>
        /// Test that the <see cref="SaveWorkspaceInteractor.Execute"/> method correctly saves a workspace.
        /// </summary>
        [Test]
        public void TestSaveWorkspace()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateSaveWorkspaceUseCase(port);

            var filename = Path.Combine(Folder, "Workspace1.slw");

            var dto = new DTOBuilder(filename)
                .AddProject("Project1")
                .AddFolder("Workspace1/Project1/Folder1")
                .AddDocument("1", "Document1", "Workspace1/Project1/Folder1")
                .CreateWworkspace();

            interactor.Execute(dto);

            port.DidNotReceive().ShowMessage(Arg.Is("StarLab"),
                                             Arg.Any<string>(),
                                             Arg.Any<InteractionType>(),
                                             Arg.Any<InteractionResponses>());

            Assert.That(File.Exists(filename), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="SaveWorkspaceInteractor.Execute"/> method correctly saves over an existing workspace with the same name.
        /// </summary>
        [Test]
        public void TestSaveExistingWorkspace()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateSaveWorkspaceUseCase(port);

            var filename = Path.Combine(Folder, "Workspace1.slw");

            var dto = new DTOBuilder(filename)
                .AddProject("Project1")
                .AddFolder("Workspace1/Project1/Folder1")
                .AddDocument("1", "Document1", "Workspace1/Project1/Folder1")
                .CreateWworkspace();

            CopyFile(Path.Combine(Resources, "Workspace1.slw"), filename);

            interactor.Execute(dto);

            port.DidNotReceive().ShowMessage(Arg.Is("StarLab"),
                                             Arg.Any<string>(),
                                             Arg.Any<InteractionType>(),
                                             Arg.Any<InteractionResponses>());

            Assert.That(File.Exists(filename), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="SaveWorkspaceInteractor.Execute"/> method saves a workspace that can subsequently be reopened.
        /// </summary>
        [Test]
        public void TestSavedWorkspaceCanBeOpened()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var saveInteractor = Factory.CreateSaveWorkspaceUseCase(port);

            var filename = Path.Combine(Folder, "Workspace1.slw");

            var dto = new DTOBuilder(filename)
                .AddProject("Project1")
                .AddFolder("Workspace1/Project1/Folder1")
                .AddDocument("1", "Document1", "Workspace1/Project1/Folder1")
                .AddDocument("2", "Document2", "Workspace1/Project1/Folder1")
                .AddProject("Project2")
                .AddFolder("Workspace1/Project2/Folder1")
                .AddDocument("3", "Document3", "Workspace1/Project2/Folder1")
                .AddDocument("4", "Document4", "Workspace1/Project2/Folder1")
                .CreateWworkspace();

            saveInteractor.Execute(dto);

            port.DidNotReceive().ShowMessage(Arg.Is("StarLab"),
                                             Arg.Any<string>(),
                                             Arg.Any<InteractionType>(),
                                             Arg.Any<InteractionResponses>());

            var openInteractor = Factory.CreateOpenWorkspaceUseCase(port);

            openInteractor.Execute(filename);

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws =>
                ws.Projects.Count == 2 &&
                ws.Projects[0].Folders.Count == 1 &&
                ws.Projects[0].Folders[0].Path == "Workspace1/Project1/Folder1" &&
                ws.Projects[0].Documents.Count == 2 &&
                ws.Projects[0].Documents[0].ID == "1" &&
                ws.Projects[0].Documents[0].Name == "Document1" &&
                ws.Projects[0].Documents[0].Path == "Workspace1/Project1/Folder1" &&
                ws.Projects[0].Documents[1].ID == "2" &&
                ws.Projects[0].Documents[1].Name == "Document2" &&
                ws.Projects[0].Documents[1].Path == "Workspace1/Project1/Folder1" &&
                ws.Projects[1].Folders.Count == 1 &&
                ws.Projects[1].Folders[0].Path == "Workspace1/Project2/Folder1" &&
                ws.Projects[1].Documents.Count == 2 &&
                ws.Projects[1].Documents[0].ID == "3" &&
                ws.Projects[1].Documents[0].Name == "Document3" &&
                ws.Projects[1].Documents[0].Path == "Workspace1/Project2/Folder1" &&
                ws.Projects[1].Documents[1].ID == "4" &&
                ws.Projects[1].Documents[1].Name == "Document4" &&
                ws.Projects[1].Documents[1].Path == "Workspace1/Project2/Folder1"));
        }
    }
}
