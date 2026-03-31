namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ColourMagnitudeChartViewPresenter"/> class.
    /// </summary>
    public class ColourMagnitudeChartViewPresenterTests : PresentationTests
    {
        private IChartView view; // A mock of the IChartView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IChartView>();
            view.ID.Returns(Views.ColourMagnitudeChart);
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IApplicationView, ICommandManager, IServiceRegistry, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.ColourMagnitudeChart})"));
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
        /// A factory method that creates a new instance of the <see cref="ColourMagnitudeChartViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="ColourMagnitudeChartViewPresenter"/>.</returns>
        private ColourMagnitudeChartViewPresenter CreatePresenter()
        {
            return new ColourMagnitudeChartViewPresenter(view, commands, services, settings, events);
        }
    }
}
