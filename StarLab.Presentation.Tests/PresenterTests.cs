using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.Resolvers.SpecializedResolvers;
using Castle.Windsor;
using Microsoft.Extensions.Logging;
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
using StarLab.UI.Workspace;
using Stratosoft.Commands;
using static WeifenLuo.WinFormsUI.Docking.DockPanelExtender;

namespace StarLab.Presentation
{
    /// <summary>
    /// Base class for testing the presenter classes.
    /// </summary>
    public abstract class PresenterTests
    {
        protected WindsorContainer container; // The Inversion of Control (IoC) container.

        protected IApplicationController controller; // The mocked application controller.

        protected IEventAggregator events; // The mocked event aggregator.

        protected IUserSettings settings; // The mocked user settings.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        [SetUp]
        public virtual void SetUp()
        {
            controller = Substitute.For<IApplicationController>();

            events = Substitute.For<IEventAggregator>();
            settings = Substitute.For<IUserSettings>();

            container = new WindsorContainer();

            container.Kernel.Resolver.AddSubResolver(new CollectionResolver(container.Kernel));

            InstallMapperClasses();
            InstallInfrastructureClasses();
            InstallApplicationClasses();
            InstallPersistenceClasses();
            InstallPresentationClasses();
            InstallUserInterfaceClasses();

            container.Resolve<IServiceRegistry>().Initialise(controller);
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

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="AboutViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <returns>The newly created <see cref="AboutViewPresenter"/>.</returns>
        protected AboutViewPresenter CreatePresenter(IAboutView view)
        {
            var presenter = new AboutViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), container.Resolve<IEventAggregator>());

            presenter.Initialise(controller);

            ClearReceivedCalls(view);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ApplicationViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="port">The use case output port.</param>
        /// <returns>Returns the newly created <see cref="ApplicationViewPresenter"/>.</returns>
        protected ApplicationViewPresenter CreatePresenter(IApplicationView view, IApplicationOutputPort port)
        {
            var presenter = new ApplicationViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), container.Resolve<IEventAggregator>());

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);

            presenter.Initialise(controller);

            ClearReceivedCalls(view);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ApplicationViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <returns>Returns the newly created <see cref="ApplicationViewPresenter"/>.</returns>
        protected ApplicationViewPresenter CreatePresenter(IApplicationView view)
        {
            var presenter = new ApplicationViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), container.Resolve<IEventAggregator>());

            controller.GetOutputPort<IApplicationOutputPort>().Returns(presenter);

            presenter.Initialise(controller);

            ClearReceivedCalls(view);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DialogViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="childController">The child controller.</param>
        /// <returns>Returns the newly created <see cref="DialogViewPresenter"/>.</returns>
        protected DialogViewPresenter CreatePresenter(IDialogView view, IChildViewController childController)
        {
            var presenter = new DialogViewPresenter(view, childController, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IEventAggregator>());

            presenter.Initialise(controller);

            ClearReceivedCalls(view);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="document">The document that the view represents.</param>
        /// <param name="childControllers">A list containing the child controllers.</param>
        /// <returns>The newly created <see cref="DocumentViewPresenter"/>.</returns>
        protected DocumentViewPresenter CreatePresenter(IDocumentView view, IDocument document, IList<IChildViewController> childControllers)
        {
            var presenter = new DocumentViewPresenter(view, document, childControllers, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IEventAggregator>());

            presenter.Initialise(controller);

            ClearReceivedCalls(view);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="OptionsViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <returns>The newly created <see cref="OptionsViewPresenter"/>.</returns>
        protected OptionsViewPresenter CreatePresenter(IOptionsView view)
        {
            var presenter = new OptionsViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), container.Resolve<IEventAggregator>());

            presenter.Initialise(controller);

            ClearReceivedCalls(view);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ToolViewPresenter"/> class.
        /// </summary>
        /// <param name="view">The managed view.</param>
        /// <param name="childController">The child controller.</param>
        /// <returns>The newly created <see cref="ToolViewPresenter"/>.</returns>
        protected ToolViewPresenter CreatePresenter(IDockableView view, IChildViewController childController)
        {
            var presenter = new ToolViewPresenter(view, childController, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IEventAggregator>());

            presenter.Initialise(controller);

            ClearReceivedCalls(view);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a mock of the <see cref="IDocumentView"/> interface.
        /// </summary>
        /// <param name="id">The ID of the document that the view represents.</param>
        /// <returns>The newly created <see cref="IDocumentView"/>.</returns>
        protected IDocumentView CreateView(DocumentID id)
        {
            var view = Substitute.For<IDocumentView>();

            view.DocumentID.Returns(id);

            controller.GetView(Arg.Is<IDocument>(d => d.ID == id)).Returns(view);

            var viewID = new ViewID(id);
            view.ID.Returns(viewID);

            controller.GetView(viewID).Returns(view);

            return view;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="IWorkspace"/> class.
        /// </summary>
        /// <param name="dto">A <see cref="WorkspaceDTO"/> representation of the workspace.</param>
        /// <returns>The newly created <see cref="IWorkspace"/>.</returns>
        protected IWorkspace CreateWorkspace(WorkspaceDTO dto)
        {
            return new Workspace.Workspace(dto);
        }

        /// <summary>
        /// Converts an <see cref="IWorkspace"/> to the equivalent <see cref="WorkspaceDTO"/> representation.
        /// </summary>
        /// <param name="workspace">The <see cref="IWorkspace"/> to convert.</param>
        /// <returns>The <see cref="WorkspaceDTO"/> representation of the <see cref="IWorkspace"/> provided.</returns>
        protected WorkspaceDTO CreateWorkspaceDTO(IWorkspace workspace)
        {
            return container.Resolve<IMapper>().Map<WorkspaceDTO>(workspace);
        }

        /// <summary>
        /// Gets the path to the specified test resource file.
        /// </summary>
        /// <param name="filename">The name of the file.</param>
        /// <returns>The path to the file.</returns>
        protected string GetPath(string filename)
        {
            return Path.Combine(AppContext.BaseDirectory, "Resources", filename);
        }

        /// <summary>
        /// Loads the specified workspace file into an <see cref="IWorkspace"/> that can be used in the tests.
        /// </summary>
        /// <param name="filename">The path to the workspace file.</param>
        /// <returns>An <see cref="IWorkspace"/> that represents the workspace defined in the file.</returns>
        protected IWorkspace? OpenWorkspace(string filename)
        {
            IWorkspace? workspace = null;

            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(args => workspace = args.Workspace));

            container.Resolve<IServiceRegistry>().GetService<IApplicationUseCaseService>().OpenWorkspace(filename);

            return workspace;
        }

        /// <summary>
        /// Clears any calls received by the mock objects.
        /// </summary>
        private void ClearReceivedCalls(IView view)
        {
            events.ClearReceivedCalls();
            view.ClearReceivedCalls();
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the application layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        private void InstallApplicationClasses()
        {
            container.Register(
                Component.For<IOverlayDataProvider>().ImplementedBy<OverlayDataProvider>(),
                Classes.FromAssemblyNamed("StarLab.Application").Where(t => t.Name.EndsWith("Factory")).WithServiceDefaultInterfaces(),
                Classes.FromAssemblyNamed("StarLab.Application").BasedOn<Profile>().WithServiceBase(),
                Component.For<IEventAggregator>().Instance(events)
            );
        }

        /// <summary>
        /// Registers the dependencies from assemblies within the infrastructure layer with the <see cref="IWindsorContainer"/>.
        /// </summary>
        private void InstallInfrastructureClasses()
        {
            container.Register(
                Component.For<ISerialisationService>().ImplementedBy<SerialisationService>(),
                Classes.FromAssemblyNamed("StarLab.Serialisation").BasedOn<Profile>().WithServiceBase()
            );
        }

        /// <summary>
        /// Registers any AutoMapper profiles that it finds within the solution with the <see cref="IWindsorContainer"/>.
        /// </summary>
        private void InstallMapperClasses()
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
        private void InstallPersistenceClasses()
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
        private void InstallPresentationClasses()
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
        private void InstallUserInterfaceClasses()
        {
            container.Register(
                Component.For<IFloatWindowFactory>().ImplementedBy<FloatWindowFactory>(),
                Component.For<ICommandManager>().ImplementedBy<CommandManager>(),
                Component.For<IApplicationController>().Instance(controller),
                Component.For<IWindsorContainer>().Instance(container),
                Component.For<IUserSettings>().Instance(settings)
            );
        }
    }
}
