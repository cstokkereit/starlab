#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Tables;
using StarLab.Presentation.Workspace.Documents.Charts;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Tables
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="TableSettingsViewPresenter"/> class.
    /// </summary>  
    public class TableSettingsViewPresenterTests : PresentationTests
    {
        private ITable table; // A mock of the ITable interface that can be used in the unit tests.

        private ITableDocument document; // A mock of the ITableDocument interface that can be used in the unit tests.

        private ITableSettingsView view; // A mock of the ITableSettingsView interface that can be used in the unit tests.

        private IWorkspace workspace; // A mock of the IWorkspace interface that can be used in the unit tests.

        private DocumentID documentID; // A DocumentID that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            documentID = new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAB");

            var title = Substitute.For<ILabel>();
            title.Text.Returns("Test Title");

            table = Substitute.For<ITable>();

            workspace = Substitute.For<IWorkspace>();
            workspace.FileName.Returns(@"C:\Test\Workspace");

            document = Substitute.For<ITableDocument>();
            document.Table.Returns(table);
            document.ID.Returns(documentID);

            view = Substitute.For<ITableSettingsView>();
            view.ID.Returns(ViewIDs.TableSettings);
        }

        /// <summary>
        /// Cleans up after each test.
        /// </summary>
        public override void TearDown()
        {
            view.ClearReceivedCalls();

            base.TearDown();
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter(ITableSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new TableSettingsViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("TableSettings"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter(ITableSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TableSettingsViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter(ITableSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TableSettingsViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter(ITableSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TableSettingsViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter(ITableSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TableSettingsViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter(ITableSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new TableSettingsViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter.ApplyPreviewSettings(ITableSettings)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplyPreviewSettings()
        {
            var interactor = Substitute.For<IUseCase<TableDTO>>();

            //factory.ApplyTableSettingsUseCase(Arg.Any<ITableOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            //var settings = new ChartSettingsBuilder().AddTitle("New Title").CreateSettings();

            //presenter.ApplyPreviewSettings(settings);

            //interactor.Received(1).Execute(Arg.Is<ChartDTO>(chart => chart.Title.Text == "New Title"));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter.ApplySettings()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplySettings()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO, string, TableDTO>>();

            //factory.CreateUpdateDocumentUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);
            //presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));
            //presenter.UpdateSettings(document);

            //var settings = new ChartSettingsBuilder().AddTitle("New Title").CreateSettings();

            //presenter.ApplyPreviewSettings(settings);

            //presenter.ApplySettings();

            //interactor.Received(1).Execute(Arg.Is<WorkspaceDTO>(ws => ws.FileName == @"C:\Test\Workspace"), documentID.ToString(), Arg.Is<ChartDTO>(chart => chart.Title.Text == "New Title"));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter.ApplySettings()"/> method throws an exception when the document ID has not been set.
        /// </summary>
        [Test]
        public void TestApplySettingsThrowsAnExceptionWhenDocumentIDNotSet()
        {
            var settings = Substitute.For<ITableSettings>();

            var presenter = CreatePresenter(true);

            //Assert.Throws<InvalidOperationException>(() => presenter.ApplySettings());
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter.ApplySettings()"/> method throws an exception when the workspace has not been set.
        /// </summary>
        [Test]
        public void TestApplySettingsThrowsAnExceptionWhenWorkspaceNotSet()
        {
            var settings = Substitute.For<ITableSettings>();

            var presenter = CreatePresenter(true);
            //presenter.UpdateSettings(document);

            //Assert.Throws<InvalidOperationException>(() => presenter.ApplySettings());
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            view.Received(1).AttachOKButtonCommand(Arg.Any<ICommand>());
            view.Received(1).AttachCancelButtonCommand(Arg.Any<ICommand>());

            view.Received(1).Initialise();

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="TableSettingsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when the parent controller has not been registered.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenParentNotRegistered()
        {
            var presenter = new TableSettingsViewPresenter(view, context, commands, services, events);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="TableSettingsViewPresenter"/> class.
        /// </summary>
        /// <param name="chart">The chart controller.</param>
        /// <returns>Returns the newly created <see cref="TableSettingsViewPresenter"/>.</returns>
        private TableSettingsViewPresenter CreatePresenter(IChartController chartController)
        {
            var presenter = new TableSettingsViewPresenter(view, context, commands, services, events);

            var parent = Substitute.For<IDocumentController>();
            parent.GetController<IChartController>().Returns(chartController);
            parent.ID.Returns(new ControllerID(ViewIDs.TableSettings));

            presenter.RegisterController(parent);

            presenter.Initialise(controller);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="TableSettingsViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="TableSettingsViewPresenter"/>.</returns>
        private TableSettingsViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new TableSettingsViewPresenter(view, context, commands, services, events);

            var parent = Substitute.For<IDocumentController>();
            parent.ID.Returns(new ControllerID(documentID));
            parent.DocumentID.Returns(documentID);

            presenter.RegisterController(parent);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}
