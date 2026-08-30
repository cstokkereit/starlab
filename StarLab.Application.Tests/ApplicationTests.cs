using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Microsoft.Extensions.Logging;
using StarLab.Application.Data;
using StarLab.Data.MongoDB;
using StarLab.Serialisation;
using System.Reflection;

namespace StarLab.Application
{
    /// <summary>
    /// Base class for testing classes from the StarLab.Application namespace.
    /// </summary>
    public abstract class ApplicationTests
    {
        protected readonly string resources; // The path to the test resources folder.

        protected readonly string folder; // The path to the output folder.

        protected IUseCaseFactory factory; // The use case factory.

        private WindsorContainer container; // The Inversion of Control (IoC) container.

        /// <summary>
        /// Initialises a new instance of the <see cref="ApplicationTests"/> class.
        /// </summary>
        public ApplicationTests()
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
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            container = new WindsorContainer();

            container.Register(Component.For<AutoMapper.IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                }, new LoggerFactory());

            }).LifestyleSingleton());

            container.Register(Component.For<IMapper>().UsingFactoryMethod(kernel => new Mapper(kernel.Resolve<AutoMapper.IConfigurationProvider>(), kernel.Resolve)));

            container.Register(
                Component.For<IDatabaseManager>().ImplementedBy<DatabaseManager>(),
                Component.For<ISerialisationProvider>().ImplementedBy<SerialisationProvider>(),
                Classes.FromAssemblyNamed("StarLab.Serialisation").BasedOn<Profile>().WithServiceBase(),
                Classes.FromAssemblyNamed("StarLab.Application").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Application").BasedOn<Profile>().WithServiceBase()
            );

            factory = container.Resolve<IUseCaseFactory>();
        }

        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            container.Dispose();
        }

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
