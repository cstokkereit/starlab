using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Microsoft.Extensions.Logging;
using StarLab.Application;
using StarLab.Presentation;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Presentation.Workspace.WorkspaceExplorer;
using StarLab.Serialisation;
using Stratosoft.Commands;

namespace StarLab.UI
{
    /// <summary>
    /// A class for registering the injectable application dependencies with the WindsorContainer.
    /// </summary>
    internal class ApplicationInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Registers all of the application dependencies with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        /// <param name="store">An <see cref="IConfigurationStore"/> that provides configuration information.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel, true));

            InstallMapperClasses(container);
            InstallInfrastructureClasses(container);
            InstallApplicationClasses(container);
            InstallPresentationClasses(container);
            InstallUserInterfaceClasses(container);
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the application layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallApplicationClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>(),
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
                Component.For<ISerialisationProvider>().ImplementedBy<SerialisationProvider>(),
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
        /// Registers the dependencies from assemblies within the presentation layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallPresentationClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IFactoryConfiguration>().ImplementedBy<FactoryConfiguration>(),
                Component.For<IUseCaseService>().ImplementedBy<ApplicationUseCaseService>(),
                Component.For<IUseCaseService>().ImplementedBy<ChartUseCaseService>(),
                Component.For<IUseCaseService>().ImplementedBy<ChartSettingsUseCaseService>(),
                Component.For<IUseCaseService>().ImplementedBy<WorkspaceExplorerUseCaseService>(),
                Component.For<IServiceRegistry>().ImplementedBy<ServiceRegistry>(),
                Classes.FromAssemblyNamed("StarLab.Presentation").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
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
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<IApplicationSettings>().ImplementedBy<ApplicationSettings>(),
                Component.For<IApplicationController>().ImplementedBy<ApplicationController>(),
                Component.For<ICommandManager>().ImplementedBy<CommandManager>().LifestyleTransient(),
                Classes.FromAssemblyNamed("StarLab.UI").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces()
            );
        }
    }
}
