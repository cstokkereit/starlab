using AutoMapper;
using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts;
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
        private IChartSettingsView view; // The mock IChartSettingsView used in the tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IChartSettingsView>();

            view.AddNode("Chart", Arg.Any<string>()).Returns("Chart");
            view.AddNode("Title", "Chart", Arg.Any<string>()).Returns("Chart/Title");
            view.AddNode("Axes", "Chart", Arg.Any<string>()).Returns("Chart/Axes");
            view.AddNode("AxisX1", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisX1");
            view.AddNode("Label", "Chart/Axes/AxisX1", Arg.Any<string>()).Returns("Chart/Axes/AxisX1/Label");
            view.AddNode("AxisX2", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisX2");
            view.AddNode("Label", "Chart/Axes/AxisX2", Arg.Any<string>()).Returns("Chart/Axes/AxisX2/Label");
            view.AddNode("AxisY1", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisY1");
            view.AddNode("Label", "Chart/Axes/AxisY1", Arg.Any<string>()).Returns("Chart/Axes/AxisY1/Label");
            view.AddNode("AxisY2", "Chart/Axes", Arg.Any<string>()).Returns("Chart/Axes/AxisY2");
            view.AddNode("Label", "Chart/Axes/AxisY2", Arg.Any<string>()).Returns("Chart/Axes/Axisy2/Label");
            view.AddNode("PlotArea", "Chart", Arg.Any<string>()).Returns("Chart/PlotArea");
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
        /// Test that the <see cref="ChartSettingsViewPresenter(IChartSettingsView, ICommandManager, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            // Arrange
            var presenter = CreatePresenter();

            // Assert
            Assert.That(presenter, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsViewPresenter.Activate()"/> method selects the chart settings node.
        /// </summary>
        [Test]
        public void TestActivate()
        {
            // Arrange
            var presenter = (IChildViewController)CreatePresenter();

            // Act
            presenter.Activate();

            // Assert
            view.Received(1).SelectNode("Chart");
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsViewPresenter.ApplyPreviewSettings(IChartSettings)"/> method uses the <see cref="UpdateChartInteractor"/> to update the chart with the preview settings.
        /// </summary>
        [Test]
        public void TestApplyPreviewSettings()
        {
            // Arrange
            var chartController = Substitute.For<IChildViewController, IChartOutputPort>();

            var presenter = CreatePresenter(chartController);

            var interactor = Substitute.For<IUseCase<ChartDTO>>();

            factory.CreateUpdateChartUseCase((IChartOutputPort)chartController).Returns(interactor);

            // Act
            var settings = new ChartSettingsBuilder().AddTitle("Preview Settings").CreateSettings();
            
            presenter.ApplyPreviewSettings(settings);

            // Assert
            interactor.Received(1).Execute(Arg.Is<ChartDTO>(chart => chart.Title != null && chart.Title.Text == "Preview Settings"));
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsViewPresenter.Name"/> property returns the correct name.
        /// </summary>
        [Test]
        public void TestGetName()
        {
            // Arrange
            var presenter = CreatePresenter();

            // Act
            var name = presenter.Name;

            // Assert
            Assert.That(name, Is.EqualTo("ChartSettingsController"));
        }

        /// <summary>
        /// Test that the <see cref="IChildViewController.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            // Arrange
            var presenter = (IChildViewController)CreatePresenter();

            var parent = Substitute.For<IDocumentController>();

            presenter.RegisterController(parent);

            // Act
            presenter.Initialise(controller);

            // Assert
            view.Received(1).Initialise(controller);
            view.Received(1).AttachOKButtonCommand(Arg.Any<ICommandChain>());
            view.Received(1).AttachCancelButtonCommand(Arg.Any<ICommandChain>());

            view.Received(1).AddNode("Chart", Resources.Chart);
            view.Received(1).AddNode("Title", "Chart", Resources.Title);
            view.Received(1).AddNode("Axes", "Chart", Resources.Axes);
            view.Received(1).AddNode("AxisX1", "Chart/Axes", Resources.AxisX1);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisX1", Resources.Label);
            view.Received(1).AddNode("AxisX2", "Chart/Axes", Resources.AxisX2);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisX2", Resources.Label);
            view.Received(1).AddNode("AxisY1", "Chart/Axes", Resources.AxisY1);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisY1", Resources.Label);
            view.Received(1).AddNode("AxisY2", "Chart/Axes", Resources.AxisY2);
            view.Received(1).AddNode("Label", "Chart/Axes/AxisY2", Resources.Label);
            view.Received(1).AddNode("PlotArea", "Chart", Resources.PlotArea);

            controller.Received(1).RegisterCommandInvokers(commands);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="IChildViewController.Initialise(IApplicationController)"/> method throws an exception when the parent controller has not been registered.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenParentNotRegistered()
        {
            // Arrange
            var presenter = (IChildViewController)CreatePresenter();

            // Act
            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            // Assert
            Assert.That(e.Message, Is.EqualTo("This instance has not been properly initialised."));
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsController.RevertSettings()"/> method uses the <see cref="IChartController"/> to restore the previous chart settings.
        /// </summary>
        [Test]
        public void TestRevertsSettings()
        {
            // Arrange
            var chartController = Substitute.For<IChartController, IApplicationOutputPort>();

            var presenter = (IChartSettingsController)CreatePresenter(chartController);

            // Act
            presenter.RevertSettings();

            // Assert
            chartController.Received(1).UpdateChart();
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the axis settings.
        /// </summary>
        [Test]
        public void TestShowAxisSettingsGroup()
        {
            // Arrange
            var presenter = CreatePresenter(Substitute.For<IChildViewController, IChartOutputPort>());

            controller.GetController(Controllers.ApplicationViewController).Returns(presenter);

            var settings = new ChartSettingsBuilder().CreateSettings();

            presenter.ApplyPreviewSettings(settings);

            // Act
            presenter.ShowSettingsGroup("Chart/Axes/AxisX1");

            // Assert
            view.Received(1).Clear();

            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1");
            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1");
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the axis label settings.
        /// </summary>
        [Test]
        public void TestShowAxisLabelSettingsGroup()
        {
            // Arrange
            var presenter = CreatePresenter(Substitute.For<IChildViewController, IChartOutputPort>());

            controller.GetController(Controllers.ApplicationViewController).Returns(presenter);

            var settings = new ChartSettingsBuilder().CreateSettings();

            presenter.ApplyPreviewSettings(settings);

            // Act
            presenter.ShowSettingsGroup("Chart/Axes/AxisX1/Label");

            // Assert
            view.Received(1).Clear();

            view.Received(1).AppendVisibleSection(settings, "Chart/Axes/AxisX1/Label");
            view.Received(1).AppendColourSection(settings, "Chart/Axes/AxisX1/Label");
            view.Received(1).AppendFontSection(settings, "Chart/Axes/AxisX1/Label");
            view.Received(1).AppendTextSection(settings, "Chart/Axes/AxisX1/Label");
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the chart settings.
        /// </summary>
        [Test]
        public void TestShowChartSettingsGroup()
        {
            // Arrange
            var presenter = CreatePresenter(Substitute.For<IChildViewController, IChartOutputPort>());

            controller.GetController(Controllers.ApplicationViewController).Returns(presenter);

            var settings = new ChartSettingsBuilder().CreateSettings();

            presenter.ApplyPreviewSettings(settings);

            // Act
            presenter.ShowSettingsGroup("Chart");

            // Assert
            view.Received(1).Clear();

            view.Received(0).AppendVisibleSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendFontSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(0).AppendTextSection(Arg.Any<IChartSettings>(), Arg.Any<string>());
            view.Received(1).AppendColourSection(settings, "Chart");
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsViewPresenter.ShowSettingsGroup(string)"/> method correctly shows the chart title settings.
        /// </summary>
        [Test]
        public void TestShowChartTitleSettingsGroup()
        {
            // Arrange
            var presenter = CreatePresenter(Substitute.For<IChildViewController, IChartOutputPort>());

            controller.GetController(Controllers.ApplicationViewController).Returns(presenter);

            var settings = new ChartSettingsBuilder().CreateSettings();

            presenter.ApplyPreviewSettings(settings);

            // Act
            presenter.ShowSettingsGroup("Chart/Title");

            // Assert
            view.Received(1).Clear();

            view.Received(1).AppendVisibleSection(settings, "Chart/Title");
            view.Received(1).AppendColourSection(settings, "Chart/Title");
            view.Received(1).AppendFontSection(settings, "Chart/Title");
            view.Received(1).AppendTextSection(settings, "Chart/Title");
        }

        /// <summary>
        /// Test that the <see cref="IChartSettingsController.ApplySettings()"/> method uses the <see cref="UpdateDocumentInteractor"/> to update the document with the new chart settings.
        /// </summary>
        [Test]
        public void TestUpdateDocument()
        {
            // Arrange
            var chartController = Substitute.For<IChildViewController, IChartOutputPort>();

            var presenter = CreatePresenter(chartController);

            var interactor = Substitute.For<IUseCase<WorkspaceDTO, string, ChartDTO>>();

            var outputPort = Substitute.For<IApplicationViewController, IApplicationOutputPort>();

            controller.GetController(Controllers.ApplicationViewController).Returns(outputPort);

            factory.CreateUpdateDocumentUseCase((IApplicationOutputPort)outputPort).Returns(interactor);

            var dtoChart = new ChartDtoBuilder().AddTitle("Original").CreateChart();

            var dtoWorkspace = new WorkspaceDtoBuilder("Workspace")
                .AddProject("Charts")
                .AddDocument("D1", "ColourMagnitudeView", "Chart", "Workspace/Charts")
                .AddChart("D1", dtoChart)
                .CreateWorkspace();

            ((ISubscriber<WorkspaceChangedEventArgs>)presenter).OnEvent(new WorkspaceChangedEventArgs(new Workspace(dtoWorkspace)));

            var document = Substitute.For<IDocument>();
            document.ID.Returns("D1");

            ((IChartSettingsController)presenter).UpdateSettings(document);

            var settings = new ChartSettingsBuilder().AddTitle("Modified").CreateSettings();

            presenter.ApplyPreviewSettings(settings);

            // Act
            ((IChartSettingsController)presenter).ApplySettings();

            // Assert
            interactor.Received(1).Execute(Arg.Any<WorkspaceDTO>(), Arg.Is("D1"), Arg.Is<ChartDTO>(args => args.Title != null && args.Title.Text == "Modified"));
        }

        /// <summary>
        /// Creates an instance of <see cref="ChartSettingsViewPresenter"/>.
        /// </summary>
        /// <param name="chartController">The <see cref="IChildViewController"/> that controls the chart.</param>
        /// <returns>Returns the <see cref="ChartSettingsViewPresenter"/>.</returns>
        private IChartSettingsViewPresenter CreatePresenter(IChildViewController chartController)
        {
            var presenter = (IChildViewController)CreatePresenter();

            controller.GetController(Controllers.ApplicationViewController).Returns(presenter);

            var parent = Substitute.For<IDocumentController>();
            parent.GetController(Controllers.ChartController).Returns(chartController);

            presenter.RegisterController(parent);

            presenter.Initialise(controller);

            return (IChartSettingsViewPresenter)presenter;
        }

        /// <summary>
        /// Creates an instance of <see cref="ChartSettingsViewPresenter"/>.
        /// </summary>
        /// <returns>Returns the <see cref="ChartSettingsViewPresenter"/>.</returns>
        protected IChartSettingsViewPresenter CreatePresenter()
        {
            return new ChartSettingsViewPresenter(view, commands, factory, settings, mapper, events);
        }
    }
} 
