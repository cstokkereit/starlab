using StarLab.Presentation.Help;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Presentation.Workspace.Documents.Charts;
using StarLab.UI;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="PresenterFactory"/> class.
    /// </summary>
    public class PresenterFactoryTests : PresentationTests
    {
        /// <summary>
        /// Test that the <see cref="PresenterFactory.PresenterFactory(Castle.Windsor.IWindsorContainer, IUseCaseFactory, IApplicationSettings, IMapper, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            // Arrange
            var sut = new PresenterFactory(container, factory, settings, mapper, events);

            // Assert
            Assert.That(sut, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IChildView)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreatePresenterFromChildView()
        {
            // Arrange
            var sut = new PresenterFactory(container, factory, settings, mapper, events);

            var view = Substitute.For<IChartSettingsView>();
            view.Name.Returns($"ColourMagnitudeChartView::{Views.ChartSettings}");

            // Act
            var presenter = sut.CreatePresenter(view);

            // Assert
            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.Name, Is.EqualTo(Controllers.ChartSettingsController));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IDocument, IDocumentView)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreatePresenterFromDocumentAndView()
        {
            // Arrange
            var sut = new PresenterFactory(container, factory, settings, mapper, events);

            var document = Substitute.For<IDocument>();
            document.Name.Returns("Test Document");
            document.ID.Returns("Test");

            var view = Substitute.For<IDocumentView>();
            
            // Act
            var presenter = sut.CreatePresenter(document, view);

            // Assert
            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.Name, Is.EqualTo(Controllers.GetDocumentControllerName("Test")));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreatePresenterFromView()
        {
            // Arrange
            var sut = new PresenterFactory(container, factory, settings, mapper, events);

            var viewFactory = new ViewFactory(sut);

            var view = viewFactory.CreateView(Views.Application, "Test");

            view.Detach();

            // Act
            var presenter = sut.CreatePresenter(view);

            // Assert
            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.Name, Is.EqualTo(Controllers.ApplicationViewController));
            Assert.That(view.Text, Is.EqualTo("Test"));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreatePresenterFromViewDefinitionAndChildView()
        {
            // Arrange
            var sut = new PresenterFactory(container, factory, settings, mapper, events);

            var view = Substitute.For<IAboutView>();
            view.Name.Returns(Views.About);

            var definition = Substitute.For<IViewDefinition>();
            definition.Name.Returns(Views.About);

            // Act
            var presenter = sut.CreatePresenter(definition, view);

            // Assert
            Assert.That(presenter, Is.Not.Null);
            Assert.That(presenter.Name, Is.EqualTo(Controllers.GetContentControllerName(Views.About)));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="PresenterFactory.CreatePresenter(IView)"/> method throws an exception if the view is of an unexpected type.
        /// </summary>
        [Test]
        public void TestCreatePresenterThrowsAnExceptionForUnexpectedViewType()
        {
            // Arrange
            var sut = new PresenterFactory(container, factory, settings, mapper, events);

            var view = Substitute.For<IView>();

            // Act
            var e = Assert.Throws<ArgumentException>(() => sut.CreatePresenter(view));

            // Assert
            Assert.That(e.Message, Does.StartWith("Unexpected view type: "));
        }
    }
}
