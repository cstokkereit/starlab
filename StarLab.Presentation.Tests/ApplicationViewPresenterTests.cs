namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ApplicationViewPresenter"/> class.
    /// </summary>
    public class ApplicationViewPresenterTests : PresentationTests
    {
        private IApplicationView view; // A mock of the IApplicationView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IApplicationView>();
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ICommandManager, IServiceRegistry, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"{Views.Application}Controller"));
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
        /// A factory method that creates a new instance of the <see cref="ApplicationViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="ApplicationViewPresenter"/>.</returns>
        private ApplicationViewPresenter CreatePresenter()
        {
            return new ApplicationViewPresenter(view, commands, services, settings, events);
        }
    }
}
