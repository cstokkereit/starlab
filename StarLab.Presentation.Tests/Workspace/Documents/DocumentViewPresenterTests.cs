#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Presentation.Configuration;
using NSubstitute.ExceptionExtensions;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DocumentViewPresenter"/> class.
    /// </summary>  
    public class DocumentViewPresenterTests : PresentationTests
    {
        private List<IChildViewController> controllers = new List<IChildViewController>(); // TODO - Need to test init etc

        private IDocumentView view; // A mock of the IDocumentView interface that can be used in the unit tests.

        private IDocument document; // A mock of the IDocument interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            document = Substitute.For<IDocument>();

            view = Substitute.For<IDocumentView>();
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new DocumentViewPresenter(view, document, controllers, context, commands, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"DocumentController({view.ID})"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the controllers argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenControllersIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, null, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the document argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, null, controllers, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, context, commands, null));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(null, document, controllers, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            Assert.That(e.Message, Is.EqualTo("The DocumentViewPresenter has already been initialised."));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="DocumentViewPresenter"/>.</returns>
        private DocumentViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new DocumentViewPresenter(view, document, controllers, context, commands, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.