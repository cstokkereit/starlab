using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using StarLab.Presentation;

namespace StarLab.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new ApplicationInstaller());

            var controller = container.Resolve<IApplicationController>();

            controller.Run();
        }
    }
}