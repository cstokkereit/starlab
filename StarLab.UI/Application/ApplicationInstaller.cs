using AutoMapper;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Services.Logging.Log4netIntegration;
using Castle.Windsor;
using StarLab.Commands;

namespace StarLab.Application
{
    internal class ApplicationInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            InstallMapperClasses(container);
            InstallInfrastructureClasses(container);
            InstallLoggingFacility(container);
            InstallApplicationClasses(container);
            InstallPresentationClasses(container);
            InstallUserInterfaceClasses(container);
        }

        private void InstallApplicationClasses(IWindsorContainer container)
        {
            container.Register(
                Component.For<IViewMap>().ImplementedBy<ViewMap>().LifestyleTransient(),
                Component.For<IEventAggregator>().ImplementedBy<EventAggregator>().LifestyleSingleton(),
                Classes.FromAssemblyNamed("StarLab.Application").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Application").BasedOn<Profile>().WithServiceBase()
            );
        }

        private void InstallInfrastructureClasses(IWindsorContainer container)
        {
            container.Register(
                Classes.FromAssemblyNamed("StarLab.Serialisation").Where(t => t.Name.EndsWith("Service")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Serialisation").BasedOn<Profile>().WithServiceBase()
            );
        }

        private void InstallLoggingFacility(IWindsorContainer container)
        {
            log4net.Config.XmlConfigurator.Configure();
            container.AddFacility<LoggingFacility>(f => f.LogUsing<Log4netFactory>());
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
                Classes.FromAssemblyNamed("StarLab.UI.Views").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Component.For<IApplicationController>().ImplementedBy<ApplicationController>().LifestyleTransient(),
                Component.For<IConfiguration>().ImplementedBy<Configuration>().LifestyleTransient()
            );
        }
    }
}
