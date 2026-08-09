namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="OpenWorkspaceInteractor"/> class.
    /// </summary>
    public class OpenWorkspaceInteractorTests : ApplicationTests
    {
        /// <summary>
        /// This will be run after each test.
        /// </summary>
        [TearDown]
        public override void TearDown()
        {
            try
            {
                DeleteFile(Path.Combine(folder, "Invalid.slw"));
            }
            catch (Exception)
            {
                Assert.Fail("TearDown Failed.");
            }

            base.TearDown();
        }

        /// <summary>
        /// Test that the <see cref="OpenWorkspaceInteractor.Execute"/> method correctly opens a workspace.
        /// </summary>
        [Test]
        public void TestOpenWorkspace()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var interactor = factory.CreateOpenWorkspaceUseCase(port);

            var filename = Path.Combine(folder, "Workspace1.slw");

            CopyFile(Path.Combine(resources, "Workspace1.slw"), filename);

            interactor.Execute(filename);

            port.Received().SetWorkspace(Arg.Is<WorkspaceDTO>(ws => ws.FileName == Path.Combine(folder, "Workspace1.slw")));
        }

        /// <summary>
        /// Test that the <see cref="OpenWorkspaceInteractor.Execute"/> method displays an error message if the workspace file cannot be loaded.
        /// </summary>
        [Test]
        public void TestOpenInvalidWorkspaceFileDisplaysAnErrorMessage()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var interactor = factory.CreateOpenWorkspaceUseCase(port);

            var filename = Path.Combine(folder, "Invalid.slw");

            CopyFile(Path.Combine(resources, "Invalid.slw"), filename);

            interactor.Execute(filename);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is($"The file {filename} could not be opened."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));
        }

        /// <summary>
        /// Test that the <see cref="OpenWorkspaceInteractor.Execute"/> method displays an error message if the workspace file does not exist.
        /// </summary>
        [Test]
        public void TestOpenNonExistentWorkspaceFileDisplaysAnErrorMessage()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var interactor = factory.CreateOpenWorkspaceUseCase(port);

            var filename = Path.Combine(folder, "Missing.slw");

            interactor.Execute(filename);

            port.Received().ShowMessage(Arg.Is("StarLab"),
                                        Arg.Is($"The file {filename} could not be found.\r\nCheck the filename and try again."),
                                        Arg.Is(InteractionType.Error),
                                        Arg.Is(InteractionResponses.OK));
        }
    }
}
