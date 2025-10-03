namespace StarLab.Presentation.Workspace.Documents
{
    public class AddDocumentViewPresenterTests : PresenterTests
    {
        private IAddDocumentView view; //

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IAddDocumentView>();
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
        private IAddDocumentViewPresenter CreatePresenter()
        {
            return new AddDocumentViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
}
