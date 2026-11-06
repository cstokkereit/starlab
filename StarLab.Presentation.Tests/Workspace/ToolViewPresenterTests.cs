#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ToolViewPresenter"/> class.
    /// </summary>
    public class ToolViewPresenterTests : PresenterTests
    {
        private IChildViewController controller; // A mock of the IChildViewController interface that can be used in the unit tests.

        private IDockableView view; // A mock of the IDockableView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            controller = Substitute.For<IChildViewController>();

            view = Substitute.For<IDockableView>();
            view.ID.Returns(ViewIDs.WorkspaceExplorer);
        }

        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        public override void TearDown()
        {
            base.TearDown();

            controller.Dispose();
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var commands = Substitute.For<ICommandManager>();
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();

            var presenter = new ToolViewPresenter(view, controller, context, commands, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("WorkspaceExplorerTool"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the childController argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenChildControllerIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, null, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, controller, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, controller, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var context = Substitute.For<ISessionContext>();

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, controller, context, commands, null));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(null, controller, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ChildControllers"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetChildControllers()
        {
            var presenter = CreateToolViewPresenter(view, controller, true);

            var controllers = new List<IChildViewController>(presenter.ChildControllers);

            Assert.That(controllers.Count, Is.EqualTo(1));
            Assert.That(controllers[0], Is.SameAs(controller));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreateToolViewPresenter(view, controller, false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("WorkspaceExplorerTool"));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Location"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetLocation()
        {
            var presenter = CreateToolViewPresenter(view, controller, false);

            Assert.That(presenter.Location, Is.EqualTo("DockRight"));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateToolViewPresenter(view, controller, events, false);
            
            presenter.Initialise(ApplicationController);

            controller.Received(1).Initialise(ApplicationController);
            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreateToolViewPresenter(view, controller, true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(ApplicationController));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Show(IView)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShow()
        {
            var dialog = Substitute.For<IView>();

            var presenter = CreateToolViewPresenter(view, controller, true);

            presenter.Show(dialog);

            view.Received(1).Show(dialog);
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ShowMessage(string, string, InteractionType, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessage()
        {
            var presenter = CreateToolViewPresenter(view, controller, true);

            presenter.ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);

            view.Received(1).ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ShowOpenFileDialog(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowOpenFileDialog()
        {
            var presenter = CreateToolViewPresenter(view, controller,true);

            presenter.ShowOpenFileDialog("Title", "Filter");

            view.Received(1).ShowOpenFileDialog("Title", "Filter");
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ShowSaveFileDialog(string, string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSaveFileDialog()
        {
            var presenter = CreateToolViewPresenter(view, controller, true);

            presenter.ShowSaveFileDialog("Title", "Filter", "Extension");

            view.Received(1).ShowSaveFileDialog("Title", "Filter", "Extension");
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateToolViewPresenter(view, controller, events, true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == ViewIDs.WorkspaceExplorer));
        }
    }
}
