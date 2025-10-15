namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ApplicationViewPresenter"/> class.
    /// </summary>
    public class ApplicationViewPresenterTests : PresentationTests
    {
        private IApplicationView view; // The mock IApplicationView used in the tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IApplicationView>();
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IApplicationView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
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
        /// Creates an instance of <see cref="ApplicationViewPresenter"/>.
        /// </summary>
        /// <returns>Returns the <see cref="ApplicationViewPresenter"/>.</returns>
        private IApplicationViewPresenter CreatePresenter()
        {
            return new ApplicationViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
