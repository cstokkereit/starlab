using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Microsoft.Extensions.Logging;
using StarLab.Application;
using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Application.Workspace.Documents.Charts.Overlays;
using StarLab.Data.Import;
using StarLab.Data.MongoDB;
using StarLab.Data.MongoDB.Import;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Options;
using StarLab.Serialisation;
using StarLab.UI;
using StarLab.UI.Options;
using StarLab.UI.Workspace;
using Stratosoft.Commands;
using static WeifenLuo.WinFormsUI.Docking.DockPanelExtender;

namespace StarLab.Presentation
{
    /// <summary>
    /// Base class for integration tests.
    /// </summary>
    public abstract class PresenterTests
    {
        protected WindsorContainer container; // The Inversion of Control (IoC) container.

        protected IViewFactory factory; // The view factory.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            factory = Substitute.For<IViewFactory>();

            InstallMapperClasses(container);
            InstallInfrastructureClasses(container);
            InstallApplicationClasses(container);
            InstallPersistenceClasses(container);
            InstallPresentationClasses(container);
            InstallUserInterfaceClasses(container);
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
        /// Registers the dependencies from assemblies within the application layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallApplicationClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>(),
                Component.For<IOverlayDataProvider>().ImplementedBy<OverlayDataProvider>(),
                Classes.FromAssemblyNamed("StarLab.Application").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Application").BasedOn<Profile>().WithServiceBase()
            );
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the infrastructure layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallInfrastructureClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<ISerialisationService>().ImplementedBy<SerialisationService>(),
                Classes.FromAssemblyNamed("StarLab.Serialisation").BasedOn<Profile>().WithServiceBase()
            );
        }

        /// <summary>
        /// Registers any AutoMapper profiles that it finds within the solution with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallMapperClasses(IWindsorContainer container)
        {
            // Register IConfigurationProvider with all registered profiles
            container.Register(Component.For<IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                }, new LoggerFactory());

            }).LifestyleSingleton());

            // Register IMapper with registered IConfigurationProvider
            container.Register(Component.For<IMapper>().UsingFactoryMethod(kernel => new Mapper(kernel.Resolve<IConfigurationProvider>(), kernel.Resolve)));
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the persistence layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallPersistenceClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IDatabaseImportProvider>().ImplementedBy<DatabaseImportProvider>(),
                Component.For<IFileImportProvider>().ImplementedBy<FileImportProvider>(),
                Component.For<IDatabaseManager>().ImplementedBy<DatabaseManager>(),
                Component.For<IQueryBuilder>().ImplementedBy<QueryBuilder>(),
                Classes.FromAssemblyNamed("StarLab.Data.MongoDB").Where(t => t.Name.EndsWith("Provider")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Data").Where(t => t.Name.EndsWith("Provider")).WithServiceDefaultInterfaces()
            );
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the presentation layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallPresentationClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IApplicationConfiguration>().ImplementedBy<ApplicationConfiguration>(),
                Component.For<IFactoryConfiguration>().ImplementedBy<FactoryConfiguration>(),
                Component.For<IServiceRegistry>().ImplementedBy<ServiceRegistry>(),
                Component.For<ISessionContext>().ImplementedBy<SessionContext>(),
                Classes.FromAssemblyNamed("StarLab.Presentation").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Presentation").Where(t => t.Name.EndsWith("UseCaseService")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Presentation").BasedOn<Profile>().WithServiceBase()
            );
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the user interface layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallUserInterfaceClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<ICommandManager>().ImplementedBy<CommandManager>().LifestyleTransient(),
                Component.For<IApplicationController>().ImplementedBy<ApplicationController>(),
                Component.For<IFloatWindowFactory>().ImplementedBy<FloatWindowFactory>(),
                Component.For<IUserSettings>().ImplementedBy<UserSettings>(),
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<IViewFactory>().Instance(factory)
            );
        }
    }
}
