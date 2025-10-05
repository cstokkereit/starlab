namespace StarLab.Presentation.Options
{
    /// <summary>
    /// 
    /// </summary>
    public class OptionsViewPresenterTests : PresentationTests
    {
        private IOptionsView view; //

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IOptionsView>();
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
            var presenter = (IChildViewController)CreatePresenter();

            //var parent = Substitute.For<IDialogController>();

            //presenter.RegisterController(parent);

            // Act
            presenter.Initialise(controller);

            // Assert
            controller.Received(1).RegisterCommandInvokers(commands);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IOptionsViewPresenter CreatePresenter()
        {
            return new OptionsViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
