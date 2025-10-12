namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ColourMagnitudeChartViewPresenter"/> class.
    /// </summary>
    public class ColourMagnitudeChartViewPresenterTests : PresentationTests
    {
        private IChartView view; // The mock IChartView used in the tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IChartView>();
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
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
        /// Test that the <see cref="IChildViewController.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            // Arrange
            var presenter = (IChildViewController)CreatePresenter();

            // Act
            presenter.Initialise(controller);

            // Assert
            view.Received(1).Initialise(controller);

            controller.Received(1).RegisterCommandInvokers(commands);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Creates an instance of <see cref="ColourMagnitudeChartViewPresenter"/>.
        /// </summary>
        /// <returns>Returns the <see cref="ColourMagnitudeChartViewPresenter"/>.</returns>
        private IChartViewPresenter CreatePresenter()
        {
            return new ColourMagnitudeChartViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
