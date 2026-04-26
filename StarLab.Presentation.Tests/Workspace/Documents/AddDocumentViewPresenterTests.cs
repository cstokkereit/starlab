#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Configuration;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddDocumentViewPresenter"/> class.
    /// </summary>
    public class AddDocumentViewPresenterTests : PresentationTests
    {
        private IAddDocumentView view; // A mock of the IAddDocumentView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            var configuration = Substitute.For<IApplicationConfiguration>();

            var definition1 = Substitute.For<IDocumentDefinition>();
            definition1.Name.Returns("Chart1");
            definition1.DisplayName.Returns("Chart-1");
            definition1.Type.Returns(DocumentTypes.Chart);
            definition1.View.Returns("ChartView");
            definition1.Image.Returns("ChartImage1");

            var definition2 = Substitute.For<IDocumentDefinition>();
            definition2.Name.Returns("Chart2");
            definition2.DisplayName.Returns("Chart-2");
            definition2.Type.Returns(DocumentTypes.Chart);
            definition2.View.Returns("ChartView");
            definition2.Image.Returns("ChartImage2");

            configuration.DocumentDefinitions.Returns([definition1, definition2]);

            context.Configuration.Returns(configuration);

            view = Substitute.For<IAddDocumentView>();
            view.ID.Returns(Views.AddDocument);
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.AddDocument(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddDocument()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO, DocumentDTO>>();

            factory.CreateAddDocumentUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.Run(new AddDocumentWorkflowContext("Workspace/Project-1/Charts", DocumentTypes.Chart));

            presenter.AddDocument("Document1", "Chart1");

            interactor.Received(1).Execute(Arg.Any<WorkspaceDTO>(), Arg.Is<DocumentDTO>(d =>
                d.Name == "Document1" &&
                d.Path == "Workspace/Project-1/Charts" &&
                d.View == "ChartView"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new AddDocumentViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.AddDocument})"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            Assert.That(e.Message, Is.EqualTo("The AddDocumentViewPresenter has already been initialised."));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.Run(IWorkflowContext)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO, DocumentDTO>>();

            factory.CreateAddDocumentUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.Run(new AddDocumentWorkflowContext("Workspace/Project-1/Charts", DocumentTypes.Chart));

            view.Received(1).ClearDocumentTypes();
            view.Received(1).AddDocumentType("Chart1", "  Chart-1", "ChartImage1");
            view.Received(1).AddDocumentType("Chart2", "  Chart-2", "ChartImage2");
        }


        /// <summary>
        /// A factory method that creates a new instance of the <see cref="AddDocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="AddDocumentViewPresenter"/>.</returns>
        private AddDocumentViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new AddDocumentViewPresenter(view, context, commands, services, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.