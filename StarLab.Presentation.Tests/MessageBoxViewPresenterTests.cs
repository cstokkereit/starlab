namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="MessageBoxViewPresenter"/> class.
    /// </summary>
    public class MessageBoxViewPresenterTests : PresentationTests
    {
        private IMessageBoxView view; //

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IMessageBoxView>();
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
        private IMessageBoxViewPresenter CreatePresenter()
        {
            return new MessageBoxViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
