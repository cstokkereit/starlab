namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DocumentViewPresenter"/> class.
    /// </summary>  
    public class DocumentViewPresenterTests : PresentationTests
    {
        private IDocumentView view; // The mock IDocumentView used in the tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IDocumentView>();
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            // Arrange
            var presenter = CreatePresenter(Substitute.For<IDocument>());

            // Assert
            Assert.That(presenter, Is.Not.Null);
        }

        /// <summary>
        /// Creates an instance of <see cref="DocumentViewPresenter"/>.
        /// </summary>
        /// <param name="document">The <see cref="IDocument"/> that the view represents.</param>
        /// <returns>Returns the <see cref="DocumentViewPresenter"/>.</returns>
        private IDockableViewPresenter CreatePresenter(IDocument document)
        {
            return new DocumentViewPresenter(view, document, commands, factory, settings, mapper, events);
        }
    }
}
