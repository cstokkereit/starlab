namespace StarLab.Presentation.Help
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AboutViewPresenter"/> class.
    /// </summary>
    public class AboutViewPresenterTests : PresentationTests
    {
        private IAboutView view; // A mock of the IAboutView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IAboutView>();
            view.ID.Returns(Views.About);
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter(IApplicationView, ICommandManager, IServiceRegistry, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.About})"));
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
        /// A factory method that creates a new instance of the <see cref="AboutViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="AboutViewPresenter"/>.</returns>
        private AboutViewPresenter CreatePresenter()
        {
            return new AboutViewPresenter(view, commands, services, settings, events);
        }
    }
}
