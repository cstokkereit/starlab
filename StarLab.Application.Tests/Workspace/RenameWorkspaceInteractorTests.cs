namespace StarLab.Application.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="RenameWorkspaceInteractor"/> class.
    /// </summary>
    public class RenameWorkspaceInteractorTests : InteractorTests
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
                DeleteFile(Path.Combine(Folder, "Workspace2.slw"));
            }
            catch (Exception)
            {
                Assert.Fail("TearDown Failed.");
            }

            base.TearDown();
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method correctly renames the workspace.
        /// </summary>
        [Test]
        public void TestRenameWorkspace()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(Folder, "Workspace1.slw")).CreateWworkspace();

            interactor.Execute(dto, "Workspace2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws => ws.FileName == Path.Combine(Folder, "Workspace2.slw")));
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method throws an exception if the new workspace name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameWorkspaceToEmptyStringThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(Folder, "Workspace1.slw")).CreateWworkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, string.Empty));

            Assert.That(e.Message, Is.EqualTo("The workspace name cannot be an empty string."));
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method throws an exception if a workspace with the new name already exists.
        /// </summary>
        [Test]
        public void TestRenameWorkspaceToExistingNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(Folder, "Workspace1.slw")).CreateWworkspace();

            CopyFile(Path.Combine(Resources, "Workspace2.slw"), Path.Combine(Folder, "Workspace2.slw"));

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace2"));

            Assert.That(e.Message, Is.EqualTo("Cannot rename 'Workspace1' to 'Workspace2' because a workspace with that name already exists at this location."));
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method throws an exception if the new workspace name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestRenameWorkspaceToInvalidNameThrowsAnException()
        {
            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = Factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(Folder, "Workspace1.slw")).CreateWworkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace1/"));

            Assert.That(e.Message, Is.EqualTo("Workspace names cannot include any of the following:\r\n\r\n                               \\ / : * ? ' \" < > |\r\n\r\nPlease enter a valid name."));
        }
    }
}
