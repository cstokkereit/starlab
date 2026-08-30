#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Application.Workspace.Documents;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Tests;
using Stratosoft.Commands;
using System.Drawing;
using static SkiaSharp.HarfBuzz.SKShaper;

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
            workspace.FileName.Returns(@"C:\Test\Workspace-1");
            workspace.Name.Returns("Workspace-1");

            view = Substitute.For<IWorkspaceExplorerView>();
            view.ID.Returns(ViewIDs.WorkspaceExplorer);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter(IWorkspaceExplorerView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new WorkspaceExplorerViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            Assert.That(presenter.ID, Is.EqualTo(new ControllerID("WorkspaceExplorer")));
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
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.AddProject()"/> method works correctly.
        /// </summary>
        [Test]
        [Ignore("Not implemented")]
        public void TestAddProject()
        {
            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.AddProject();
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Collapse(string)"/> method works correctly for a folder.
        /// </summary>
        [Test]
        public void TestCollapseFolder()
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

            workspace.Received(1).CollapseAll();

            view.Received(1).ExpandNode(Constants.Workspace);
            view.Received(1).CollapseNode(projectKey);
            view.Received(1).CollapseNode(folderKey);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Copy(string)"/> and <see cref="WorkspaceExplorerViewPresenter.Paste(string)"/> methods work correctly for a document.
        /// </summary>
        [Test]
        public void TestCopyAndPasteDocument()
        {
            var workspace = new Workspace(new WorkspaceDtoBuilder(@"C:\Test\Workspace-2")
                .AddProject("Project-1")
                .AddFolder("Workspace/Project-1/Folder-1")
                .AddFolder("Workspace/Project-1/Folder-2")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Chart-1.1", "Workspace-1/Project-1/Folder-1")
                .CreateWorkspace());

            var interactor = Substitute.For<IUseCase<ClipboardUseCaseArgs>>();

            factory.CreateCopyAndPasteUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.Copy("19542B1A-36A5-494F-B6B0-CB562FA36CAC");

            presenter.Paste("Workspace/Project-1/Folder-2");

            interactor.Received(1).Execute(Arg.Is<ClipboardUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-2" && args.Source == "19542B1A-36A5-494F-B6B0-CB562FA36CAC" && args.Destination == "Workspace/Project-1/Folder-2"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Copy(string)"/> and <see cref="WorkspaceExplorerViewPresenter.Paste(string)"/> methods work correctly for a folder.
        /// </summary>
        [Test]
        public void TestCopyAndPasteFolder()
        {
            var workspace = new Workspace(new WorkspaceDtoBuilder(@"C:\Test\Workspace-2")
                .AddProject("Project-1")
                .AddFolder("Workspace/Project-1/Folder-1")
                .AddFolder("Workspace/Project-1/Folder-2")
                .CreateWorkspace());

            var interactor = Substitute.For<IUseCase<ClipboardUseCaseArgs>>();

            factory.CreateCopyAndPasteUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.Copy("Workspace/Project-1/Folder-2");

            presenter.Paste("Workspace/Project-1/Folder-1");

            interactor.Received(1).Execute(Arg.Is<ClipboardUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-2" && args.Source == "Workspace/Project-1/Folder-2" && args.Destination == "Workspace/Project-1/Folder-1"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Copy(string)"/> method throws an exception when the target is an empty string.
        /// </summary>
        [Test]
        public void TestCopyThrowsExceptionWhenFolderNameIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.Copy(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.CreateDatabaseContextMenu(string, IMenuManager)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateDatabaseContextMenu()
        {
            var menu = Substitute.For<IMenuManager>();

            var presenter = CreatePresenter(true);

            presenter.CreateDatabaseContextMenu("Workspace/Project-1/Database", menu);

            //menu.Received(1).AddMenuSeparator();
            //menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            //menu.Received(4).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.CreateDocumentContextMenu(string, IMenuManager)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateDocumentContextMenu()
        {
            var menu = Substitute.For<IMenuManager>();

            var presenter = CreatePresenter(true);

            presenter.CreateDocumentContextMenu("Workspace/Project-1/Documents/Document-1", menu);

            menu.Received(1).AddMenuSeparator();
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            menu.Received(4).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.CreateFolderContextMenu(string, IMenuManager)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateFolderContextMenu()
        {
            var paste = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("Paste(Workspace/Project-1/Documents)").Returns((ICommand)paste);

            var menu = Substitute.For<IMenuManager>();

            var presenter = CreatePresenter(true);

            presenter.CreateFolderContextMenu("Workspace/Project-1/Documents", menu);

            menu.Received(2).AddMenuSeparator();
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>());
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            menu.Received(5).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());
            menu.Received(3).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());

            commands.Received(13).GetCommand(Arg.Any<string>());

            paste.Received(1).Enabled = false;
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.CreateProjectContextMenu(string, IMenuManager)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateProjectContextMenu()
        {
            var paste = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("Paste(Workspace/Project-1)").Returns((ICommand)paste);

            var menu = Substitute.For<IMenuManager>();

            var presenter = CreatePresenter(true);

            presenter.CreateProjectContextMenu("Workspace/Project-1", menu);

            menu.Received(2).AddMenuSeparator();
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>());
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>());
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            menu.Received(3).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());
            menu.Received(0).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            menu.Received(3).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());

            commands.Received(11).GetCommand(Arg.Any<string>());

            paste.Received(1).Enabled = false;
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.CreateWorkspaceContextMenu(string, IMenuManager)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCreateWorkspaceContextMenu()
        {
            var menu = Substitute.For<IMenuManager>();

            var presenter = CreatePresenter(true);

            presenter.CreateWorkspaceContextMenu(menu);

            menu.Received(2).AddMenuSeparator();
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>());
            menu.Received(2).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());
            menu.Received(1).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            
            commands.Received(6).GetCommand(Arg.Any<string>());
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Cut(string)"/> and <see cref="WorkspaceExplorerViewPresenter.Paste(string)"/> methods work correctly for a document.
        /// </summary>
        [Test]
        public void TestCutAndPasteDocument()
        {
            var workspace = new Workspace(new WorkspaceDtoBuilder(@"C:\Test\Workspace-2")
                .AddProject("Project-1")
                .AddFolder("Workspace/Project-1/Folder-1")
                .AddFolder("Workspace/Project-1/Folder-2")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Chart-1.1", "Workspace-1/Project-1/Folder-1")
                .CreateWorkspace());

            var interactor = Substitute.For<IUseCase<ClipboardUseCaseArgs>>();

            factory.CreateCutAndPasteUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.Cut("19542B1A-36A5-494F-B6B0-CB562FA36CAC");

            presenter.Paste("Workspace/Project-1/Folder-2");

            interactor.Received(1).Execute(Arg.Is<ClipboardUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-2" 
                && args.Source == "19542B1A-36A5-494F-B6B0-CB562FA36CAC" 
                && args.Destination == "Workspace/Project-1/Folder-2"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Cut(string)"/> and <see cref="WorkspaceExplorerViewPresenter.Paste(string)"/> methods work correctly for a folder.
        /// </summary>
        [Test]
        public void TestCutAndPasteFolder()
        {
            var workspace = new Workspace(new WorkspaceDtoBuilder(@"C:\Test\Workspace-2")
                .AddProject("Project-1")
                .AddFolder("Workspace/Project-1/Folder-1")
                .AddFolder("Workspace/Project-1/Folder-2")
                .CreateWorkspace());

            var interactor = Substitute.For<IUseCase<ClipboardUseCaseArgs>>();

            factory.CreateCutAndPasteUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.Cut("Workspace/Project-1/Folder-2");

            presenter.Paste("Workspace/Project-1/Folder-1");

            interactor.Received(1).Execute(Arg.Is<ClipboardUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-2" 
                && args.Source == "Workspace/Project-1/Folder-2" 
                && args.Destination == "Workspace/Project-1/Folder-1"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Cut(string)"/> method throws an exception when the target is an empty string.
        /// </summary>
        [Test]
        public void TestCutThrowsExceptionWhenFolderNameIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.Cut(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.DeleteDocument(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestDeleteDocument()
        {
            var interactor = Substitute.For<IUseCase<DeleteDocumentUseCaseArgs>>();

            factory.CreateDeleteDocumentUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.DeleteDocument("EBD0CED6-A2D0-4A77-A65D-69EB1A0585A8");

            interactor.Received(1).Execute(Arg.Is<DeleteDocumentUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-1" && args.DocumentID == "EBD0CED6-A2D0-4A77-A65D-69EB1A0585A8"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.DeleteFolder(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestDeleteFolder()
        {
            var interactor = Substitute.For<IUseCase<DeleteFolderUseCaseArgs>>();

            factory.CreateDeleteFolderUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.DeleteFolder("Workspace/Project-1/Documents");

            interactor.Received(1).Execute(Arg.Is<DeleteFolderUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-1" && args.Path == "Workspace/Project-1/Documents"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.DeleteFolder(string)"/> method throws an exception when the folder name is an empty string.
        /// </summary>
        [Test]
        public void TestDeleteFolderThrowsExceptionWhenFolderNameIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.DeleteFolder(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.DeleteProject(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestDeleteProject()
        {
            var interactor = Substitute.For<IUseCase<DeleteFolderUseCaseArgs>>();

            factory.CreateDeleteFolderUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.DeleteProject("Workspace/Project-1/Documents");

            interactor.Received(1).Execute(Arg.Is<DeleteFolderUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-1" && args.Path == "Workspace/Project-1/Documents"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.DeleteProject(string)"/> method throws an exception when the project name is an empty string.
        /// </summary>
        [Test]
        public void TestDeleteProjectThrowsExceptionWhenProjectNameIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.DeleteProject(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.FolderCollapsed(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestFolderCollapsed()
        {
            var folder = Substitute.For<IFolder>();

            workspace.GetFolder("Workspace/Project-1/Documents").Returns(folder);

            var presenter = CreatePresenter(true);

            presenter.FolderCollapsed("Workspace/Project-1/Documents");

            folder.Received(1).Collapse();
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.FolderExpanded(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestFolderExpanded()
        {
            var folder = Substitute.For<IFolder>();

            workspace.GetFolder("Workspace/Project-1/Documents").Returns(folder);

            var presenter = CreatePresenter(true);

            presenter.FolderExpanded("Workspace/Project-1/Documents");

            folder.Received(1).Expand();
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("WorkspaceExplorer"));
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

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.OnEvent(ActiveDocumentChangedEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOnActiveDocumentChangedEvent()
        {
            var modified = new Workspace(new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Chart-1.1", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", new ChartDtoBuilder().CreateChart())
                .CreateWorkspace());

            var presenter = CreatePresenter(true);

            commands.ClearReceivedCalls();

            presenter.OnEvent(new ActiveDocumentChangedEventArgs(modified));

            commands.Received(1).GetCommand("Synchronise");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.OnEvent(WorkspaceChangedEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOnWorkspaceChangedEvent()
        {
            var modified = new Workspace(new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Chart-1.1", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", new ChartDtoBuilder().CreateChart())
                .CreateWorkspace());

            var presenter = CreatePresenter(true);

            view.ClearReceivedCalls();

            presenter.OnEvent(new WorkspaceChangedEventArgs(modified));

            view.Received(1).Clear();
            view.Received(1).AddWorkspaceNode(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>());
            view.Received(1).AddProjectNode(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>());
            view.Received(1).AddProjectNode(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<int>());

            commands.Received(3).GetCommand("CollapseWorkspace");
            commands.Received(1).GetCommand("Synchronise");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.OpenDocument(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenDocument()
        {
            var id = new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAC");

            var document = Substitute.For<IDocument>();
            document.ID.Returns(id);

            workspace.GetDocument(id).Returns(document);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.OpenDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAC");

            controller.Received(1).ShowDocument(Arg.Is<IDocument>(document => document.ID.ToString() == "19542B1A-36A5-494F-B6B0-CB562FA36CAC"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Paste(string)"/> method throws an exception when the target is an empty string.
        /// </summary>
        [Test]
        public void TestPasteThrowsExceptionWhenFolderNameIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.Paste(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.ProjectCollapsed(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestProjectCollapsed()
        {
            var project = Substitute.For<IProject>();

            workspace.GetProject("Workspace/Project-1").Returns(project);

            var presenter = CreatePresenter(true);

            presenter.ProjectCollapsed("Workspace/Project-1");

            project.Received(1).Collapse();
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.ProjectExpanded(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestProjectExpanded()
        {
            var project = Substitute.For<IProject>();

            workspace.GetProject("Workspace/Project-1").Returns(project);

            var presenter = CreatePresenter(true);

            presenter.ProjectExpanded("Workspace/Project-1");

            project.Received(1).Expand();
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameDocument(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRenameDocument()
        {
            var interactor = Substitute.For<IUseCase<RenameDocumentUseCaseArgs>>();

            factory.CreateRenameDocumentUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.RenameDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "Document-2");

            interactor.Received(1).Execute(Arg.Is<RenameDocumentUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-1" && args.DocumentID == "19542B1A-36A5-494F-B6B0-CB562FA36CAC" && args.Name == "Document-2"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameDocument(string, string)"/> method throws an exception when the key is an empty string.
        /// </summary>
        [Test]
        public void TestRenameDocumentThrowsExceptionWhenKeyIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.RenameDocument(string.Empty, "Document-2"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameDocument(string, string)"/> method throws an exception when the name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameDocumentThrowsExceptionWhenNameIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.RenameDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAC", string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameFolder(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRenameFolder()
        {
            var interactor = Substitute.For<IUseCase<RenameFolderUseCaseArgs>>();

            factory.CreateRenameFolderUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.RenameFolder("Workspace-1/Project-1/Folder-1", "Folder-2");

            interactor.Received(1).Execute(Arg.Is<RenameFolderUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-1" && args.Path == "Workspace-1/Project-1/Folder-1" && args.Name == "Folder-2"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameFolder(string, string)"/> method throws an exception when the key is an empty string.
        /// </summary>
        [Test]
        public void TestRenameFolderThrowsExceptionWhenKeyIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.RenameFolder(string.Empty, "Folder-2"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameFolder(string, string)"/> method throws an exception when the name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameFolderThrowsExceptionWhenNameIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.RenameFolder("Workspace-1/Project-1/Folder-1", string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Rename(string)"/> method works correctly when provided with a folder key.
        /// </summary>
        [Test]
        public void TestRenameGivenAFolderKey()
        {
            var presenter = CreatePresenter(true);

            presenter.Rename("Workspace-1/Project-1/Folder-1");

            view.Received(1).EditNodeLabel("Workspace-1/Project-1/Folder-1");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Rename(string)"/> method works correctly when provided with the workspace key.
        /// </summary>
        [Test]
        public void TestRenameGivenTheWorkspaceKey()
        {
            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.Rename("Workspace");

            view.Received(1).SetNodeText("Workspace", "Workspace-1");
            view.Received(1).EditNodeLabel("Workspace");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameWorkspace(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestRenameWorkspace()
        {
            var interactor = Substitute.For<IUseCase<RenameWorkspaceUseCaseArgs>>();

            factory.CreateRenameWorkspaceUseCase(Arg.Any<IWorkspaceOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new WorkspaceChangedEventArgs(workspace));

            presenter.RenameWorkspace("Workspace-2");

            interactor.Received(1).Execute(Arg.Is<RenameWorkspaceUseCaseArgs>(args => args.Workspace.FileName == @"C:\Test\Workspace-1" && args.Name == "Workspace-2"));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.RenameWorkspace(string)"/> method throws an exception when the name is an empty string.
        /// </summary>
        [Test]
        public void TestRenameWorkspaceThrowsExceptionWhenKeyIsEmptyString()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.RenameWorkspace(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.SetSelectedFolder(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestSetSelectedFolder()
        {
            var presenter = CreatePresenter(true);

            presenter.SetSelectedFolder("Workspace/Project");

            workspace.Received(1).SetSelectedFolder("Workspace/Project");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.SetSelectedFolder(string)"/> method works correctly when given a database node key.
        /// </summary>
        [Test]
        public void TestSetSelectedFolderWithDatabaseNodeKey()
        {
            workspace.HasProject("Workspace/Project").Returns(true);

            var presenter = CreatePresenter(true);

            presenter.SetSelectedFolder("Workspace/Project/Database");

            workspace.Received(1).SetSelectedFolder("Workspace/Project");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.ShowMessage(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessage()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowMessage("Test message.");

            controller.Received(1).ShowMessage("StarLab", "Test message.", InteractionType.Error, InteractionResponses.OK);
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.Synchronise()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestSynchronise()
        {
            var document = Substitute.For<IDocument>();
            document.ID.Returns(new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAC"));

            workspace.ActiveDocument.Returns(document);

            var presenter = CreatePresenter(true);

            presenter.Synchronise();

            view.Received(1).FocusOnSelectedNode();

            view.Received(1).SelectNode("19542B1A-36A5-494F-B6B0-CB562FA36CAC");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.UpdateDocument(WorkspaceDTO, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateDocument()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);

            var presenter = CreatePresenter(true);

            var dto = new WorkspaceDTO()
            {
                FileName = @"C:\Test\Workspace-1"
            };

            presenter.UpdateDocument(dto, "19542B1A-36A5-494F-B6B0-CB562FA36CAC");

            port.Received(1).UpdateDocument(Arg.Is<WorkspaceDTO>(ws => ws.FileName == @"C:\Test\Workspace-1"), "19542B1A-36A5-494F-B6B0-CB562FA36CAC");
        }

        /// <summary>
        /// Test that the <see cref="WorkspaceExplorerViewPresenter.UpdateWorkspace(WorkspaceDTO)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateWorkspace()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);

            var presenter = CreatePresenter(true);

            var dto = new WorkspaceDTO()
            {
                FileName = @"C:\Test\Workspace-1"
            };

            presenter.UpdateWorkspace(dto);

            port.Received(1).UpdateWorkspace(Arg.Is<WorkspaceDTO>(ws => ws.FileName == @"C:\Test\Workspace-1"));
        }

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