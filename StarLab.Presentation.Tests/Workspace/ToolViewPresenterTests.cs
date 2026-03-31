namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ToolViewPresenter"/> class.
    /// </summary>
    public class ToolViewPresenterTests : PresentationTests
    {
        private IChildViewController content; // A mock of the IChildViewController interface that can be used in the unit tests.

        private IDockableView view; // A mock of the IDockableView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            content = Substitute.For<IChildViewController>();

            view = Substitute.For<IDockableView>();
            view.ID.Returns(Views.WorkspaceExplorer);
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
        /// Test that the <see cref="ToolViewPresenter(IDockableView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"{Views.WorkspaceExplorer}Controller"));
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
        /// A factory method that creates a new instance of the <see cref="ToolViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="ToolViewPresenter"/>.</returns>
        private ToolViewPresenter CreatePresenter()
        {
            return new ToolViewPresenter(view, content, commands, settings, events);
        }
    }
}
