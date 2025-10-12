namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="WorkspaceExplorerViewPresenter"/> class.
    /// </summary>
    public class WorkspaceExplorerViewPresenterTests : PresentationTests
    {
        private IWorkspaceExplorerView view; // The mock IWorkspaceExplorerView used in the tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IWorkspaceExplorerView>();
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IWorkspaceExplorerView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
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
        /// Creates an instance of <see cref="WorkspaceExplorerViewPresenter"/>.
        /// </summary>
        /// <returns>Returns the <see cref="WorkspaceExplorerViewPresenter"/>.</returns>
        private IWorkspaceExplorerViewPresenter CreatePresenter()
        {
            return new WorkspaceExplorerViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
