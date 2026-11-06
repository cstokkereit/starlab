#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using Stratosoft.Commands;

namespace StarLab.Presentation.Options
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="OptionsViewPresenter"/> class.
    /// </summary>
    public class OptionsViewPresenterTests : PresenterTests
    {
        private IOptionsView view; // A mock of the IOptionsView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IOptionsView>();
            view.ID.Returns(ViewIDs.Options);
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            var presenter = new OptionsViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("Options"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();
            var services = Substitute.For<IServiceRegistry>();

            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreateOptionsViewPresenter(view, false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("Options"));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateOptionsViewPresenter(view, events, false);

            presenter.Initialise(ApplicationController);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreateOptionsViewPresenter(view, true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(ApplicationController));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter.Run()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var presenter = CreateOptionsViewPresenter(view, true);

            presenter.Run();

            // TODO - Modify for implementation
        }
    }
}
