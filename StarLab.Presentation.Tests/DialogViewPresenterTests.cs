namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DialogViewPresenter"/> class.
    /// </summary>
    public class DialogViewPresenterTests : PresentationTests
    {
        private IChildViewController content; // A mock of the IChildViewController interface that can be used in the unit tests.

        private IDialogView view; // A mock of the IDialogView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            content = Substitute.For<IChildViewController>();

            view = Substitute.For<IDialogView>();
            view.ID.Returns(Views.About);
        }

        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        public override void TearDown()
        {
            base.TearDown();

            content.Dispose();
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IApplicationView, IChildViewController, ICommandManager, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"{Views.About}Controller"));
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

            content.Received(1).Initialise(controller);
            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DialogViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="DialogViewPresenter"/>.</returns>
        private DialogViewPresenter CreatePresenter()
        {
            return new DialogViewPresenter(view, content, commands, settings, events);
        }
    }
}
