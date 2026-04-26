#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.
#pragma warning disable CS8602 // Dereference of a possibly null reference.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Presentation.Configuration;
using StarLab.Shared.Properties;
using StarLab.Tests;
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

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            var title = Substitute.For<ILabel>();
            title.Text.Returns("Test Title");

            chart = Substitute.For<IChart>();
            chart.Title.Returns(title);

            workspace = Substitute.For<IWorkspace>();
            workspace.FileName.Returns(@"C:\Test\Workspace");

            document = Substitute.For<IChartDocument>();
            document.Chart.Returns(chart);
            document.ID.Returns("Test Chart");

            view = Substitute.For<IChartSettingsView>();
            view.ID.Returns("Test View");

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
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ChartSettingsViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController(Test View)"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ChartSettingsViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplyPreviewSettings(IChartSettings)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplyPreviewSettings()
        {
            var interactor = Substitute.For<IUseCase<ChartDTO>>();

            factory.CreateUpdateChartUseCase(Arg.Any<IChartOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            var settings = new ChartSettingsBuilder().AddTitle("New Title").CreateSettings();

            presenter.ApplyPreviewSettings(settings);

            interactor.Received(1).Execute(Arg.Is<ChartDTO>(chart => chart.Title.Text == "New Title"));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplySettings()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestApplySettings()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO, string, ChartDTO>>();

            factory.CreateUpdateDocumentUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);
            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));
            presenter.UpdateSettings(document);

            var settings = new ChartSettingsBuilder().AddTitle("New Title").CreateSettings();

            presenter.ApplyPreviewSettings(settings);

            presenter.ApplySettings();

            interactor.Received(1).Execute(Arg.Is<WorkspaceDTO>(ws => ws.FileName == @"C:\Test\Workspace"), "Test Chart", Arg.Is<ChartDTO>(chart => chart.Title.Text == "New Title"));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplySettings()"/> method throws an exception when the document ID has not been set.
        /// </summary>
        [Test]
        public void TestApplySettingsThrowsAnExceptionWhenDocumentIDNotSet()
        {
            var settings = Substitute.For<IChartSettings>();

            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.ApplySettings());

            Assert.That(e.Message, Is.EqualTo("The document id has not been set."));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ApplySettings()"/> method throws an exception when the workspace has not been set.
        /// </summary>
        [Test]
        public void TestApplySettingsThrowsAnExceptionWhenWorkspaceNotSet()
        {
            var settings = Substitute.For<IChartSettings>();

            var presenter = CreatePresenter(true);
            presenter.UpdateSettings(document);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.ApplySettings());

            Assert.That(e.Message, Is.EqualTo("The workspace has not been set."));
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

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            Assert.That(e.Message, Is.EqualTo("The ChartSettingsViewPresenter has already been initialised."));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.Initialise(IApplicationController)"/> method throws an exception when the parent controller has not been registered.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenParentNotRegistered()
        {
            var presenter = new ChartSettingsViewPresenter(view, context, commands, services, events);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            Assert.That(e.Message, Is.EqualTo("This instance has not been properly initialised."));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.RevertSettings()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRevertsSettings()
        {
            var chartController = Substitute.For<IChartController, IApplicationOutputPort>();

            var presenter = CreatePresenter(chartController);

            presenter.RevertSettings();

            chartController.Received(0).UpdateChart(Arg.Any<IChart>());
            chartController.Received(1).UpdatePreview();
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the axis settings.
        /// </summary>
        [Test]
        public void TestShowAxisSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/Axes/AxisX1");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1");
            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the axis label settings.
        /// </summary>
        [Test]
        public void TestShowAxisLabelSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/Axes/AxisX1/Label");

            view.Received(1).Clear();

            view.Received(1).AppendTextSection(settings, "Chart/Axes/AxisX1/Label");
            view.Received(1).AppendFontSection(settings, "Chart/Axes/AxisX1/Label");
            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1/Label");
            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1/Label");
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the axis scale settings.
        /// </summary>
        [Test]
        public void TestShowAxisScaleSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/Axes/AxisX1/Scale");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1/Scale");
            view.Received(1).AppendScaleSection(settings, "Chart/Axes/AxisX1/Scale");
            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1/Scale");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the chart settings.
        /// </summary>
        [Test]
        public void TestShowChartSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendVisibleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the chart title settings.
        /// </summary>
        [Test]
        public void TestShowChartTitleSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/Title");

            view.Received(1).Clear();

            view.Received(1).AppendTextSection(settings, "Chart/Title");
            view.Received(1).AppendFontSection(settings, "Chart/Title");
            view.Received(1).AppendColourSection(settings, "Chart/Title");
            view.Received(1).AppendVisibleSection(settings, "Chart/Title");
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the grid settings.
        /// </summary>
        [Test]
        public void TestShowGridSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/PlotArea/Grid");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/PlotArea/Grid");
            view.Received(1).AppendVisibleSection(settings, Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the major grid line settings.
        /// </summary>
        [Test]
        public void TestShowMajorGridLinesSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/PlotArea/Grid/MajorGridLines");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/PlotArea/Grid/MajorGridLines");
            view.Received(1).AppendVisibleSection(settings, "Chart/PlotArea/Grid/MajorGridLines");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the major tick mark settings.
        /// </summary>
        [Test]
        public void TestShowMajorTickMarkSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/Axes/AxisX1/Scale/MajorTickMarks");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1/Scale/MajorTickMarks");
            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1/Scale/MajorTickMarks");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the minor grid line settings.
        /// </summary>
        [Test]
        public void TestShowMinorGridLinesSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/PlotArea/Grid/MinorGridLines");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/PlotArea/Grid/MinorGridLines");
            view.Received(1).AppendVisibleSection(settings, "Chart/PlotArea/Grid/MinorGridLines");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the minor tick mark settings.
        /// </summary>
        [Test]
        public void TestShowMinorTickMarkSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/Axes/AxisX1/Scale/MinorTickMarks");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1/Scale/MinorTickMarks");
            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1/Scale/MinorTickMarks");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the plot area settings.
        /// </summary>
        [Test]
        public void TestShowPlotAreaSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/PlotArea");

            view.Received(1).Clear();

            view.Received(1).AppendColourSection(settings, "Chart/PlotArea");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendVisibleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the tick label settings.
        /// </summary>
        [Test]
        public void TestShowTickLabelSettingsGroup()
        {
            var settings = new ChartSettingsBuilder().CreateSettings();

            var presenter = CreatePresenter(true);
            presenter.ApplyPreviewSettings(settings);

            presenter.ShowSettingsGroup("Chart/Axes/AxisX1/Scale/TickLabels");

            view.Received(1).Clear();

            view.Received(1).AppendFontSection(settings, "Chart/Axes/AxisX1/Scale/TickLabels");
            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1/Scale/TickLabels");
            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1/Scale/TickLabels");
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendScaleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method throws an exception when the chart has not been set.
        /// </summary>
        [Test]
        public void TestShowSettingsGroupThrowsAnExceptionWhenChartNotSet()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.ShowSettingsGroup("Chart"));

            Assert.That(e.Message, Is.EqualTo("The chart has not been set."));
        }

        /// <summary>
        /// Test that the <see cref="ChartSettingsViewPresenter.UpdateSettings(IChartDocument)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateSettings()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO, string, ChartDTO>>();

            factory.CreateUpdateDocumentUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            var title = Substitute.For<ILabel>();
            title.Text.Returns("Updated Test Title");

            var chart = Substitute.For<IChart>();
            chart.Title.Returns(title);

            var document = Substitute.For<IChartDocument>();
            document.Chart.Returns(chart);
            document.ID.Returns("Updated Test Chart");
            
            var presenter = CreatePresenter(true);
            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.UpdateSettings(document);

            presenter.ApplySettings();

            interactor.Received(1).Execute(Arg.Is<WorkspaceDTO>(workspace => workspace.FileName == @"C:\Test\Workspace"), "Updated Test Chart", Arg.Is<ChartDTO>(chart => chart.Title.Text == "Updated Test Title"));
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ChartSettingsViewPresenter"/> class.
        /// </summary>
        /// <param name="chart">The chart controller.</param>
        /// <returns>Returns the newly created <see cref="ChartSettingsViewPresenter"/>.</returns>
        private ChartSettingsViewPresenter CreatePresenter(IChartController chartController)
        {
            var presenter = new ChartSettingsViewPresenter(view, context, commands, services, events);

            var parent = Substitute.For<IDocumentController>();
            parent.GetController<IChartController>().Returns(chartController);
            parent.ID.Returns("DocumentController(Test)");
            
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
            var presenter = new ChartSettingsViewPresenter(view, context, commands, services, events);

            var parent = Substitute.For<IDocumentController>();
            parent.ID.Returns("DocumentController(Test)");

            presenter.RegisterController(parent);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
} 
