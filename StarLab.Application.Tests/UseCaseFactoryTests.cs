using AutoMapper;
using StarLab.Application.Data;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents.Charts;
using StarLab.Application.Workspace.Documents.Charts.Overlays;

namespace StarLab.Application
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="UseCaseFactory"/> class.
    /// </summary>
    public class UseCaseFactoryTests
    {
        private readonly IMapper mapper = Substitute.For<IMapper>(); // A mock of the IMapper interface for use in the unit tests.

        private readonly IOverlayDataProvider overlays = Substitute.For<IOverlayDataProvider>(); // A mock of the IOverlayDataProvider interface for use in the unit tests.

        private readonly IQueryBuilder queries = Substitute.For<IQueryBuilder>(); // A mock of the IQueryBuilder interface for use in the unit tests.

        private readonly IDatabaseManager databases = Substitute.For<IDatabaseManager>(); // A mock of the IDatabaseManager interface for use in the unit tests.

        private readonly ISerialisationService serialiser = Substitute.For<ISerialisationService>(); // A mock of the ISerialisationService interface for use in the unit tests.

        /// <summary>
        /// Initialises the class level variables before each test.
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            mapper.ClearReceivedCalls();
        }

        /// <summary>
        /// Cleans up after the tests have been completed.
        /// </summary>
        [OneTimeTearDown]
        public void TearDown()
        {
            databases.Dispose();
        }

        /// <summary>
        /// Test that the static <see cref="UseCaseFactory(IMapper, IDatabase, IQueryBuilder, ISerialisationService)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var factory = new UseCaseFactory(mapper, databases, queries, overlays, serialiser);

            Assert.That(factory, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateAddChartOverlayUseCase(IChartOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateAddChartOverlayUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateAddChartOverlayUseCase(Substitute.For<IChartOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateAddDocumentUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateAddDocumentUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateAddDocumentUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateAddFolderUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateAddFolderUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateAddFolderUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateAddProjectUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateAddProjectUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateAddProjectUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateCopyAndPasteUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateCopyAndPasteUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateCopyAndPasteUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateCutAndPasteUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateCutAndPasteUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateCutAndPasteUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateDeleteDocumentUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateDeleteDocumentUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateDeleteDocumentUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateDeleteFolderUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateDeleteFolderUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateDeleteFolderUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateOpenWorkspaceUseCase(IApplicationOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateOpenWorkspaceUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateOpenWorkspaceUseCase(Substitute.For<IApplicationOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreatePlotChartUseCase(IChartOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreatePlotChartUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreatePlotChartUseCase(Substitute.For<IChartOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateRemoveChartOverlayUseCase(IChartOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateRemoveChartOverlayUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateRemoveChartOverlayUseCase(Substitute.For<IChartOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateRenameDocumentUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateRenameDocumentUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateRenameDocumentUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateRenameFolderUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateRenameFolderUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateRenameFolderUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateRenameWorkspaceUseCase(IWorkspaceOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateRenameWorkspaceUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateRenameWorkspaceUseCase(Substitute.For<IWorkspaceOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateSaveWorkspaceUseCase(IApplicationOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateSaveWorkspaceUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateSaveWorkspaceUseCase(Substitute.For<IApplicationOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateUpdateChartPreviewUseCase(IChartOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateUpdateChartPreviewUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateUpdateChartPreviewUseCase(Substitute.For<IChartOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="UseCaseFactory.CreateUpdateChartUseCase(IApplicationOutputPort)"/> function works correctly.
        /// </summary>
        [Test]
        public void TestCreateUpdateChartUseCase()
        {
            var factory = CreateFactory();

            var useCase = factory.CreateUpdateChartUseCase(Substitute.For<IApplicationOutputPort>());

            Assert.That(useCase, Is.Not.Null);
        }

        /// <summary>
        /// Creates a <see cref="UseCaseFactory"/> ready for testing.
        /// </summary>
        /// <returns>An instance of the <see cref="IUseCaseFactory"/> interface.</returns>
        private IUseCaseFactory CreateFactory()
        {
            return new UseCaseFactory(mapper, databases, queries, overlays, serialiser);
        }
    }
}
