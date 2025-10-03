namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// 
    /// </summary>
    public class ToolViewPresenterTests : PresenterTests
    {
        private IDockableView view; //

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IDockableView>();
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
        [Test]
        public void TestInitialise()
        {
            // Arrange
            var presenter = (IViewController)CreatePresenter();

            // Act
            presenter.Initialise(controller);

            // Assert
            view.Received(1).Initialise(controller);

            controller.Received(1).RegisterCommandInvokers(commands);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IDockableViewPresenter CreatePresenter()
        {
            return new ToolViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
