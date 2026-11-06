#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Presentation.Workspace.Documents.Tables;
using StarLab.Tests;
using Stratosoft.Commands;
using System.Drawing;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DocumentViewPresenter"/> class.
    /// </summary>  
    public class DocumentViewPresenterTests : PresenterTests
    {
        private List<IChildViewController> controllers = new List<IChildViewController>(); // A list containing mocked child view controllers that can be used in the unit tests.

        private IDocumentView view; // A mock of the IDocumentView interface that can be used in the unit tests.

        private IDocument document; // A mock of the IDocument interface that can be used in the unit tests.

        private DocumentID documentID; // A document ID that can be used in the unit tests.

        private ViewID viewID; // A view ID that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            documentID = new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAB");

            document = Substitute.For<IDocument>();
            document.Name.Returns("Document-1");
            document.ID.Returns(documentID);

            viewID = new ViewID(document);

            view = Substitute.For<IDocumentView>();
            view.DocumentID.Returns(documentID);
            view.ID.Returns(viewID);

            controllers.Clear();
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Close()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestClose()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.Close();

            view.Received(1).HideOnClose = false;
            view.Received(1).Close();
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, false);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("19542B1A-36A5-494F-B6B0-CB562FA36CAB"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the controllers argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenControllersIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, null, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the document argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenDocumentIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, null, controllers, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();

            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, context, commands, null));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            var context = Substitute.For<ISessionContext>();
            var commands = Substitute.For<ICommandManager>();
            var events = Substitute.For<IEventAggregator>();

            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(null, document, controllers, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.AddToolbarButton(string, string, Image, ICommand)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddToolbarButton()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            var command = Substitute.For<ICommand>();
            var image = new Bitmap(16, 16);

            presenter.AddToolbarButton("ShowSettings", "Settings", image, command);
            
            view.Received(1).AddToolbarButton("ShowSettings", "Settings", image, command);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.GetController{TController}()"/> method works correctly when the required controller type exists.
        /// </summary>
        [Test]
        public void TestGetController()
        {
            var settingsController = Substitute.For<IChildViewController>();
            settingsController.ID.Returns(new ControllerID(ViewIDs.ChartSettings));

            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            chartController.ID.Returns(new ControllerID(ViewIDs.Chart));
            controllers.Add(chartController);

            var presenter = CreateDocumentViewPresenter(view, document, controllers, false);

            var controller = presenter.GetController<IChildViewController>(new ControllerID(ViewIDs.ChartSettings));

            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.SameAs(settingsController));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.GetController{TController}()"/> method throws an exception when a controller with the specified <see cref="ControllerID"> does not exist.
        /// </summary>
        [Test]
        public void TestGetControllerThrowsExceptionWhenControllerNotFound()
        {
            var settingsController = Substitute.For<IChildViewController>();
            settingsController.ID.Returns(new ControllerID(ViewIDs.ChartSettings));
            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            chartController.ID.Returns(new ControllerID(ViewIDs.Chart));
            controllers.Add(chartController);

            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            Assert.Throws<Exception>(() => presenter.GetController<ITableController>(new ControllerID(ViewIDs.Table)));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.GetController{TController}()"/> method throws an exception when a controller with the specified <see cref="ControllerID"> does not exist.
        /// </summary>
        [Test]
        public void TestGetControllerThrowsExceptionWhenControllerTypeUnknown()
        {
            var settingsController = Substitute.For<IChildViewController>();
            settingsController.ID.Returns(new ControllerID(ViewIDs.ChartSettings));
            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            chartController.ID.Returns(new ControllerID(ViewIDs.Chart));
            controllers.Add(chartController);

            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            Assert.Throws<Exception>(() => presenter.GetController<ITableController>(new ControllerID(ViewIDs.Chart)));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("19542B1A-36A5-494F-B6B0-CB562FA36CAB"));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Location"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetLocation()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, false);

            Assert.That(presenter.Location, Is.EqualTo("Document"));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.HideSplitContent(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestHideSplitContent()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.HideSplitContent(Constants.ChartSettings);

            view.Received(1).HideSplitContent(Constants.ChartSettings);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateDocumentViewPresenter(view, document, controllers, events, false);

            presenter.Initialise(ApplicationController);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(ApplicationController));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.OnEvent(WorkspaceChangedEventArgs)"/> method works correctly for a chart document.
        /// </summary>
        [Test]
        public void TestOnEventWhenDocumentIsChart()
        {
            var settingsController = Substitute.For<IChildViewController>();
            settingsController.ID.Returns(new ControllerID(ViewIDs.ChartSettings));
            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            chartController.ID.Returns(new ControllerID(ViewIDs.Chart));
            controllers.Add(chartController);

            var chart1 = new ChartDtoBuilder()
                .AddTitle("Chart-1.1")
                .CreateChart();

            var chart2 = new ChartDtoBuilder()
                .AddTitle("Chart-1.2")
                .CreateChart();

            var workspace = CreateWorkspace(new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAB", "ChartView", "Chart-1.1", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAB", chart1)
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Chart-1.2", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", chart2)
                .CreateWorkspace());

            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            chartController.Received(1).UpdateChart(Arg.Any<IChart>());

            view.Received(1).SetName("Chart-1.1");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.OnEvent(WorkspaceChangedEventArgs)"/> method works correctly for a table document.
        /// </summary>
        [Test]
        [Ignore("Not implemented")]
        public void TestOnEventWhenDocumentIsTable()
        {
            //var settingsController = Substitute.For<ITableSettingsController>();
            //controllers.Add(settingsController);

            //var chartController = Substitute.For<ITableController>();
            //controllers.Add(chartController);

            //var table1 = new TableDtoBuilder()
            //    .CreateTable();

            //var table2 = new TableDtoBuilder()
            //    .CreateTable();

            //var dtoWorkspace = new WorkspaceDtoBuilder(@"C:\Workspace-1")
            //    .AddProject("Project-1")
            //    .AddFolder("Workspace-1/Project-1/Folder-1")
            //    .AddTable("19542B1A-36A5-494F-B6B0-CB562FA36CAB", "TableView", "Table-1.1", "Workspace-1/Project-1/Folder-1")
            //    .AddTable("19542B1A-36A5-494F-B6B0-CB562FA36CAB", table1)
            //    .AddTable("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "TableView", "Table-1.2", "Workspace-1/Project-1/Folder-1")
            //    .AddTable("19542B1A-36A5-494F-B6B0-CB562FA36CAC", table2)
            //    .CreateWorkspace();

            //var workspace = CreateWorkspace(dtoWorkspace);

            //var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            //presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            //chartController.Received(1).UpdateTable(Arg.Any<ITable>());
            //settingsController.Received(1).UpdateSettings(Arg.Any<ITableDocument>());
            //view.Received(1).SetName("Table-1.1");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Show(IView)"/> method works correctly.
        /// </summary>
        [Test]
        [Ignore("Not implemented")]
        public void TestShow()
        {
            var dialog = Substitute.For<IView>();

            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.Show(dialog);

            view.Received(1).Show(dialog);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ShowMessage(string, string, InteractionType, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessage()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);

            view.Received(1).ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ShowOpenFileDialog(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowOpenFileDialog()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.ShowOpenFileDialog("Title", "Filter");

            view.Received(1).ShowOpenFileDialog("Title", "Filter");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ShowSaveFileDialog(string, string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSaveFileDialog()
        {
            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.ShowSaveFileDialog("Title", "Filter", "Extension");

            view.Received(1).ShowSaveFileDialog("Title", "Filter", "Extension");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ShowSplitContent(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSplitContent()
        {
            var settings = Substitute.For<IChildViewController>();
            settings.ID.Returns(new ControllerID(Constants.ChartSettings));

            controllers.Add(settings);

            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.ShowSplitContent(Constants.ChartSettings);

            view.Received(1).ShowSplitContent(Constants.ChartSettings);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.UpdateDocument(IDocument)"/> method works correctly for chart documents.
        /// </summary>
        [Test]
        public void TestUpdateDocumentWhenDocumentIsChart()
        {
            var settingsController = Substitute.For<IChildViewController>();
            settingsController.ID.Returns(new ControllerID(ViewIDs.ChartSettings));
            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            chartController.ID.Returns(new ControllerID(ViewIDs.Chart));
            controllers.Add(chartController);

            var document = Substitute.For<IChartDocument, IDocument>();
            document.Name.Returns("Chart-1.1");
            document.ID.Returns(documentID);

            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            chartController.ClearReceivedCalls();

            presenter.UpdateDocument(document);

            chartController.Received(1).UpdateChart(document.Chart);

            view.Received(1).SetName("Chart-1.1");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.UpdateDocument(IDocument)"/> method works correctly for table documents.
        /// </summary>
        [Test]
        [Ignore("Not implemented")]
        public void TestUpdateDocumentWhenDocumentIsTable()
        {
            var settingsController = Substitute.For<ITableSettingsController>();
            controllers.Add(settingsController);

            var tableController = Substitute.For<ITableController>();
            controllers.Add(tableController);

            var document = Substitute.For<ITableDocument, IDocument>();
            document.Name.Returns("Table-1.1");
            document.ID.Returns(documentID);

            var presenter = CreateDocumentViewPresenter(view, document, controllers, true);

            presenter.UpdateDocument(document);

            tableController.Received(1).UpdateTable(document.Table);

            view.Received(1).SetName("Table-1.1");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var events = Substitute.For<IEventAggregator>();

            var presenter = CreateDocumentViewPresenter(view, document, controllers, events, true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == viewID));
        }
    }
}