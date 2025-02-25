using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;

namespace StarLab.Application
{
    /// <summary>
    /// A class for registering the injectable application dependencies with the WindsorContainer.
    /// </summary>
    internal class DependencyInstaller : IWindsorInstaller
    {
        /// <summary>
        /// Registers all of the application dependencies with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">An <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        /// <param name="store">An <see cref="IConfigurationStore"/> that provides configuration information.</param>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallMapperClasses(container);
            InstallInfrastructureClasses(container);
            InstallApplicationClasses(container);
            //InstallPresentationClasses(container);
            //InstallUserInterfaceClasses(container);
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the application layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        private void InstallApplicationClasses(IWindsorContainer container)
        {
            container.Register(
                //Component.For<IEventAggregator>().ImplementedBy<EventAggregator>(),
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
                //Component.For<Configuration.IConfigurationProvider>().ImplementedBy<ConfigurationProvider>(),
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
            container.Register(Component.For<AutoMapper.IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                });

            }).LifestyleSingleton());

            // Register IMapper with registered IConfigurationProvider
            container.Register(Component.For<IMapper>().UsingFactoryMethod(kernel => new Mapper(kernel.Resolve<AutoMapper.IConfigurationProvider>(), kernel.Resolve)));
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the presentation layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        //private void InstallPresentationClasses(IWindsorContainer container)
        //{
        //    container.Register(
        //        Component.For<IWindsorContainer>().Instance(container),
        //        Component.For<ICommandManager>().ImplementedBy<CommandManager>().LifestyleTransient(),
        //        Classes.FromAssemblyNamed("StarLab.Presentation").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
        //        Classes.FromAssemblyNamed("StarLab.Presentation").BasedOn<Profile>().WithServiceBase()
        //    );
        //}

        /// <summary>
        /// Registers the dependencies from assemblies within the user interface layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        /// <param name="container">The <see cref="IWindsorContainer"/> that will be used to register the dependencies.</param>
        //private void InstallUserInterfaceClasses(IWindsorContainer container)
        //{
        //    container.Register(
        //        Classes.FromAssemblyNamed("StarLab.UI").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
        //        Component.For<IApplicationController>().ImplementedBy<ApplicationController>()
        //    );
        //}
    }
}
