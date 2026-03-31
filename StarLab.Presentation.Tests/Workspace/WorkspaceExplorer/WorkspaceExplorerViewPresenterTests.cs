using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="WorkspaceExplorerViewPresenter"/> class.
    /// </summary>
    public class WorkspaceExplorerViewPresenterTests : PresentationTests
    {
        private IWorkspaceExplorerView view; // A mock of the IWorkspaceExplorerView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IWorkspaceExplorerView>();
            view.ID.Returns(Views.WorkspaceExplorer);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ICommandManager, IServiceRegistry, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreatePresenter();

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.WorkspaceExplorer})"));
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
        /// A factory method that creates a new instance of the <see cref="WorkspaceExplorerViewPresenter"/> class.
        /// </summary>
        /// <returns>Returns the newly created <see cref="WorkspaceExplorerViewPresenter"/>.</returns>
        private WorkspaceExplorerViewPresenter CreatePresenter()
        {
            return new WorkspaceExplorerViewPresenter(view, commands, services, settings, events);
        }
    }
}
