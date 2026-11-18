#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Presentation.Configuration;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddDocumentViewPresenter"/> class.
    /// </summary>
    public class AddDocumentViewPresenterTests : PresenterTests
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

            view = Substitute.For<IAddDocumentView>();
            view.ID.Returns(ViewIDs.AddDocument);
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.AddDocument(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddDocument()
        {
            //var presenter = CreatePresenter(view);

            //presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            //presenter.Run(new NamedArguments().Add(Constants.Path, "Workspace/Project-1/Charts").Add(Constants.Type, DocumentTypes.Chart));

            //presenter.AddDocument("Document1", "Chart1");
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new AddDocumentViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            Assert.That(presenter, Is.Not.Null);

            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, container.Resolve<ISessionContext>(), null, container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, null, container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), null));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), null, events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new AddDocumentViewPresenter(null, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(view);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("AddDocument"));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = new AddDocumentViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            presenter.RegisterController(Substitute.For<IDialogController>());

            presenter.Initialise(controller);

            view.Received(1).Initialise();

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = new AddDocumentViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.Run(INamedArguments)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var presenter = CreatePresenter(view);

            presenter.Run(new AddDocumentArguments("Workspace/Project-1/Charts", DocumentTypes.Chart));

            view.Received(1).ClearDocumentTypes();
            view.Received(1).AddDocumentType("ColourMagnitudeDiagram", "  Colour-Magnitude Diagram", "ColourMagnitudeDiagram32X32");
            view.Received(1).AddDocumentType("ColourColourDiagram", "  Colour-Colour Diagram", "ColourColourDiagram32X32");
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter.Run(IRunArguments)"/> method throws an exception when provided with a null value.
        /// </summary>
        [Test]
        public void TestRunThrowsExceptionForInvalidContextType()
        {
            var presenter = CreatePresenter(view);

            presenter.RegisterController(Substitute.For<IDialogController>());

            Assert.Throws<ArgumentNullException>(() => presenter.Run(null));
        }
    }
}