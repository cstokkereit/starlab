#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.WorkspaceExplorer;
using Stratosoft.Commands;

namespace StarLab.Presentation.Options
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="OptionsViewPresenter"/> class.
    /// </summary>
    public class OptionsViewPresenterTests : PresentationTests
    {
        private IOptionsView view; // A mock of the IOptionsView interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IOptionsView>();
            view.ID.Returns(Views.Options);
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new OptionsViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.Options})"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter(IOptionsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new OptionsViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="OptionsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="OptionsViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="OptionsViewPresenter"/>.</returns>
        private OptionsViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new OptionsViewPresenter(view, context, commands, services, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}
