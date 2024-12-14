using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using StarLab.Application.Configuration;
using StarLab.Commands;

namespace StarLab.Application
{
    internal class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallMapperClasses(container);
            InstallInfrastructureClasses(container);
            InstallApplicationClasses(container);
            InstallPresentationClasses(container);
            InstallUserInterfaceClasses(container);
        }

        private void InstallApplicationClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>(),
                Classes.FromAssemblyNamed("StarLab.Application").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Application").BasedOn<Profile>().WithServiceBase()
            );
        }

        private void InstallInfrastructureClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IConfigurationService>().ImplementedBy<ConfigurationService>(),
                Component.For<ISerialisationService>().ImplementedBy<SerialisationService>(),
                Classes.FromAssemblyNamed("StarLab.Serialisation").BasedOn<Profile>().WithServiceBase()
            );
        }

        private void InstallMapperClasses(IWindsorContainer container)
        {
            // Register IConfigurationProvider with all registered profiles
            container.Register(Component.For<IConfigurationProvider>().UsingFactoryMethod(kernel =>
            {
                return new MapperConfiguration(configuration =>
                {
                    kernel.ResolveAll<Profile>().ToList().ForEach(configuration.AddProfile);
                });

            }).LifestyleSingleton());

            // Register IMapper with registered IConfigurationProvider
            container.Register(
                Component.For<IMapper>().UsingFactoryMethod(kernel => new Mapper(kernel.Resolve<IConfigurationProvider>(), kernel.Resolve))
            );
        }

        private void InstallPresentationClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<ICommandManager>().ImplementedBy<CommandManager>().LifestyleTransient(),
                Classes.FromAssemblyNamed("StarLab.Presentation").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Presentation").BasedOn<Profile>().WithServiceBase()
            );
        }

        private void InstallUserInterfaceClasses(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("StarLab.UI").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Component.For<IApplicationController>().ImplementedBy<ApplicationController>()
            );
        }
    }
}
