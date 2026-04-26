#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Configuration;
using Stratosoft.Commands;

namespace StarLab.Presentation.Workspace.WorkspaceExplorer
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="WorkspaceExplorerViewPresenter"/> class.
    /// </summary>
    public class WorkspaceExplorerViewPresenterTests : PresentationTests
    {
        private IWorkspaceExplorerView view; // A mock of the IWorkspaceExplorerView interface that can be used in the unit tests.

        private IWorkspace workspace; // A mock of the IWorkspace interface that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            workspace = Substitute.For<IWorkspace>();
            workspace.FileName.Returns(@"C:\Test\Workspace");

            view = Substitute.For<IWorkspaceExplorerView>();
            view.ID.Returns(Views.WorkspaceExplorer);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new WorkspaceExplorerViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo($"ContentController({Views.WorkspaceExplorer})"));
            view.Received().Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkspaceExplorerViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkspaceExplorerViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkspaceExplorerViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkspaceExplorerViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new WorkspaceExplorerViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.AddChart(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddChart()
        {
            var presenter = CreatePresenter(true);

            presenter.AddChart("Workspace/Project-1/Charts");
            
            controller.Received(1).ShowAddChartDialog(workspace, "Workspace/Project-1/Charts");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.AddFolder(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddFolder()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO, string>>();

            factory.CreateAddFolderUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.AddFolder("Workspace/Project-1/Documents/Charts");

            interactor.Received(1).Execute(Arg.Is<WorkspaceDTO>(ws => ws.FileName == @"C:\Test\Workspace"), "Workspace/Project-1/Documents/Charts");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.AddProject(string)"/> method works correctly.
        /// </summary>
        [Test]
        [Ignore("Not implemented yet.")]
        public void TestAddProject()
        {
            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.ClearClipboard()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestClearClipboard()
        {

        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Collapse(string)"/> method works correctly for a folder.
        /// </summary>
        [Test]
        public void TestCollapseFolder ()
        {
            var folderKey = "Workspace/Project-1/Documents/Charts";

            var folder = Substitute.For<IFolder>();
            folder.Key.Returns(folderKey);
            folder.Expanded.Returns(false);

            var projectKey = "Workspace/Project-1";

            var project = Substitute.For<IProject>();
            project.Key.Returns(projectKey);
            project.Expanded.Returns(true);

            workspace.Projects.Returns(new List<IProject>([project]));
            workspace.HasProject(projectKey).Returns(true);
            workspace.GetProject(projectKey).Returns(project);

            workspace.Folders.Returns(new List<IFolder>([folder]));
            workspace.HasFolder(folderKey).Returns(true);
            workspace.GetFolder(folderKey).Returns(folder);

            var presenter = CreatePresenter(true);

            presenter.Collapse(folderKey);

            workspace.Received(0).Collapse();
            project.Received(0).CollapseAll();
            folder.Received(1).CollapseAll();
            
            view.Received(1).ExpandNode(Constants.Workspace);
            view.Received(0).CollapseNode(projectKey);
            view.Received(1).ExpandNode(projectKey);
            view.Received(1).CollapseNode(folderKey);
            view.Received(0).ExpandNode(folderKey);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Collapse(string)"/> method works correctly for a project.
        /// </summary>
        [Test]
        public void TestCollapseProject()
        {
            var folderKey = "Workspace/Project-1/Documents/Charts";

            var folder = Substitute.For<IFolder>();
            folder.Key.Returns(folderKey);
            folder.Expanded.Returns(false);

            var projectKey = "Workspace/Project-1";

            var project = Substitute.For<IProject>();
            project.Key.Returns(projectKey);
            project.Expanded.Returns(false);

            workspace.Projects.Returns(new List<IProject>([project]));
            workspace.HasProject(projectKey).Returns(true);
            workspace.GetProject(projectKey).Returns(project);

            workspace.Folders.Returns(new List<IFolder>([folder]));
            workspace.HasFolder(folderKey).Returns(true);
            workspace.GetFolder(folderKey).Returns(folder);

            var presenter = CreatePresenter(true);

            presenter.Collapse(projectKey);

            project.Received(1).CollapseAll();

            view.Received(1).ExpandNode(Constants.Workspace);
            view.Received(1).CollapseNode(projectKey);
            view.Received(1).CollapseNode(folderKey);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Collapse(string)"/> method works correctly for the workspace.
        /// </summary>
        [Test]
        public void TestCollapseWorkspace()
        {
            var folderKey = "Workspace/Project-1/Documents/Charts";

            var folder = Substitute.For<IFolder>();
            folder.Key.Returns(folderKey);
            folder.Expanded.Returns(false);

            var projectKey = "Workspace/Project-1";

            var project = Substitute.For<IProject>();
            project.Key.Returns(projectKey);
            project.Expanded.Returns(false);

            workspace.Projects.Returns(new List<IProject>([project]));
            workspace.HasProject(projectKey).Returns(true);
            workspace.GetProject(projectKey).Returns(project);

            workspace.Folders.Returns(new List<IFolder>([folder]));
            workspace.HasFolder(folderKey).Returns(true);
            workspace.GetFolder(folderKey).Returns(folder);

            var presenter = CreatePresenter(true);

            presenter.Collapse("Workspace");

            workspace.Received(1).Collapse();

            view.Received(1).ExpandNode(Constants.Workspace);
            view.Received(1).CollapseNode(projectKey);
            view.Received(1).CollapseNode(folderKey);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Copy(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCopy()
        {



        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));

            Assert.That(e.Message, Is.EqualTo("The WorkspaceExplorerViewPresenter has already been initialised."));
        }

        //public void CreateDocumentContextMenu(string id, IMenuManager manager)
        //public void CreateFolderContextMenu(string folder, IMenuManager manager)
        //public void CreateProjectContextMenu(string project, IMenuManager manager)
        //public void CreateWorkspaceContextMenu(IMenuManager manager)
        //public void Cut(string key)
        //public void DeleteDocument(string id)
        //public void DeleteFolder(string key)
        //public void DeleteProject(string key)
        //public void FolderCollapsed(string key)
        //public void FolderExpanded(string key)
        //public override void Initialise(IApplicationController controller)
        //public void OnEvent(ActiveDocumentChangedEventArgs args)
        //public void OnEvent(WorkspaceChangedEventArgs args)
        //public void OpenDocument(string key)
        //public void Paste(string key)
        //public void ProjectCollapsed(string key)
        //public void ProjectExpanded(string key)
        //public void Rename(string key)
        //public void RenameDocument(string key, string name)
        //public void RenameFolder(string key, string name)
        //public void RenameFolder(string key)
        //public void RenameWorkspace(string name)
        //public void ShowMessage(string message)
        //public void Synchronise()
        //public void UpdateClipboard(string key)
        //public void UpdateDocument(WorkspaceDTO dto, string id)
        //public void UpdateWorkspace(WorkspaceDTO dto)
        //public void WorkspaceCollapsed()
        //public void WorkspaceExpanded()

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="WorkspaceExplorerViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="WorkspaceExplorerViewPresenter"/>.</returns>
        private WorkspaceExplorerViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new WorkspaceExplorerViewPresenter(view, context, commands, services, events);

            if (initialise) presenter.Initialise(controller);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            return presenter;
        }
    }
}