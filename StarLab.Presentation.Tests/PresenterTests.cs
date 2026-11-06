using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Microsoft.Extensions.Logging;
using ScottPlot.Statistics;
using SkiaSharp;
using StarLab.Application;
using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts.Overlays;
using StarLab.Data.Import;
using StarLab.Data.MongoDB;
using StarLab.Data.MongoDB.Import;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Help;
using StarLab.Presentation.Options;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Serialisation;
using StarLab.UI;
using StarLab.UI.Options;
using StarLab.UI.Workspace;
using Stratosoft.Commands;
using System.Windows.Forms;
using static WeifenLuo.WinFormsUI.Docking.DockPanelExtender;

namespace StarLab.Presentation
{
    /// <summary>
    /// Base class for testing the presenter classes.
    /// </summary>
    public abstract class PresenterTests
    {
        private WindsorContainer container; // The Inversion of Control (IoC) container.

        private IViewFactory factory; // The view factory.

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
        /// Gets the application controller.
        /// </summary>
        protected IApplicationController ApplicationController => container.Resolve<IApplicationController>();

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="AboutViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="AboutViewPresenter"/>.</returns>
        protected AboutViewPresenter CreateAboutViewPresenter(IAboutView view, bool initialise)
        {
            var presenter = new AboutViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), container.Resolve<IEventAggregator>());

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="AboutViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="events">The event aggregator.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="AboutViewPresenter"/>.</returns>
        protected AboutViewPresenter CreateAboutViewPresenter(IAboutView view, IEventAggregator events, bool initialise)
        {
            var presenter = new AboutViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DialogViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="controller">The child controller.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>Returns the newly created <see cref="DialogViewPresenter"/>.</returns>
        protected DialogViewPresenter CreateDialogViewPresenter(IDialogView view, IChildViewController controller, bool initialise)
        {
            var presenter = new DialogViewPresenter(view, controller, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IEventAggregator>());

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DialogViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="controller">The child controller.</param>
        /// <param name="events">The event aggregator.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>Returns the newly created <see cref="DialogViewPresenter"/>.</returns>
        protected DialogViewPresenter CreateDialogViewPresenter(IDialogView view, IChildViewController controller, IEventAggregator events, bool initialise)
        {
            var presenter = new DialogViewPresenter(view, controller, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), events);

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="document">The document that the view represents.</param>
        /// <param name="controllers">A list containing the child controllers.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="DocumentViewPresenter"/>.</returns>
        protected DocumentViewPresenter CreateDocumentViewPresenter(IDocumentView view, IDocument document, IList<IChildViewController> controllers, bool initialise)
        {
            var presenter = new DocumentViewPresenter(view, document, controllers, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IEventAggregator>());

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="document">The document that the view represents.</param>
        /// <param name="controllers">A list containing the child controllers.</param>
        /// <param name="events">The event aggregator.</param>  
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="DocumentViewPresenter"/>.</returns>
        protected DocumentViewPresenter CreateDocumentViewPresenter(IDocumentView view, IDocument document, IList<IChildViewController> controllers, IEventAggregator events, bool initialise)
        {
            var presenter = new DocumentViewPresenter(view, document, controllers, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), events);

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="OptionsViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="OptionsViewPresenter"/>.</returns>
        protected OptionsViewPresenter CreateOptionsViewPresenter(IOptionsView view, bool initialise)
        {
            var presenter = new OptionsViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), container.Resolve<IEventAggregator>());

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="OptionsViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="events">The event aggregator.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="OptionsViewPresenter"/>.</returns>
        protected OptionsViewPresenter CreateOptionsViewPresenter(IOptionsView view, IEventAggregator events, bool initialise)
        {
            var presenter = new OptionsViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ToolViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="controller">The child controller.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="ToolViewPresenter"/>.</returns>
        protected ToolViewPresenter CreateToolViewPresenter(IDockableView view, IChildViewController controller, bool initialise)
        {
            var presenter = new ToolViewPresenter(view, controller, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IEventAggregator>());

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ToolViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="controller">The child controller.</param>
        /// <param name="events">The event aggregator.</param>
        /// <param name="initialise"><c>true</c> to initialise the presenter; <c>false</c> otherwise.</param>
        /// <returns>The newly created <see cref="ToolViewPresenter"/>.</returns>
        protected ToolViewPresenter CreateToolViewPresenter(IDockableView view, IChildViewController controller, IEventAggregator events, bool initialise)
        {
            var presenter = new ToolViewPresenter(view, controller, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), events);

            if (initialise) presenter.Initialise(ApplicationController);

            return presenter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        protected IWorkspace CreateWorkspace(WorkspaceDTO dto)
        {
            return new Workspace.Workspace(dto);
        }


        /// <summary>
        /// Configures the mock factory to return the view provided when <see cref="ViewFactory.CreateApplicationView(string)"/> CreateApplicationView is called.
        /// </summary>
        /// <param name="view"></param>
        protected void RegisterView(IApplicationView view)
        {
            factory.CreateApplicationView("StarLab").Returns(view);
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
