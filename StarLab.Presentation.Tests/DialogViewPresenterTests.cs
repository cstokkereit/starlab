#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Presentation.Configuration;
using Stratosoft.Commands;
using System.ComponentModel;

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
        /// Test that the <see cref="DialogViewPresenter.Close()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestClose()
        {
            var presenter = CreatePresenter(true);

            presenter.Close();

            view.Received(1).Close();
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter(IDialogView, IChildViewController, ISessionContext, ICommandManager, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new DialogViewPresenter(view, child, context, commands, events);

            Assert.That(presenter, Is.Not.Null);

            view.Received().Attach(Arg.Is(presenter));
            child.Received(1).RegisterController(presenter);
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
            Assert.Throws<ArgumentNullException>(() => new DialogViewPresenter(null, child, context, commands, events));
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
        /// Test that the <see cref="DialogViewPresenter.ChildControllers"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetChildControllers()
        {
            var presenter = CreatePresenter(true);

            var controllers = new List<IChildViewController>(presenter.ChildControllers);

            Assert.That(controllers.Count, Is.EqualTo(1));
            Assert.That(controllers[0], Is.SameAs(child));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.ID, Is.EqualTo("AboutViewController"));
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
        /// Test that the <see cref="DialogViewPresenter.Run(IWorkflowContext)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var wf = Substitute.For<IWorkflowContext>();

            var presenter = CreatePresenter(true);

            presenter.Run(wf);
            
            child.Received(1).Run(wf);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.Show(IView)"/> method works correctly.
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
        /// Test that the <see cref="DialogViewPresenter.ShowMessage(string, string, InteractionType, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessage()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);

            view.Received(1).ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ShowOpenFileDialog(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowOpenFileDialog()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowOpenFileDialog("Title", "Filter");

            view.Received(1).ShowOpenFileDialog("Title", "Filter");
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ShowSaveFileDialog(string, string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSaveFileDialog()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowSaveFileDialog("Title", "Filter", "Extension");

            view.Received(1).ShowSaveFileDialog("Title", "Filter", "Extension");
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var presenter = CreatePresenter(true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == "AboutView"));
        }

        /// <summary>
        /// Test that the <see cref="DialogViewPresenter.ViewClosing(CancelEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewClosing()
        {
            var presenter = CreatePresenter(true);

            var e = new CancelEventArgs();

            presenter.ViewClosing(e);

            view.Received(1).Hide();

            Assert.That(e.Cancel, Is.True);
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
