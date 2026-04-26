#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Presentation.Configuration;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ApplicationViewPresenter"/> class.
    /// </summary>
    public class ApplicationViewPresenterTests : PresentationTests
    {
        private IApplicationView view; // A mock of the IApplicationView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IApplicationView>();
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ApplicationViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"{Views.Application}Controller"));
            view.Received(1).Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ApplicationViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="ApplicationViewPresenter"/>.</returns>
        private ApplicationViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new ApplicationViewPresenter(view, context, commands, services, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}
