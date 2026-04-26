#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.WorkspaceExplorer;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DialogViewPresenter"/> class.
    /// </summary>
    public class DialogViewPresenterTests : PresentationTests
    {
        private IChildViewController child; // A mock of the IChildViewController interface that can be used in the unit tests.

        private IDialogView view; // A mock of the IDialogView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            child = Substitute.For<IChildViewController>();

            view = Substitute.For<IDialogView>();
            view.ID.Returns(Views.About);
        }

        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        public override void TearDown()
        {
            base.TearDown();

            child.Dispose();
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new DialogViewPresenter(view, child, context, commands, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"{Views.About}Controller"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the childController argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenChildControllerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, null, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, child, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, child, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, child, context, commands, null));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkspaceExplorerViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            child.Received(1).Initialise(controller);
            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            Assert.That(e.Message, Is.EqualTo("The DialogViewPresenter has already been initialised."));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DialogViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="DialogViewPresenter"/>.</returns>
        private DialogViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new DialogViewPresenter(view, child, context, commands, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}
