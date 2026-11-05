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
            view.ID.Returns(ViewIDs.WorkspaceExplorer);
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
            var commands = Substitute.For<ICommandManager>();
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();

            var presenter = new ToolViewPresenter(view, childController, context, commands, events);

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

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, childController, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, childController, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter(IDockableView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var context = Substitute.For<ISessionContext>();

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(view, childController, context, commands, null));
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

            Assert.Throws<ArgumentNullException>(() => new ToolViewPresenter(null, childController, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ChildControllers"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetChildControllers()
        {
            var presenter = CreatePresenter(true);

            var controllers = new List<IChildViewController>(presenter.ChildControllers);

            Assert.That(controllers.Count, Is.EqualTo(1));
            Assert.That(controllers[0], Is.SameAs(childController));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("WorkspaceExplorerTool"));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Location"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetLocation()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.Location, Is.EqualTo("DockRight"));
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var controller = container.Resolve<IApplicationController>();

            var events = Substitute.For<IEventAggregator>();

            var presenter = CreatePresenter(events, false);
            
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

            //var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.Show(IView)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShow()
        {
            var v = Substitute.For<IView>();

            var presenter = CreatePresenter(true);

            presenter.Show(v);

            view.Received(1).Show(v);
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ShowMessage(string, string, InteractionType, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessage()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);

            view.Received(1).ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ShowOpenFileDialog(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowOpenFileDialog()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowOpenFileDialog("Title", "Filter");

            view.Received(1).ShowOpenFileDialog("Title", "Filter");
        }

        /// <summary>
        /// Test that the <see cref="ToolViewPresenter.ShowSaveFileDialog(string, string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSaveFileDialog()
        {
            var presenter = CreatePresenter(true);

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

            var presenter = CreatePresenter(events, true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == ViewIDs.WorkspaceExplorer));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ToolViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; <c>false</c>.</param>
        /// <returns>Returns the newly created <see cref="ToolViewPresenter"/>.</returns>
        private ToolViewPresenter CreatePresenter(IEventAggregator events, bool initialise)
        {
            var commands = container.Resolve<ICommandManager>();
            var context = container.Resolve<ISessionContext>();

            var presenter = new ToolViewPresenter(view, childController, context, commands, events);

            if (initialise) presenter.Initialise(container.Resolve<IApplicationController>());

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ToolViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; <c>false</c>.</param>
        /// <returns>Returns the newly created <see cref="ToolViewPresenter"/>.</returns>
        private ToolViewPresenter CreatePresenter(bool initialise)
        {
            var commands = container.Resolve<ICommandManager>();
            var context = container.Resolve<ISessionContext>();
            var events = container.Resolve<IEventAggregator>();

            var presenter = new ToolViewPresenter(view, childController, context, commands, events);

            if (initialise) presenter.Initialise(container.Resolve<IApplicationController>());

            return presenter;
        }
    }
}
