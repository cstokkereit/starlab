namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// 
    /// </summary>
    public class WorkspaceExplorerViewPresenterTests : PresenterTests
    {
        private IWorkspaceExplorerView view; //

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IWorkspaceExplorerView>();
        }

        /// <summary>
        /// 
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
        /// 
        /// </summary>
        /// <returns></returns>
        private IWorkspaceExplorerViewPresenter CreatePresenter()
        {
            return new WorkspaceExplorerViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
