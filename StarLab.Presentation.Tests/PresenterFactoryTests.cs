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
        /// <summary>
        /// Test that the <see cref="PresenterFactory(IServiceRegistry, IFactoryConfiguration, IApplicationSettings, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

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
            viewConfiguration.GetChildViewConfiguration(Views.About).Returns(childConfiguration);

            configuration.GetConfiguration(Views.About).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, configuration, settings, events);
            
            var view = Substitute.For<IAboutView>();
            view.Name.Returns(Views.About);
            view.ID.Returns(Views.About);

            var presenter = factory.CreatePresenter(view, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.About})"));
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
            viewConfiguration.GetChildViewConfiguration(Views.About).Returns(childConfiguration);

            configuration.GetConfiguration(Views.About).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IAboutView>();
            view.Name.Returns(Views.About);
            view.ID.Returns(Views.About);

            var e = Assert.Throws<Exception>(() => factory.CreatePresenter(view, commands));

            Assert.That(e.Message, Does.StartWith("Unknown type: "));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method works correctly when view is <see cref="IDialogView"/>.
        /// </summary>
        [Test]
        public void TestCreateDialogViewPresenter()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IDialogView>();
            view.ID.Returns(Views.About);

            var child = Substitute.For<IChildViewPresenter, IChildViewController>();

            var presenter = factory.CreatePresenter(view, child, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID, Is.EqualTo($"{Views.About}Controller"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method throws an exception if the child view presenter does not implement the <see cref="IChildViewController"/> interface.
        /// </summary>
        [Test]
        public void TestCreateDialogViewPresenterThrowsAnExceptionWhenInterfaceNotImplemented()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IDialogView>();

            var child = Substitute.For<IChildViewPresenter>();

            var e = Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, child, commands));

            Assert.That(e.Message, Is.EqualTo("childPresenter does not implement the IChildViewController interface."));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method throws an exception if the view is of an unexpected view type.
        /// </summary>
        [Test]
        public void TestCreateDialogOrToolViewPresenterThrowsAnExceptionForUnexpectedViewType()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IView>();

            var child = Substitute.For<IChildViewPresenter, IChildViewController>();

            var e = Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, child, commands));

            Assert.That(e.Message, Does.StartWith("Unexpected view type: "));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IDocument, IDocumentView, IEnumerable{IChildViewPresenter}, ICommandManger)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateDocumentViewPresenter()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var document = Substitute.For<IDocument>();
            document.ID.Returns("Test");

            var view = Substitute.For<IDocumentView>();
            view.ID.Returns("Test");

            var presenter = factory.CreatePresenter(document, view, [], commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID, Is.EqualTo("DocumentController(Test)"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IDocument, IDocumentView, IEnumerable{IChildViewPresenter}, ICommandManger)"/> method throws an exception if any of the child view presenters does not implement the <see cref="IChildViewController"/> interface.
        /// </summary>
        [Test]
        public void TestCreateDocumentViewPresenterThrowsAnExceptionWhenInterfaceNotImplemented()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var document = Substitute.For<IDocument>();
            document.ID.Returns("Test");

            var view = Substitute.For<IDocumentView>();
            view.ID.Returns("Test");

            var child = Substitute.For<IChildViewPresenter>();

            var e = Assert.Throws<Exception>(() => factory.CreatePresenter(document, view, [child], commands));

            Assert.That(e.Message, Is.EqualTo("childPresenter does not implement the IChildViewController interface."));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method works correctly when view is <see cref="IDockableView"/>.
        /// </summary>
        [Test]
        public void TestCreateToolViewPresenter()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IDockableView>();
            view.ID.Returns(Views.WorkspaceExplorer);

            var child = Substitute.For<IChildViewPresenter, IChildViewController>();

            var presenter = factory.CreatePresenter(view, child, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID, Is.EqualTo($"{Views.WorkspaceExplorer}Controller"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, IChildViewPresenter, ICommandManager)"/> method throws an exception if the child view presenter does not implement the <see cref="IChildViewController"/> interface.
        /// </summary>
        [Test]
        public void TestCreateToolViewPresenterThrowsAnExceptionWhenInterfaceNotImplemented()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IDockableView>();

            var child = Substitute.For<IChildViewPresenter>();

            var e = Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, child, commands));

            Assert.That(e.Message, Is.EqualTo("childPresenter does not implement the IChildViewController interface."));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView, ICommandManger)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateViewPresenter()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IApplicationView>();
            view.ID.Returns(Views.Application);

            var presenter = factory.CreatePresenter(view, commands);

            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.ID, Is.EqualTo(Controllers.ApplicationViewController));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView)"/> method throws an exception if the view is of an unexpected view type.
        /// </summary>
        [Test]
        public void TestCreateViewPresenterThrowsAnExceptionForUnexpectedViewType()
        {
            var factory = new PresenterFactory(services, configuration, settings, events);

            var view = Substitute.For<IView>();

            var e = Assert.Throws<ArgumentException>(() => factory.CreatePresenter(view, commands));

            Assert.That(e.Message, Does.StartWith("Unexpected view type: "));
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
            viewConfiguration.GetChildViewConfiguration(Views.ChartSettings).Returns(childConfiguration1);
            viewConfiguration.GetChildViewConfiguration(Views.Chart).Returns(childConfiguration2);

            configuration.GetConfiguration(Views.ColourMagnitudeChart).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, configuration, settings, events);

            var document = Substitute.For<IDocument>();
            document.View.Returns(Views.ColourMagnitudeChart);

            var child1 = Substitute.For<IChartSettingsView>();
            child1.Name.Returns(Views.ChartSettings);
            child1.ID.Returns(Views.ChartSettings);

            var child2 = Substitute.For<IChartView>();
            child2.Name.Returns(Views.Chart);
            child2.ID.Returns(Views.Chart);

            var presenters = new List<IChildViewPresenter>(factory.CreatePresenters(document, [child1, child2], commands));

            Assert.That(presenters, Has.Count.EqualTo(2));

            Assert.That(presenters[0], Is.Not.Null);
            Assert.That(presenters[0].ID, Is.EqualTo($"ContentController({Views.ChartSettings})"));

            Assert.That(presenters[1], Is.Not.Null);
            Assert.That(presenters[1].ID, Is.EqualTo($"ContentController({Views.Chart})"));
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
            viewConfiguration.GetChildViewConfiguration(Views.ChartSettings).Returns(childConfiguration1);
            viewConfiguration.GetChildViewConfiguration(Views.Chart).Returns(childConfiguration2);

            configuration.GetConfiguration(Views.ColourMagnitudeChart).Returns(viewConfiguration);

            var factory = new PresenterFactory(services, configuration, settings, events);

            var document = Substitute.For<IDocument>();
            document.View.Returns(Views.ColourMagnitudeChart);

            var child1 = Substitute.For<IChartSettingsView>();
            child1.Name.Returns(Views.ChartSettings);
            child1.ID.Returns(Views.ChartSettings);

            var child2 = Substitute.For<IChartView>();
            child2.Name.Returns(Views.Chart);
            child2.ID.Returns(Views.Chart);

            var e = Assert.Throws<Exception>(() => factory.CreatePresenters(document, [child1, child2], commands));

            Assert.That(e.Message, Does.StartWith("Unknown type: "));
        }
    }
}
