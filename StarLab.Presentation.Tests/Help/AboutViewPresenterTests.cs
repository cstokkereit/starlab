#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using Stratosoft.Commands;

namespace StarLab.Presentation.Help
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="AboutViewPresenter"/> class.
    /// </summary>
    public class AboutViewPresenterTests : PresenterTests
    {
        private IAboutView view; // A mock of the IAboutView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IAboutView>();
            view.ID.Returns(ViewIDs.About);
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter(IAboutView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            var presenter = new AboutViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("About"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter(IAboutView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new AboutViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter(IAboutView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new AboutViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter(IAboutView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new AboutViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter(IAboutView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new AboutViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter(IAboutView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                         var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new AboutViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreateAboutViewPresenter(view, false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("About"));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateAboutViewPresenter(view, events, false);

            presenter.Initialise(ApplicationController);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreateAboutViewPresenter(view, true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(ApplicationController));
        }

        /// <summary>
        /// Test that the <see cref="AboutViewPresenter.Run()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var presenter = CreateAboutViewPresenter(view, true);

            presenter.Run();

            // TODO - Modify for implementation
        }
    }
}
