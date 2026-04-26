using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Microsoft.Extensions.Logging;
using StarLab.Application;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Presentation.Workspace.WorkspaceExplorer;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// Base class for testing classes that are derived from <see cref="Presenter"/>.
    /// </summary>
    public abstract class PresentationTests
    {
        protected ICommandManager commands; // The command manager.

        protected IFactoryConfiguration configuration; // Configuration for the view and presenter factories.

        protected ISessionContext context; // The session context.

        protected IApplicationController controller; // The application controller.

        protected IEventAggregator events; // The event aggregator.

        protected IUseCaseFactory factory; // The use case factory.

        protected IServiceRegistry services; // The service registry.

        private WindsorContainer container; // The Inversion of Control (IoC) container.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            configuration = Substitute.For<IFactoryConfiguration>();
            controller = Substitute.For<IApplicationController>();
            commands = Substitute.For<ICommandManager>();
            context = Substitute.For<ISessionContext>();
            events = Substitute.For<IEventAggregator>();
            factory = Substitute.For<IUseCaseFactory>();

            container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            container.Register(Component.For<IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                }, new LoggerFactory());

            }).LifestyleSingleton());

            container.Register(Component.For<IMapper>().UsingFactoryMethod(kernel => new Mapper(kernel.Resolve<IConfigurationProvider>(), kernel.Resolve)));
            
            container.Register(
                Component.For<IUseCaseFactory>().Instance(factory),
                Component.For<IUseCaseService>().ImplementedBy<AddDocumentUseCaseService>(),
                Component.For<IUseCaseService>().ImplementedBy<ApplicationUseCaseService>(),
                Component.For<IUseCaseService>().ImplementedBy<ChartUseCaseService>(),
                Component.For<IUseCaseService>().ImplementedBy<ChartSettingsUseCaseService>(),
                Component.For<IUseCaseService>().ImplementedBy<WorkspaceExplorerUseCaseService>(),
                Component.For<IServiceRegistry>().ImplementedBy<ServiceRegistry>(),
                Classes.FromAssemblyNamed("StarLab.Application").BasedOn<Profile>().WithServiceBase(),
                Classes.FromAssemblyNamed("StarLab.Presentation").BasedOn<Profile>().WithServiceBase());

            services = container.Resolve<IServiceRegistry>();

            services.Initialise(controller);
        }

        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        [TearDown]
        public virtual void TearDown()
        {
            controller.Dispose();
            container.Dispose();
        }
    }
}
