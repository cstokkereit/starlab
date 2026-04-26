#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using Castle.MicroKernel.Registration;
using NSubstitute.ExceptionExtensions;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.WorkspaceExplorer;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ToolViewPresenter"/> class.
    /// </summary>
    public class ToolViewPresenterTests : PresentationTests
    {
        private IChildViewController childController; // A mock of the IChildViewController interface that can be used in the unit tests.

        private IDockableView view; // A mock of the IDockableView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            childController = Substitute.For<IChildViewController>();

            view = Substitute.For<IDockableView>();
            view.ID.Returns(Views.WorkspaceExplorer);
        }

        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        public override void TearDown()
        {
            base.TearDown();

            childController.Dispose();
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ToolViewPresenter(view, childController, context, commands, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"{Views.WorkspaceExplorer}Controller"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the childController argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenChildControllerIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, null, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, childController, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, childController, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, childController, context, commands, null));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(null, childController, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            childController.Received(1).Initialise(controller);
            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ToolViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="ToolViewPresenter"/>.</returns>
        private ToolViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new ToolViewPresenter(view, childController, context, commands, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}
