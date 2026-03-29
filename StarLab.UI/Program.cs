using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using log4net;
using log4net.Config;
using StarLab.Presentation;
using StarLab.Shared.Resources;
using StarLab.UI;

namespace StarLab
{
    /// <summary>
    /// A static class that contains the entry point for the application.
    /// </summary>
    internal static class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program)); // The logger that will be used for writing log messages.

        /// <summary>
        ///  The entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();

            XmlConfigurator.Configure();

            log.Info(LogEntries.StartingApplication);

            var container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            container.Install(new ApplicationInstaller());

            var controller = container.Resolve<IApplicationController>();

            controller.Run();
        }
    }
}