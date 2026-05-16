using StarLab.Presentation.Configuration;
using StarLab.Presentation.Help;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents.Charts;
using Stratosoft.Commands;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="PresenterFactory"/> class.
    /// </summary>
    public class PresenterFactoryTests : PresentationTests
    {
        private DocumentID documentID;

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            documentID = new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAB");
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory(IServiceRegistry, IFactoryConfiguration, IUserSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            Assert.That(factory, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IChildView, ICommandManger)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateChildViewPresenter()
        {
            var childConfiguration = Substitute.For<IChildViewConfiguration>();
            childConfiguration.Presenter.Returns("StarLab.Presentation.Help.AboutViewPresenter, StarLab.Presentation");

            var viewConfiguration = Substitute.For<IViewConfiguration>();
            viewConfiguration.GetChildViewConfiguration(ViewNames.About).Returns(childConfiguration);

            configuration.GetConfiguration(ViewNames.About).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, context, configuration, events);
            
            var view = Substitute.For<IAboutView>();
            view.ID.Returns(ViewIDs.About);
            view.Name.Returns(ViewNames.About);

            var presenter = factory.CreatePresenter(view, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID.ToString(), Is.EqualTo("About"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IChildView, ICommandManger)"/> method throws an exception if the view is of an unknown type.
        /// </summary>
        [Test]
        public void TestCreateChildViewPresenterThrowsAnExceptionForUnknownType()
        {
            var childConfiguration = Substitute.For<IChildViewConfiguration>();
            childConfiguration.Presenter.Returns("StarLab.Presentation.Presenter, StarLab.Presentation");

            var viewConfiguration = Substitute.For<IViewConfiguration>();
            viewConfiguration.GetChildViewConfiguration(ViewNames.About).Returns(childConfiguration);

            configuration.GetConfiguration(ViewNames.About).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IAboutView>();
            view.ID.Returns(ViewIDs.About);
            view.Name.Returns(ViewNames.About);
            
            Assert.Throws<Exception>(() => factory.CreatePresenter(view, commands));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method works correctly when view is <see cref="IDialogView"/>.
        /// </summary>
        [Test]
        public void TestCreateDialogViewPresenter()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IDialogView>();
            view.ID.Returns(ViewIDs.About);

            var child = Substitute.For<IChildViewPresenter, IChildViewController>();

            var presenter = factory.CreatePresenter(view, child, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID.ToString(), Is.EqualTo("AboutDialog"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method throws an exception if the child view presenter does not implement the <see cref="IChildViewController"/> interface.
        /// </summary>
        [Test]
        public void TestCreateDialogViewPresenterThrowsAnExceptionWhenInterfaceNotImplemented()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IDialogView>();

            var child = Substitute.For<IChildViewPresenter>();

            Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, child, commands));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method throws an exception if the view is of an unexpected view type.
        /// </summary>
        [Test]
        public void TestCreateDialogOrToolViewPresenterThrowsAnExceptionForUnexpectedViewType()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IView>();

            var child = Substitute.For<IChildViewPresenter, IChildViewController>();

            Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, child, commands));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IDocument, IDocumentView, IEnumerable{IChildViewPresenter}, ICommandManger)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateDocumentViewPresenter()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var document = Substitute.For<IDocument>();
            document.ID.Returns(documentID);

            var view = Substitute.For<IDocumentView>();
            view.ID.Returns(new ViewID(documentID));
            view.DocumentID.Returns(documentID);
            
            var presenter = factory.CreatePresenter(document, view, [], commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID.ToString(), Is.EqualTo("19542B1A-36A5-494F-B6B0-CB562FA36CAB"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IDocument, IDocumentView, IEnumerable{IChildViewPresenter}, ICommandManger)"/> method throws an exception if any of the child view presenters does not implement the <see cref="IChildViewController"/> interface.
        /// </summary>
        [Test]
        public void TestCreateDocumentViewPresenterThrowsAnExceptionWhenInterfaceNotImplemented()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var document = Substitute.For<IDocument>();
            document.ID.Returns(documentID);

            var view = Substitute.For<IDocumentView>();
            view.ID.Returns(ViewIDs.About);
            view.DocumentID.Returns(documentID);

            var child = Substitute.For<IChildViewPresenter>();

            Assert.Throws<Exception>(() => factory.CreatePresenter(document, view, [child], commands));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method works correctly when view is <see cref="IDockableView"/>.
        /// </summary>
        [Test]
        public void TestCreateToolViewPresenter()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IDockableView>();
            view.ID.Returns(ViewIDs.WorkspaceExplorer);

            var child = Substitute.For<IChildViewPresenter, IChildViewController>();

            var presenter = factory.CreatePresenter(view, child, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID.ToString(), Is.EqualTo("WorkspaceExplorerTool"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method throws an exception if the child view presenter does not implement the <see cref="IChildViewController"/> interface.
        /// </summary>
        [Test]
        public void TestCreateToolViewPresenterThrowsAnExceptionWhenInterfaceNotImplemented()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IDockableView>();

            var child = Substitute.For<IChildViewPresenter>();

            Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, child, commands));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, ICommandManger)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateViewPresenter()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IApplicationView>();
            view.ID.Returns(ViewIDs.Application);

            var presenter = factory.CreatePresenter(view, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID.ToString(), Is.EqualTo("ApplicationWindow"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView)"/> method throws an exception if the view is of an unexpected view type.
        /// </summary>
        [Test]
        public void TestCreateViewPresenterThrowsAnExceptionForUnexpectedViewType()
        {
            var factory = new PresenterFactory(services, context, configuration, events);

            var view = Substitute.For<IView>();

            Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, commands));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenters(IDocument, IEnumerable{IChildView}, ICommandManager)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreatePresenters()
        {
            var childConfiguration1 = Substitute.For<IChildViewConfiguration>();
            childConfiguration1.Presenter.Returns("StarLab.Presentation.Workspace.Documents.Charts.ChartSettingsViewPresenter, StarLab.Presentation");

            var childConfiguration2 = Substitute.For<IChildViewConfiguration>();
            childConfiguration2.Presenter.Returns("StarLab.Presentation.Workspace.Documents.Charts.ColourMagnitudeChartViewPresenter, StarLab.Presentation");

            var viewConfiguration = Substitute.For<IViewConfiguration>();
            viewConfiguration.GetChildViewConfiguration(ViewNames.ChartSettings).Returns(childConfiguration1);
            viewConfiguration.GetChildViewConfiguration(ViewNames.Chart).Returns(childConfiguration2);

            configuration.GetConfiguration(ViewNames.ColourMagnitudeDiagram).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, context, configuration, events);

            var document = Substitute.For<IDocument>();
            document.View.Returns(ViewNames.ColourMagnitudeDiagram);

            var child1 = Substitute.For<IChartSettingsView>();
            child1.ID.Returns(new ViewID(ViewNames.ChartSettings));
            child1.Name.Returns(ViewNames.ChartSettings);
            
            var child2 = Substitute.For<IChartView>();
            child2.ID.Returns(new ViewID(ViewNames.Chart));
            child2.Name.Returns(ViewNames.Chart);
            
            var presenters = new List<IChildViewPresenter>(factory.CreatePresenters(document, [child1, child2], commands));

            Assert.That(presenters, Has.Count.EqualTo(2));

            Assert.That(presenters[0], Is.Not.Null);
            Assert.That(presenters[0].ID.ToString(), Is.EqualTo("ChartSettings"));

            Assert.That(presenters[1], Is.Not.Null);
            Assert.That(presenters[1].ID.ToString(), Is.EqualTo("Chart"));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenters(IDocument, IEnumerable{IChildView}, ICommandManager)"/> method throws an exception if any of the child views is of an unknown type.
        /// </summary>
        [Test]
        public void TestCreatePresentersThrowsAnExceptionForUnknownType()
        {
            var childConfiguration1 = Substitute.For<IChildViewConfiguration>();
            childConfiguration1.Presenter.Returns("StarLab.Presentation.Workspace.Documents.Charts.ChartSettingsViewPresenter, StarLab.Presentation");

            var childConfiguration2 = Substitute.For<IChildViewConfiguration>();
            childConfiguration2.Presenter.Returns("StarLab.Presentation.Presenter, StarLab.Presentation");

            var viewConfiguration = Substitute.For<IViewConfiguration>();
            viewConfiguration.GetChildViewConfiguration(ViewNames.ChartSettings).Returns(childConfiguration1);
            viewConfiguration.GetChildViewConfiguration(ViewNames.Chart).Returns(childConfiguration2);

            configuration.GetConfiguration(ViewNames.ColourMagnitudeDiagram).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, context, configuration, events);

            var document = Substitute.For<IDocument>();
            document.View.Returns(ViewNames.ColourMagnitudeDiagram);

            var child1 = Substitute.For<IChartSettingsView>();
            child1.ID.Returns(new ViewID(ViewNames.ChartSettings));
            child1.Name.Returns(ViewNames.ChartSettings);
            var child2 = Substitute.For<IChartView>();
            child2.ID.Returns(new ViewID(ViewNames.Chart));
            child2.Name.Returns(ViewNames.Chart);
            Assert.Throws<Exception>(() => factory.CreatePresenters(document, [child1, child2], commands));
        }
    }
}
