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

            view = Substitute.For<IAddDocumentView>();
            view.ID.Returns(Views.AddDocument);
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IApplicationView, ICommandManager, IServiceRegistry, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.AddDocument})"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter();

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="AddDocumentViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="AddDocumentViewPresenter"/>.</returns>
        private AddDocumentViewPresenter CreatePresenter()
        {
            return new AddDocumentViewPresenter(view, commands, services, settings, events);
        }
    }
}
