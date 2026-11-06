#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using Stratosoft.Commands;
using System.ComponentModel;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DialogViewPresenter"/> class.
    /// </summary>
    public class DialogViewPresenterTests : PresenterTests
    {
        private IChildViewController controller; // A mock of the IChildViewController interface that can be used in the unit tests.

        private IDialogView view; // A mock of the IDialogView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            controller = Substitute.For<IChildViewController>();

            view = Substitute.For<IDialogView>();
            view.ID.Returns(ViewIDs.About);
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
        /// Test that the <see cref="DialogViewPresenter.Close()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestClose()
        {
            var presenter = CreateDialogViewPresenter(view, controller,true);

            presenter.Close();

            view.Received(1).Close();
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            var presenter = new DialogViewPresenter(view, controller, context, commands, events);

            Assert.That(presenter, Is.Not.Null);

            view.Received().Attach(Arg.Is(presenter));
            controller.Received(1).RegisterController(presenter);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, controller, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, controller, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();

            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, controller, context, commands, null));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(null, controller, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the childController argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenChildControllerIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(view, null, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ChildControllers"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetChildControllers()
        {
            var presenter = CreateDialogViewPresenter(view, controller, true);

            var controllers = new List<IChildViewController>(presenter.ChildControllers);

            Assert.That(controllers.Count, Is.EqualTo(1));
            Assert.That(controllers[0], Is.SameAs(controller));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreateDialogViewPresenter(view, controller, false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("AboutDialog"));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateDialogViewPresenter(view, controller, events, false);

            presenter.Initialise(ApplicationController);

            controller.Received(1).Initialise(ApplicationController);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreateDialogViewPresenter(view, controller, true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(ApplicationController));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.Run(IViewContext)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var wf = Substitute.For<INamedArguments>();

            var presenter = CreateDialogViewPresenter(view, controller, true);

            presenter.Run(wf);

            controller.Received(1).Run(wf);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.Show(IView)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShow()
        {
            var child = Substitute.For<IView>();

            var presenter = CreateDialogViewPresenter(view, controller, true);

            presenter.Show(child);

            view.Received(1).Show(child);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ShowMessage(string, string, InteractionType, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessage()
        {
            var presenter = CreateDialogViewPresenter(view, controller, true);

            presenter.ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);

            view.Received(1).ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ShowOpenFileDialog(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowOpenFileDialog()
        {
            var presenter = CreateDialogViewPresenter(view, controller, true);

            presenter.ShowOpenFileDialog("Title", "Filter");

            view.Received(1).ShowOpenFileDialog("Title", "Filter");
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ShowSaveFileDialog(string, string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSaveFileDialog()
        {
            var presenter = CreateDialogViewPresenter(view, controller, true);

            presenter.ShowSaveFileDialog("Title", "Filter", "Extension");

            view.Received(1).ShowSaveFileDialog("Title", "Filter", "Extension");
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateDialogViewPresenter(view, controller, events, true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == ViewIDs.About));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ViewClosing(CancelEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewClosing()
        {
            var presenter = CreateDialogViewPresenter(view, controller, true);

            var e = new CancelEventArgs();

            presenter.ViewClosing(e);

            view.Received(1).Hide();

            Assert.That(e.Cancel, Is.True);
        }
    }
}
