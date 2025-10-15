namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="MessageBoxViewPresenter"/> class.
    /// </summary>
    public class MessageBoxViewPresenterTests : PresentationTests
    {
        private IMessageBoxView view; // The mock IMessageBoxView used in the tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IMessageBoxView>();
        }

        /// <summary>
        /// Test that the <see cref="MessageBoxViewPresenter(IMessageBoxView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
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
        /// Creates an instance of <see cref="MessageBoxViewPresenter"/>.
        /// </summary>
        /// <returns>Returns the <see cref="MessageBoxViewPresenter"/>.</returns>
        private IMessageBoxViewPresenter CreatePresenter()
        {
            return new MessageBoxViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
