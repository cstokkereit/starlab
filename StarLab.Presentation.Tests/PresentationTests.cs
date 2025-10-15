using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers;
using Castle.Windsor;
using StarLab.Application;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// Base class for testing classes that are derived from <see cref="Presenter"/>.
    /// </summary>
    public abstract class PresentationTests
    {
        protected WindsorContainer container; // The Inversion of Control (IoC) container.

        protected IApplicationController controller; // The application controller.

        protected IApplicationSettings settings; // The application settings.

        protected ICommandManager commands; // The command manager.

        protected IEventAggregator events; // The event aggregator.

        protected IUseCaseFactory factory; // The use case factory.

        protected IMapper mapper; // The object mapper.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            container = new WindsorContainer();

            // Register IConfigurationProvider with all registered profiles
            container.Register(Component.For<IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                });

            }).LifestyleSingleton());

            // Register IMapper with registered IConfigurationProvider
            container.Register(Component.For<IMapper>().UsingFactoryMethod(kernel => new Mapper(kernel.Resolve<IConfigurationProvider>(), kernel.Resolve)));

            container.Register(Classes.FromAssemblyNamed("StarLab.Presentation").BasedOn<Profile>().WithServiceBase());

            controller = Substitute.For<IApplicationController>();

            settings = Substitute.For<IApplicationSettings>();

            commands = Substitute.For<ICommandManager>();

            factory = Substitute.For<IUseCaseFactory>();

            events = Substitute.For<IEventAggregator>();

            mapper = container.Resolve<IMapper>();

            // Register ILazyComponentLoader last
            container.Register(Component.For<ILazyComponentLoader>().ImplementedBy<LazyComponentAutoMocker>());
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
        /// Uses <see cref="NSubstitute"/> to create mocked dependencies on demand.
        /// </summary>
        private class LazyComponentAutoMocker : ILazyComponentLoader
        {
            public IRegistration Load(string name, Type service, Castle.MicroKernel.Arguments arguments)
            {
                return Component.For(service).Instance(Substitute.For([service], null));
            }
        }
    }
}
