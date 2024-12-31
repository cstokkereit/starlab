using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using log4net.Config;
using StarLab.Application;

namespace StarLab
{
    /// <summary>
    /// A static class that contains the entry point for the application.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        ///  The entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            XmlConfigurator.Configure();

            ApplicationConfiguration.Initialize();

            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new ApplicationInstaller());

            var controller = container.Resolve<IApplicationController>();

            controller.Run();
        }
    }
}