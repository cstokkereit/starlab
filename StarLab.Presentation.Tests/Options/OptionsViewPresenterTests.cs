using StarLab.Presentation.Help;

namespace StarLab.Presentation.Options
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AboutViewPresenter"/> class.
    /// </summary>
    public class OptionsViewPresenterTests : PresentationTests
    {
        private IOptionsView view; // A mock of the IOptionsView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IOptionsView>();
            view.ID.Returns(Views.Options);
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IApplicationView, ICommandManager, IServiceRegistry, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.Options})"));
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
        /// A factory method that creates a new instance of the <see cref="OptionsViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="OptionsViewPresenter"/>.</returns>
        private OptionsViewPresenter CreatePresenter()
        {
            return new OptionsViewPresenter(view, commands, services, settings, events);
        }
    }
}
