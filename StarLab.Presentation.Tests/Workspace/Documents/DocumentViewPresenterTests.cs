namespace StarLab.Presentation.Workspace.Documents
{
    public class DocumentViewPresenterTests : PresenterTests
    {
        private IDocumentView view; //

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IDocumentView>();
        }

        /// <summary>
        /// 
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            // Arrange
            var presenter = CreatePresenter(Substitute.For<IDocument>());

            // Assert
            Assert.That(presenter, Is.Not.Null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        private IDockableViewPresenter CreatePresenter(IDocument document)
        {
            return new DocumentViewPresenter(view, document, commands, factory, settings, mapper, events);
        }
    }
}
