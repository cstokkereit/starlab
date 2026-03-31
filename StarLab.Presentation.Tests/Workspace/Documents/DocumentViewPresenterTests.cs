namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DocumentViewPresenter"/> class.
    /// </summary>  
    public class DocumentViewPresenterTests : PresentationTests
    {
        private List<IChildViewController> controllers = new List<IChildViewController>(); // TODO - Need to test init etc

        private IDocumentView view; // A mock of the IDocumentView interface that can be used in the unit tests.

        private IDocument document; // A mock of the IDocument interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            document = Substitute.For<IDocument>();

            view = Substitute.For<IDocumentView>();
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ICommandManager, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"DocumentController({view.ID})"));
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
        /// A factory method that creates a new instance of the <see cref="DocumentViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="DocumentViewPresenter"/>.</returns>
        private DocumentViewPresenter CreatePresenter()
        {
            return new DocumentViewPresenter(view, document, controllers, commands, settings, events);
        }
    }
}
