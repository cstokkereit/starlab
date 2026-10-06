#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using StarLab.Application;
using StarLab.Application.Workspace.Documents;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Presentation.Configuration;
using StarLab.Shared.Properties;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ChartSettingsViewPresenter"/> class.
    /// </summary>  
    public class ChartSettingsViewPresenterTests : PresentationTests
    {
        private IChart chart; // A mock of the IChart interface that can be used in the unit tests.

        private IChartDocument document; // A mock of the IChartDocument interface that can be used in the unit tests.

        private IChartSettingsView view; // A mock of the IChartSettingsView interface that can be used in the unit tests.

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

            chart = Substitute.For<IChart>();
            chart.Title.Returns(title);

            workspace = Substitute.For<IWorkspace>();
            workspace.FileName.Returns(@"C:\Test\Workspace");

            document = Substitute.For<IChartDocument>();
            document.Chart.Returns(chart);
            document.ID.Returns(documentID);

            view = Substitute.For<IChartSettingsView>();
            view.ID.Returns(ViewIDs.ChartSettings);

            view.AddNode("Chart", Arg.Any<string>()).Returns("Chart");
            view.AddNode("Title", "Chart", Arg.Any<string>()).Returns("Chart/Title");
            view.AddNode("Axes", "Chart", Arg.Any<string>()).Returns("Chart/Axes");
            view.AddNode("AxisX1", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisX1");
            view.AddNode("Label", "Chart/Axes/AxisX1", Arg.Any<string>()).Returns("Chart/Axes/AxisX1/Label");
            view.AddNode("Scale", "Chart/Axes/AxisX1", Arg.Any<string>()).Returns("Chart/Axes/AxisX1/Scale");
            view.AddNode("MajorTickMarks", "Chart/Axes/AxisX1/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisX1/Scale/MajorTickMarks");
            view.AddNode("MinorTickMarks", "Chart/Axes/AxisX1/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisX1/Scale/MinorTickMarks");
            view.AddNode("TickLabels", "Chart/Axes/AxisX1/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisX1/Scale/TickLabels");
            view.AddNode("AxisX2", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisX2");
            view.AddNode("Label", "Chart/Axes/AxisX2", Arg.Any<string>()).Returns("Chart/Axes/AxisX2/Label");
            view.AddNode("Scale", "Chart/Axes/AxisX2", Arg.Any<string>()).Returns("Chart/Axes/AxisX2/Scale");
            view.AddNode("MajorTickMarks", "Chart/Axes/AxisX2/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisX2/Scale/MajorTickMarks");
            view.AddNode("MinorTickMarks", "Chart/Axes/AxisX2/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisX2/Scale/MinorTickMarks");
            view.AddNode("TickLabels", "Chart/Axes/AxisX2/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisX2/Scale/TickLabels");
            view.AddNode("AxisY1", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisY1");
            view.AddNode("Label", "Chart/Axes/AxisY1", Arg.Any<string>()).Returns("Chart/Axes/AxisY1/Label");
            view.AddNode("Scale", "Chart/Axes/AxisY1", Arg.Any<string>()).Returns("Chart/Axes/AxisY1/Scale");
            view.AddNode("MajorTickMarks", "Chart/Axes/AxisY1/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisY1/Scale/MajorTickMarks");
            view.AddNode("MinorTickMarks", "Chart/Axes/AxisY1/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisY1/Scale/MinorTickMarks");
            view.AddNode("TickLabels", "Chart/Axes/AxisY1/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisY1/Scale/TickLabels");
            view.AddNode("AxisY2", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisY2");
            view.AddNode("Label", "Chart/Axes/AxisY2", Arg.Any<string>()).Returns("Chart/Axes/Axisy2/Label");
            view.AddNode("Scale", "Chart/Axes/AxisY2", Arg.Any<string>()).Returns("Chart/Axes/AxisY2/Scale");
            view.AddNode("MajorTickMarks", "Chart/Axes/AxisY2/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisY2/Scale/MajorTickMarks");
            view.AddNode("MinorTickMarks", "Chart/Axes/AxisY2/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisY2/Scale/MinorTickMarks");
            view.AddNode("TickLabels", "Chart/Axes/AxisY2/Scale", Arg.Any<string>()).Returns("Chart/Axes/AxisY2/Scale/TickLabels");
            view.AddNode("PlotArea", "Chart", Arg.Any<string>()).Returns("Chart/PlotArea");
            view.AddNode("Grid", "Chart/PlotArea", Arg.Any<string>()).Returns("Chart/PlotArea/Grid");
            view.AddNode("MajorGridLines", "Chart/PlotArea/Grid", Arg.Any<string>()).Returns("Chart/PlotArea/Grid/MajorGridLines");
            view.AddNode("MinorGridLines", "Chart/PlotArea/Grid", Arg.Any<string>()).Returns("Chart/PlotArea/Grid/MinorGridLines");
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
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ChartSettingsViewPresenter(view, document, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("ChartSettings"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, document, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, document, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the document argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, document, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, document, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(null, document, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplyPreviewSettings(IChartSettings)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplyPreviewSettings()
        {
            view.AppendColourSection(Arg.Do<IChartSettings>(arg => arg.ForeColour = "Red"));

            var interactor = Substitute.For<IUseCase<ChartDTO>>();

            factory.CreateApplyChartSettingsUseCase(Arg.Any<IChartOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.Run();

            presenter.ApplyPreviewSettings();

            interactor.Received(1).Execute(Arg.Is<ChartDTO>(dto => dto.ForeColour == "Red"));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplySettings()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplySettings()
        {
            view.AppendColourSection(Arg.Do<IChartSettings>(arg => arg.ForeColour = "Red"));

            var interactor = Substitute.For<IUseCase<UpdateDocumentUseCaseArgs>>();

            factory.CreateUpdateDocumentUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.Run();

            presenter.ApplySettings();

            interactor.Received(1).Execute(Arg.Is<UpdateDocumentUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace" && args.DocumentID == documentID.ToString() && args.Chart.ForeColour == "Red"));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplySettings()"/> method throws an exception when the document ID has not been set.
        /// </summary>
        [Test]
        public void TestApplySettingsThrowsAnExceptionWhenDocumentIDNotSet()
        {
            var settings = Substitute.For<IChartSettings>();

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            Assert.Throws<InvalidOperationException>(() => presenter.ApplySettings());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplySettings()"/> method throws an exception when the workspace has not been set.
        /// </summary>
        [Test]
        public void TestApplySettingsThrowsAnExceptionWhenWorkspaceNotSet()
        {
            var settings = Substitute.For<IChartSettings>();

            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.ApplySettings());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("ChartSettings"));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            view.Received(1).AttachOKButtonCommand(Arg.Any<ICommand>());
            view.Received(1).AttachCancelButtonCommand(Arg.Any<ICommand>());

            view.Received(1).AddNode("Chart", Resources.Chart);
            view.Received(1).AddNode("Title", "Chart", Resources.Title);
            view.Received(1).AddNode("Axes", "Chart", Resources.Axes);
            view.Received(1).AddNode("AxisX1", "Chart/Axes", Resources.AxisX1);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisX1", Resources.Label);
            view.Received(1).AddNode("MajorTickMarks", "Chart/Axes/AxisX1/Scale", Resources.MajorTickMarks);
            view.Received(1).AddNode("MinorTickMarks", "Chart/Axes/AxisX1/Scale", Resources.MinorTickMarks);
            view.Received(1).AddNode("TickLabels", "Chart/Axes/AxisX1/Scale", Resources.TickLabels);
            view.Received(1).AddNode("AxisX2", "Chart/Axes", Resources.AxisX2);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisX2", Resources.Label);
            view.Received(1).AddNode("MajorTickMarks", "Chart/Axes/AxisX2/Scale", Resources.MajorTickMarks);
            view.Received(1).AddNode("MinorTickMarks", "Chart/Axes/AxisX2/Scale", Resources.MinorTickMarks);
            view.Received(1).AddNode("TickLabels", "Chart/Axes/AxisX2/Scale", Resources.TickLabels);
            view.Received(1).AddNode("AxisY1", "Chart/Axes", Resources.AxisY1);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisY1", Resources.Label);
            view.Received(1).AddNode("MajorTickMarks", "Chart/Axes/AxisY1/Scale", Resources.MajorTickMarks);
            view.Received(1).AddNode("MinorTickMarks", "Chart/Axes/AxisY1/Scale", Resources.MinorTickMarks);
            view.Received(1).AddNode("TickLabels", "Chart/Axes/AxisY1/Scale", Resources.TickLabels);
            view.Received(1).AddNode("AxisY2", "Chart/Axes", Resources.AxisY2);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisY2", Resources.Label);
            view.Received(1).AddNode("MajorTickMarks", "Chart/Axes/AxisY2/Scale", Resources.MajorTickMarks);
            view.Received(1).AddNode("MinorTickMarks", "Chart/Axes/AxisY2/Scale", Resources.MinorTickMarks);
            view.Received(1).AddNode("TickLabels", "Chart/Axes/AxisY2/Scale", Resources.TickLabels);
            view.Received(1).AddNode("PlotArea", "Chart", Resources.PlotArea);
            view.Received(1).AddNode("Grid", "Chart/PlotArea", Resources.Grid);
            view.Received(1).AddNode("MajorGridLines", "Chart/PlotArea/Grid", Resources.MajorGridLines);
            view.Received(1).AddNode("MinorGridLines", "Chart/PlotArea/Grid", Resources.MinorGridLines);

            view.Received(1).Initialise();

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when the parent controller has not been registered.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenParentNotRegistered()
        {
            var presenter = new ChartSettingsViewPresenter(view, document, context, commands, services, events);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.RevertSettings()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRevertSettings()
        {
            view.AppendColourSection(Arg.Do<IChartSettings>(arg => arg.ForeColour = "Red"));

            var chartController = Substitute.For<IChartController, IApplicationOutputPort>();

            var presenter = CreatePresenter(chartController);

            presenter.Run();

            presenter.RevertSettings();

            chartController.Received(0).UpdateChart(Arg.Any<IChart>());
            chartController.Received(1).UpdatePreview();
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Run()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRun()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IChartSettings>());

            view.Received(1).ExpandNode("Chart");
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the axis settings.
        /// </summary>
        [Test]
        public void TestShowAxisSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/Axes/AxisX1");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IAxisSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<IAxisSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the axis label settings.
        /// </summary>
        [Test]
        public void TestShowAxisLabelSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/Axes/AxisX1/Label");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<ILabelSettings>());
            view.Received(1).AppendFontSection(Arg.Any<ILabelSettings>());
            view.Received(1).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<ILabelSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the axis scale settings.
        /// </summary>
        [Test]
        public void TestShowAxisScaleSettings()
        {
            var presenter = CreatePresenter(true);
            
            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/Axes/AxisX1/Scale");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IScaleSettings>());
            view.Received(1).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<IScaleSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the chart settings.
        /// </summary>
        [Test]
        public void TestShowChartSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IChartSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
            view.Received(0).AppendVisibleSection(Arg.Any<IScaleSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the chart title settings.
        /// </summary>
        [Test]
        public void TestShowChartTitleSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/Title");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<ILabelSettings>());
            view.Received(1).AppendFontSection(Arg.Any<ILabelSettings>());
            view.Received(1).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<ILabelSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the grid settings.
        /// </summary>
        [Test]
        public void TestShowGridSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/PlotArea/Grid");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IGridSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<IGridSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the major grid line settings.
        /// </summary>
        [Test]
        public void TestShowMajorGridLinesSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/PlotArea/Grid/MajorGridLines");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IGridLineSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<IGridLineSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the major tick mark settings.
        /// </summary>
        [Test]
        public void TestShowMajorTickMarkSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/Axes/AxisX1/Scale/MajorTickMarks");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<ITickMarkSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<ITickMarkSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the minor grid line settings.
        /// </summary>
        [Test]
        public void TestShowMinorGridLinesSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/PlotArea/Grid/MinorGridLines");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IGridLineSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<IGridLineSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the minor tick mark settings.
        /// </summary>
        [Test]
        public void TestShowMinorTickMarkSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/Axes/AxisX1/Scale/MinorTickMarks");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<ITickMarkSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<ITickMarkSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the plot area settings.
        /// </summary>
        [Test]
        public void TestShowPlotAreaSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/PlotArea");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IPlotAreaSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendSizeSection(Arg.Any<IPointSettings>());
            view.Received(0).AppendVisibleSection(Arg.Any<ITickMarkSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the point settings.
        /// </summary>
        [Test]
        public void TestShowPointSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();

            presenter.ShowSettings("Chart/PlotArea/Points");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<IPointSettings>());
            view.Received(1).AppendSizeSection(Arg.Any<IPointSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<IPointSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendFontSection(Arg.Any<IFontSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendVisibleSection(Arg.Any<ITickMarkSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method correctly shows the tick label settings.
        /// </summary>
        [Test]
        public void TestShowTickLabelSettings()
        {
            var presenter = CreatePresenter(true);

            presenter.Run();

            view.ClearReceivedCalls();  

            presenter.ShowSettings("Chart/Axes/AxisX1/Scale/TickLabels");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(Arg.Any<ITickLabelSettings>());
            view.Received(1).AppendFontSection(Arg.Any<ITickLabelSettings>());
            view.Received(1).AppendVisibleSection(Arg.Any<ITickLabelSettings>());

            view.Received(0).AppendColourSection(Arg.Any<IChartSettings>());
            view.Received(0).AppendColourSection(Arg.Any<IPlotAreaSettings>());
            view.Received(0).AppendTextSection(Arg.Any<ILabelSettings>());
            view.Received(0).AppendScaleSection(Arg.Any<IScaleSettings>());
            view.Received(0).AppendVisibleSection(Arg.Any<ITickMarkSettings>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettings(string)"/> method throws an exception when the chart has not been set.
        /// </summary>
        [Test]
        public void TestShowSettingsThrowsAnExceptionWhenChartNotSet()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.ShowSettings("Chart"));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ChartSettingsViewPresenter"/> class.
        /// </summary>
        /// <param name="chart">The chart controller.</param>
        /// <returns>Returns the newly created <see cref="ChartSettingsViewPresenter"/>.</returns>
        private ChartSettingsViewPresenter CreatePresenter(IChartController chartController)
        {
            var presenter = new ChartSettingsViewPresenter(view, document, context, commands, services, events);

            var parent = Substitute.For<IDocumentController>();
            parent.GetController<IChartController>(new ControllerID(Constants.Chart)).Returns(chartController);
            parent.ID.Returns(new ControllerID(documentID));
            
            presenter.RegisterController(parent);

            presenter.Initialise(controller);

            return presenter;
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ChartSettingsViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="ChartSettingsViewPresenter"/>.</returns>
        private ChartSettingsViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new ChartSettingsViewPresenter(view, document, context, commands, services, events);

            var parent = Substitute.For<IDocumentController>();
            parent.ID.Returns(new ControllerID(documentID));

            presenter.RegisterController(parent);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
} 
