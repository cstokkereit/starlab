using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using StarLab.Application;
using StarLab.Application.Workspace;
using System.Reflection;

namespace StarLab
{
    /// <summary>
    /// Base class for testing classes derived from <see cref="UseCaseInteractor{TOutputPort}"/>.
    /// </summary>
    public class InteractorTests
    {
        private readonly string resources; // The path to the test resources folder.

        private readonly string folder; // The path to the output folder.

        private WindsorContainer container; // The container used to resolve dependencies.

        /// <summary>
        /// Initialises a new instance of the <see cref="RenameWorkspaceInteractorTests"/> class.
        /// </summary>
        public InteractorTests()
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
            catch (Exception)
            {
                Assert.Fail("Initialisation Failed.");
            }
        }

        /// <summary>
        /// This will be run before each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new DependencyInstaller());
        }

        /// <summary>
        /// This will be run after each test.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            container.Dispose();
        }

        /// <summary>
        /// 
        /// </summary>
        protected IUseCaseFactory Factory => container.Resolve<IUseCaseFactory>();

        /// <summary>
        /// 
        /// </summary>
        protected string Folder => folder;

        /// <summary>
        /// 
        /// </summary>
        protected string Resources => resources;

        /// <summary>
        /// Copies the specified source file to the specified destination.
        /// </summary>
        /// <param name="source">The path to the source file.</param>
        /// <param name="destination">The path to the destination file.</param>
        protected static void CopyFile(string source, string destination)
        {
            File.Copy(source, destination, true);
        }

        /// <summary>
        /// Deletes the specified file.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        protected static void DeleteFile(string filename)
        {
            if (File.Exists(filename)) File.Delete(filename);
        }
    }
}
