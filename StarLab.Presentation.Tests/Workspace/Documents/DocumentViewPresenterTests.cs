#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.Presentation.Workspace.Documents.Tables;
using StarLab.Tests;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DocumentViewPresenter"/> class.
    /// </summary>  
    public class DocumentViewPresenterTests : PresentationTests
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
            var presenter = CreatePresenter(true);

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
            var presenter = new DocumentViewPresenter(view, document, controllers, context, commands, events);

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
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, context, null, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, null, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the controllers argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenControllersIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, null, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the document argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, null, controllers, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(view, document, controllers, context, commands, null));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter(IDocumentView, IDocument, IEnumerable{IChildViewController}, ISessionContext, ICommandManager, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new DocumentViewPresenter(null, document, controllers, context, commands, events));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.GetController{TController}()"/> method works correctly when the required controller type exists.
        /// </summary>
        [Test]
        public void TestGetController()
        {
            var settingsController = Substitute.For<IChartSettingsController>();
            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            controllers.Add(chartController);

            var presenter = CreatePresenter(true);

            var controller = presenter.GetController<IChartSettingsController>();

            Assert.That(controller, Is.Not.Null);
            Assert.That(controller, Is.SameAs(settingsController));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.GetController{TController}()"/> method works correctly when the required controller type exists.
        /// </summary>
        [Test]
        public void TestGetControllerThrowsExceptionWhenControllerNotFound()
        {
            var settingsController = Substitute.For<IChartSettingsController>();
            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            controllers.Add(chartController);

            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.GetController<ITableController>());
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("19542B1A-36A5-494F-B6B0-CB562FA36CAB"));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.HideSplitContent(string)"/> method works correctly.
        /// </summary>
        [Test]    
        public void TestHideSplitContent()
        {
            var presenter = CreatePresenter(true);
            
            presenter.HideSplitContent("ContentName");

            view.Received(1).HideSplitContent("ContentName");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.OnEvent(WorkspaceChangedEventArgs)"/> method works correctly for a chart document.
        /// </summary>
        [Test]
        public void TestOnEventWhenDocumentIsChart()
        {
            IDocument? document = null;

            var settingsController = Substitute.For<IChartSettingsController>();
            settingsController.UpdateSettings(Arg.Do<IChartDocument>(d => document = d));
            controllers.Add(settingsController);

            IChart? chart = null;

            var chartController = Substitute.For<IChartController>();
            chartController.UpdateChart(Arg.Do<IChart>(c => chart = c));
            controllers.Add(chartController);

            var dtoChart = new ChartDtoBuilder()
                .AddTitle("Chart-1.1")
                .CreateChart();

            var dtoWorkspace = new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAB", "ChartView", "Document-1.1", "Workspace-1/Project-1/Folder-1")
                .AddDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Document-2", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAB", dtoChart)
                .CreateWorkspace();

            var workspace = CreateWorkspace(dtoWorkspace);

            var presenter = CreatePresenter(true);
            
            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            chartController.Received(1).UpdateChart(Arg.Any<IChart>());
            settingsController.Received(1).UpdateSettings(Arg.Any<IChartDocument>());
            view.Received(1).SetName("Document-1.1");

            Assert.That(document, Is.Not.Null);
            Assert.That(document.Name, Is.EqualTo("Document-1.1"));
            Assert.That(document.ID.ToString, Is.EqualTo("19542B1A-36A5-494F-B6B0-CB562FA36CAB"));

            Assert.That(chart, Is.Not.Null);
            Assert.That(chart.Title.Text, Is.EqualTo("Chart-1.1"));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.OnEvent(WorkspaceChangedEventArgs)"/> method works correctly for a table document.
        /// </summary>
        [Test]
        [Ignore("Functionality not implemented")]
        public void TestOnEventWhenDocumentIsTable()
        {
            //IDocument? document = null;

            //var settingsController = Substitute.For<IChartSettingsController>();
            //settingsController.UpdateSettings(Arg.Do<IChartDocument>(d => document = d));
            //controllers.Add(settingsController);

            //IChart? chart = null;

            //var chartController = Substitute.For<IChartController>();
            //chartController.UpdateChart(Arg.Do<IChart>(c => chart = c));
            //controllers.Add(chartController);

            //var dtoChart = new ChartDtoBuilder()
            //    .AddTitle("Chart-1.1")
            //    .CreateChart();

            //var dtoWorkspace = new WorkspaceDtoBuilder(@"C:\Workspace-1")
            //    .AddProject("Project-1")
            //    .AddFolder("Workspace-1/Project-1/Folder-1")
            //    .AddDocument("Document1", "ChartView", "Document-1.1", "Workspace-1/Project-1/Folder-1")
            //    .AddDocument("Document2", "ChartView", "Document-2", "Workspace-1/Project-1/Folder-1")
            //    .AddChart("Document1", dtoChart)
            //    .CreateWorkspace();

            //var workspace = CreateWorkspace(dtoWorkspace);

            //var presenter = CreatePresenter(true);

            //presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            //chartController.Received(1).UpdateChart(Arg.Any<IChart>());
            //settingsController.Received(1).UpdateSettings(Arg.Any<IChartDocument>());
            //view.Received(1).SetName("Document-1.1");

            //Assert.That(document, Is.Not.Null);
            //Assert.That(document.Name, Is.EqualTo("Document-1.1"));
            //Assert.That(document.ID, Is.EqualTo("Document1"));

            //Assert.That(chart, Is.Not.Null);
            //Assert.That(chart.Title.Text, Is.EqualTo("Chart-1.1"));
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.HideSplitContent(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSplitContent()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowSplitContent("ContentName");

            view.Received(1).ShowSplitContent("ContentName");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.UpdateDocument(IDocument)"/> method works correctly for chart documents.
        /// </summary>
        [Test]
        public void TestUpdateDocumentWhenDocumentIsChart()
        {
            var settingsController = Substitute.For<IChartSettingsController>();
            controllers.Add(settingsController);

            var chartController = Substitute.For<IChartController>();
            controllers.Add(chartController);

            var document = Substitute.For<IChartDocument, IDocument>();
            document.Name.Returns("Document-1.1");
            document.ID.Returns(documentID);

            var presenter = CreatePresenter(true);

            presenter.UpdateDocument(document);

            chartController.Received(1).UpdateChart(document.Chart);
            settingsController.Received(1).UpdateSettings(document);
            view.Received(1).SetName("Document-1.1");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.UpdateDocument(IDocument)"/> method works correctly for table documents.
        /// </summary>
        [Test]
        [Ignore("Functionality not implemented")]
        public void TestUpdateDocumentWhenDocumentIsTable()
        {
            //var settingsController = Substitute.For<IChartSettingsController>();
            //controllers.Add(settingsController);

            //var chartController = Substitute.For<IChartController>();
            //controllers.Add(chartController);

            //var document = Substitute.For<IChartDocument, IDocument>();
            //document.Name.Returns("Document-1.1");
            //document.ID.Returns("Document1");

            //var presenter = CreatePresenter(true);

            //presenter.UpdateDocument(document);

            //chartController.Received(1).UpdateChart(document.Chart);
            //settingsController.Received(1).UpdateSettings(document);
            //view.Received(1).SetName("Document-1.1");
        }

        /// <summary>
        /// Test that the <see cref="DocumentViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var presenter = CreatePresenter(true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == viewID));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="DocumentViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="DocumentViewPresenter"/>.</returns>
        private DocumentViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new DocumentViewPresenter(view, document, controllers, context, commands, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}