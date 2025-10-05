namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DialogViewPresenter"/> class.
    /// </summary>
    public class DialogViewPresenterTests : PresentationTests
    {
        private IDialogView view; //

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IDialogView>();
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

            //var parent = Substitute.For<IDialogController>();

            //presenter.RegisterController(parent);

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
        private IDialogViewPresenter CreatePresenter()
        {
            return new DialogViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
