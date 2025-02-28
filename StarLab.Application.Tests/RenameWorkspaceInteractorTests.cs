using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using StarLab.Application.Workspace;
using System.Reflection;

namespace StarLab.Application
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="RenameWorkspaceInteractor"/> class.
    /// </summary>
    public class RenameWorkspaceInteractorTests
    {
        private readonly string resources; // The path to the test resources folder.

        private readonly string folder; // The path to the output folder.

        private WindsorContainer container; // The container used to resolve dependencies.

        /// <summary>
        /// Initialises a new instance of the <see cref="RenameWorkspaceInteractorTests"/> class.
        /// </summary>
        public RenameWorkspaceInteractorTests()
        {
            resources = string.Empty;
            folder = string.Empty;

            try
            {
                var location = Directory.GetParent(Assembly.GetExecutingAssembly().Location);

                if (location != null)
                {
                    folder = location.FullName;
                }

                resources = Path.GetFullPath($"{folder}..\\..\\..\\..\\Resources");
            }
            catch(Exception)
            {
                Assert.Fail();
            }
        }

        /// <summary>
        /// This will be run before each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new DependencyInstaller());
        }

        /// <summary>
        /// This will be run after each test.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            try
            {
                DeleteFile(Path.Combine(folder, "Workspace1.slw"));
                DeleteFile(Path.Combine(folder, "Workspace2.slw"));
            }
            catch (Exception)
            {
                Assert.Fail("TearDown Failed.");
            }

            container.Dispose();
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method correctly renames the workspace.
        /// </summary>
        [Test]
        public void TestRenameWorkspace()
        {
            var factory = container.Resolve<IUseCaseFactory>();

            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(folder, "Workspace1.slw")).CreateWworkspace();

            interactor.Execute(dto, "Workspace2");

            port.Received().UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws => ws.FileName == Path.Combine(folder, "Workspace2.slw")));
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method throws an exception if the new workspace name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameWorkspaceToEmptyStringThrowsAnException()
        {
            var factory = container.Resolve<IUseCaseFactory>();

            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(folder, "Workspace1.slw")).CreateWworkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, string.Empty));

            Assert.That(e.Message, Is.EqualTo("The workspace name cannot be an empty string."));
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method throws an exception if a workspace with the new name already exists.
        /// </summary>
        [Test]
        public void TestRenameWorkspaceToExistingNameThrowsAnException()
        {
            var factory = container.Resolve<IUseCaseFactory>();

            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(folder, "Workspace1.slw")).CreateWworkspace();

            CopyFile(Path.Combine(resources, "Workspace2.slw"), Path.Combine(folder, "Workspace2.slw"));

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Workspace2"));

            Assert.That(e.Message, Is.EqualTo("Cannot rename 'Workspace1' to 'Workspace2' because a workspace with that name already exists at this location."));
        }

        /// <summary>
        /// Test that the <see cref="RenameWorkspaceInteractor.Execute"/> method throws an exception if the new workspace name contains one or more illegal characters.
        /// </summary>
        [Test]
        public void TestRenameWorkspaceToInvalidNameThrowsAnException()
        {
            var factory = container.Resolve<IUseCaseFactory>();

            var port = Substitute.For<IWorkspaceOutputPort>();

            var interactor = factory.CreateRenameWorkspaceUseCase(port);

            var dto = new DTOBuilder(Path.Combine(folder, "Workspace1.slw")).CreateWworkspace();

            var e = Assert.Throws<Exception>(() => interactor.Execute(dto, "Wworkspace1/"));

            Assert.That(e.Message, Is.EqualTo("Workspace names cannot include any of the following:\r\n\r\n                               \\ / : * ? ' \" < > |\r\n\r\nPlease enter a valid name."));
        }

        /// <summary>
        /// Copies the specified source file to the specified destination.
        /// </summary>
        /// <param name="source">The path to the source file.</param>
        /// <param name="destination">The path to the destination file.</param>
        private static void CopyFile(string source, string destination)
        {
            File.Copy(source, destination, true);
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        private static void DeleteFile(string filename)
        {
            if (File.Exists(filename)) File.Delete(filename);
        }
    }
}
