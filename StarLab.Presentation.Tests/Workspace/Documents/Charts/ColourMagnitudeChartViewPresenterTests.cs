#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Tests;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ColourMagnitudeChartViewPresenter"/> class.
    /// </summary>
    public class ColourMagnitudeChartViewPresenterTests : PresenterTests
    {
        private IChartView view; // A mock of the IChartView interface that can be used in the unit tests.

        private IDocument document; // A mock of the IDocument interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IChartView>();
            view.ID.Returns(ViewIDs.ColourMagnitudeDiagram);

            document = Substitute.For<IDocument>();
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ColourMagnitudeChartViewPresenter(view, document, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            Assert.That(presenter, Is.Not.Null);

            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ColourMagnitudeChartViewPresenter(view, document, container.Resolve<ISessionContext>(), null, container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ColourMagnitudeChartViewPresenter(view, document, null, container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the document argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ColourMagnitudeChartViewPresenter(view, null, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ColourMagnitudeChartViewPresenter(view, document, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), null));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ColourMagnitudeChartViewPresenter(view, document, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), null, events));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter(IChartView, IDocument, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ColourMagnitudeChartViewPresenter(null, document, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(view, document);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("ColourMagnitudeDiagram"));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = new ColourMagnitudeChartViewPresenter(view, document, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            presenter.Initialise(controller);

            view.Received(1).Initialise();

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(view, document);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter.UpdateChart(IChart)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateChart()
        {
            var chart = Substitute.For<IChart>();

            var presenter = CreatePresenter(view, document);

            presenter.UpdateChart(chart);

            view.Received(1).UpdateChart(chart);
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter.RevertChanges()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdatePreview()
        {
            var chart = Substitute.For<IChart>();

            var presenter = CreatePresenter(view, document);

            presenter.UpdateChart(chart);

            view.ClearReceivedCalls();

            presenter.RevertChanges();

            view.Received(1).UpdateChart(chart);
        }

        /// <summary>
        /// Test that the <see cref="ColourMagnitudeChartViewPresenter.ApplyChanges(ChartDTO)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdatePreviewWithChartDto()
        {
            var chart = Substitute.For<IChart>();
            chart.Title.Text.Returns("Chart-1");

            var presenter = CreatePresenter(view, document);

            presenter.UpdateChart(chart);

            view.ClearReceivedCalls();

            presenter.ApplyChanges(new ChartDtoBuilder().AddTitle("Chart-1.1").CreateChart());

            view.Received(1).UpdateChart(Arg.Is<IChart>(c => c.Title.Text == "Chart-1.1"));
        }
    }
}