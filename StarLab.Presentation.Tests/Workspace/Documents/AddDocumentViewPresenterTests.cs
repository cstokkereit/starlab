namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AddDocumentViewPresenter"/> class.
    /// </summary>
    public class AddDocumentViewPresenterTests : PresentationTests
    {
        private IAddDocumentView view; // The mock IAddDocumentView used in the tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IAddDocumentView>();
        }

        /// <summary>
        /// Test that the <see cref="AddDocumentViewPresenter(IAddDocumentView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            // Arrange
            var presenter = CreatePresenter();

            // Assert
            Assert.That(presenter, Is.Not.Null);
        }

        /// <summary>
        /// Creates an instance of <see cref="AddDocumentViewPresenter"/>.
        /// </summary>
        /// <returns>Returns the <see cref="AddDocumentViewPresenter"/>.</returns>
        private IAddDocumentViewPresenter CreatePresenter()
        {
            return new AddDocumentViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
