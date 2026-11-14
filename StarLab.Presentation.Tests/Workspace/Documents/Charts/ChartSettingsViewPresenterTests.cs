#pragma warning disable CS8601 // Possible null reference assignment.
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ChartSettingsViewPresenter"/> class.
    /// </summary>  
    public class ChartSettingsViewPresenterTests : PresenterTests
    {
        private IChart chart; // A mock of the IChart interface that can be used in the unit tests.

        private IChartDocument document; // A mock of the IChartDocument interface that can be used in the unit tests.

        private IChartSettingsView view; // A mock of the IChartSettingsView interface that can be used in the unit tests.

        private DocumentID documentID; // A DocumentID that can be used in the unit tests.

        private IWorkspace workspace; // An IWorkspace that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            workspace = new Workspace(container.Resolve<ISerialisationService>().DeserialiseWorkspace(GetPath("Workspace-2.slw")));

            documentID = new DocumentID("FB97BA9C-7B51-43F9-9025-ED54A90DB990");

            view = Substitute.For<IChartSettingsView>();
            view.ID.Returns(ViewIDs.ChartSettings);

            var title = Substitute.For<ILabel>();
            title.Text.Returns("Test Title");

            chart = Substitute.For<IChart>();
            chart.Title.Returns(title);

            document = Substitute.For<IChartDocument>();
            document.ID.Returns(documentID);
            document.Chart.Returns(chart);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ChartSettingsViewPresenter(view, Substitute.For<IDocument>(), container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            Assert.That(presenter, Is.Not.Null);

            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, Substitute.For<IDocument>(), container.Resolve<ISessionContext>(), null, container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, Substitute.For<IDocument>(), null, container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the document argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, null, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, Substitute.For<IDocument>(), container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), null));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, Substitute.For<IDocument>(), container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), null, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(null, Substitute.For<IDocument>(), container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplyPreviewSettings(IChartSettings)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplyPreviewSettings()
        {
            var port = Substitute.For<IChartOutputPort>();

            var presenter = CreatePresenter(view, document, port);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            presenter.UpdateChartPreview();

            port.Received(1).ApplyChanges(Arg.Any<ChartDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplySettings()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplySettings()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var presenter = CreatePresenter(view, document, port);

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            presenter.ApplySettings();

            port.Received(1).UpdateWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(view, document);
            
            Assert.That(presenter.ID.ToString(), Is.EqualTo("ChartSettings"));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = new ChartSettingsViewPresenter(view, document, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            presenter.RegisterController(Substitute.For<IDocumentController>());

            presenter.Initialise(controller);

            view.Received(1).AttachOKButtonCommand(Arg.Any<ICommand>());
            view.Received(1).AttachCancelButtonCommand(Arg.Any<ICommand>());

            view.Received().AddNode(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            view.Received().AddNode(Arg.Any<string>(), Arg.Any<string>());

            view.Received(1).Initialise();

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(view, document);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when the parent controller has not been registered.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenParentNotRegistered()
        {
            var presenter = new ChartSettingsViewPresenter(view, document, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.RevertSettings()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRevertSettings()
        {
            var chartController = Substitute.For<IChartController>();

            var presenter = CreatePresenter(view, document, chartController);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            presenter.RevertSettings();

            chartController.Received(1).RevertChanges();
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Run()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.Received(1).ClearOverlays();

            view.Received(1).Clear();

            view.Received(1).AppendColourSection("Chart", false);

            view.Received(1).ExpandNode("Chart");
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Run()"/> method throws an exception when the chart has not been set.
        /// </summary>
        [Test]
        public void TestRunThrowsAnExceptionWhenChartNotSet()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            var presenter = CreatePresenter(view, document, port);

            Assert.Throws<InvalidOperationException>(() => presenter.Run());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the axis settings.
        /// </summary>
        [Test]
        public void TestShowAxisSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Axes/AxisX1";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the axis label settings.
        /// </summary>
        [Test]
        public void TestShowAxisLabelSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Axes/AxisX1/Label";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(1).AppendFontSection(key, false);
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(1).AppendTextSection(key);
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the axis scale settings.
        /// </summary>
        [Test]
        public void TestShowAxisScaleSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Axes/AxisX1/Scale";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(1).AppendScaleSection(key);
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the chart settings.
        /// </summary>
        [Test]
        public void TestShowChartSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, false);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(0).AppendVisibleSection(Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the chart title settings.
        /// </summary>
        [Test]
        public void TestShowChartTitleSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Title";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(1).AppendFontSection(key, false);
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(1).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the data settings.
        /// </summary>
        [Test]
        public void TestShowDataSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Data");

            view.Received(1).Clear();

            view.Received(0).AppendColourSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(1).AppendDataSourceSection("Chart/Axes", Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(0).AppendVisibleSection(Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the grid settings.
        /// </summary>
        [Test]
        public void TestShowGridSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/PlotArea/Grid";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the major grid line settings.
        /// </summary>
        [Test]
        public void TestShowMajorGridLinesSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/PlotArea/Grid/MajorGridLines";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(key);
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the major tick mark settings.
        /// </summary>
        [Test]
        public void TestShowMajorTickMarkSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Axes/AxisX1/Scale/MajorTickMarks";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the minor grid line settings.
        /// </summary>
        [Test]
        public void TestShowMinorGridLinesSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/PlotArea/Grid/MinorGridLines";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the minor tick mark settings.
        /// </summary>
        [Test]
        public void TestShowMinorTickMarkSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Axes/AxisX1/Scale/MinorTickMarks";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the tick label settings.
        /// </summary>
        [Test]
        public void TestShowOverlaySettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Overlays";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(1).AppendFontSection(key, true);
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the plot area settings.
        /// </summary>
        [Test]
        public void TestShowPlotAreaSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/PlotArea";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, false);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(0).AppendVisibleSection(Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the point settings.
        /// </summary>
        [Test]
        public void TestShowPointSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/PlotArea/Points";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(0).AppendFontSection(Arg.Any<string>(), Arg.Any<bool>());
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(1).AppendSizeSection(key);
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the tick label settings.
        /// </summary>
        [Test]
        public void TestShowTickLabelSettings()
        {
            var presenter = CreatePresenter(view, document);

            presenter.OnEvent(new ChartSettingsChangedEventArgs(documentID, chart));

            presenter.Run();

            view.ClearReceivedCalls();

            var key = "Chart/Axes/AxisX1/Scale/TickLabels";

            presenter.ShowSettings(key);

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(key, true);
            view.Received(0).AppendDataSourceSection(Arg.Any<string>(), Arg.Any<IEnumerable<IDisplayField>>(), Arg.Any<IEnumerable<IDisplayField>>());
            view.Received(1).AppendFontSection(key, true);
            view.Received(0).AppendScaleSection(Arg.Any<string>());
            view.Received(0).AppendSizeSection(Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<string>());
            view.Received(1).AppendVisibleSection(key);
        }
    }
}
