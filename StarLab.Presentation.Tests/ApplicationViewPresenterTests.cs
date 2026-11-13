#pragma warning disable CS0252 // Possible unintended reference comparison; left hand side needs cast
#pragma warning disable CS8602 // Dereference of a possibly null reference.
#pragma warning disable CS8604 // Possible null reference argument.
#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using StarLab.Tests;
using Stratosoft.Commands;
using System.ComponentModel;
using System.Drawing;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ApplicationViewPresenter"/> class.
    /// </summary>
    public class ApplicationViewPresenterTests : PresenterTests
    {
        private IApplicationView view; // A mock of the IApplicationView interface that can be used in the unit tests.

        private WorkspaceDTO workspace; // A workspace DTO that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IApplicationView>();
            view.ID.Returns(ViewIDs.Application);

            var charts = new ChartDtoBuilder();

            workspace = new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAB", "ChartView", "Chart-1", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAB", charts.CreateChart())
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Chart-2", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", charts.CreateChart())
                .CreateWorkspace();
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ClearActiveDocument()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestClearActiveDocument()
        {
            IDocument? document = null;

            events.Publish(Arg.Do<ActiveDocumentChangedEventArgs>(e => document = e.Workspace.ActiveDocument));

            var presenter = CreatePresenter(view);

            presenter.UpdateWorkspace(workspace);

            presenter.ClearActiveDocument();

            events.Received(1).Publish(Arg.Any<ActiveDocumentChangedEventArgs>());

            Assert.That(document, Is.Null);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseActiveDocument()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestCloseActiveDocument()
        {
            var presenter = CreatePresenter(view);

            presenter.UpdateWorkspace(workspace);

            presenter.CloseActiveDocument();

            view.Received(1).CloseActiveDocument();
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseWorkspace()"/> method works correctly when the workspace has not been modified.
        /// </summary>
        [Test]
        public void TestCloseWorkspace()
        {
            var presenter = CreatePresenter(view);

            OpenWorkspace(GetPath("Workspace-1.slw"));

            var port = Substitute.For<IApplicationOutputPort>();

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);

            events.ClearReceivedCalls();

            presenter.CloseWorkspace();

            controller.Received(0).ShowMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<InteractionResponses>());

            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "FB97BA9C-7B51-43F9-9025-ED54A90DB990"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "6E82FF05-3C75-4A59-95D3-AF176EF2571E"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "577CB05B-C02F-499C-BC36-DEB47AFF9029"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "F7F6EC62-39D9-411E-AB6A-AF547DCA8DC5"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));

            events.Received(1).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);

            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            events.Received(0).Publish(Arg.Any<WorkspaceSavedEventArgs>());

            port.Received(0).SetWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseWorkspace()"/> method works correctly when the workspace has been modified and the user selects No (discard changes).
        /// </summary>
        [Test]
        public void TestCloseWorkspaceAndDiscardChanges()
        {
            var presenter = CreatePresenter(view);

            var workspace = OpenWorkspace(GetPath("Workspace-1.slw"));

            var dto = CreateWorkspaceDTO(workspace);

            dto.Projects[0].Documents.RemoveAt(0);

            presenter.UpdateWorkspace(dto);

            var port = Substitute.For<IApplicationOutputPort>();

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);
            controller.ShowMessage(Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.No);
            
            events.ClearReceivedCalls();

            presenter.CloseWorkspace();

            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "FB97BA9C-7B51-43F9-9025-ED54A90DB990"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "6E82FF05-3C75-4A59-95D3-AF176EF2571E"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "577CB05B-C02F-499C-BC36-DEB47AFF9029"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "F7F6EC62-39D9-411E-AB6A-AF547DCA8DC5"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));

            events.Received(1).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);

            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            events.Received(0).Publish(Arg.Any<WorkspaceSavedEventArgs>());

            port.Received(0).SetWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseWorkspace()"/> method works correctly when the workspace has been modified and the user selects Yes (save changes).
        /// </summary>
        [Test]
        public void TestCloseWorkspaceAndSaveChanges()
        {
            var presenter = CreatePresenter(view);

            var workspace = OpenWorkspace(GetPath("Workspace-1.slw"));

            var dto = CreateWorkspaceDTO(workspace);

            dto.Projects[0].Documents.RemoveAt(0);

            presenter.UpdateWorkspace(dto);

            var port = Substitute.For<IApplicationOutputPort>();

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);
            controller.ShowMessage(Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.Yes);

            events.ClearReceivedCalls();

            presenter.CloseWorkspace();

            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "FB97BA9C-7B51-43F9-9025-ED54A90DB990"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "6E82FF05-3C75-4A59-95D3-AF176EF2571E"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "577CB05B-C02F-499C-BC36-DEB47AFF9029"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "F7F6EC62-39D9-411E-AB6A-AF547DCA8DC5"));
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));

            events.Received(1).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);

            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            events.Received(1).Publish(Arg.Any<WorkspaceSavedEventArgs>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseWorkspace()"/> method works correctly when the workspace has been modified and the user selects Cancel.
        /// </summary>
        [Test]
        public void TestCloseWorkspaceWhenCancelled()
        {
            var presenter = CreatePresenter(view);

            var workspace = OpenWorkspace(GetPath("Workspace-1.slw"));

            var dto = CreateWorkspaceDTO(workspace);

            dto.Projects[0].Documents.RemoveAt(0);

            presenter.UpdateWorkspace(dto);

            var port = Substitute.For<IApplicationOutputPort>();

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);
            controller.ShowMessage(Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.Cancel);

            events.ClearReceivedCalls();

            presenter.CloseWorkspace();

            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "FB97BA9C-7B51-43F9-9025-ED54A90DB990"));
            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));
            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "6E82FF05-3C75-4A59-95D3-AF176EF2571E"));
            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "577CB05B-C02F-499C-BC36-DEB47AFF9029"));
            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "F7F6EC62-39D9-411E-AB6A-AF547DCA8DC5"));
            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));

            events.Received(0).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);

            events.Received(0).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            events.Received(0).Publish(Arg.Any<WorkspaceSavedEventArgs>());

            port.Received(0).SetWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ApplicationViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            Assert.That(presenter, Is.Not.Null);

            view.Received(1).Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, container.Resolve<ISessionContext>(), null, container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, null, container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), null));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), null, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(null, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CreateView(string)"/> method returns a new view when it does not already exist within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCreateView()
        {
            var mock = Substitute.For<IDockableView>();

            controller.GetView(Arg.Is(new ViewID("19542B1A-36A5-494F-B6B0-CB562FA36CAA"))).Returns(mock);

            var presenter = CreatePresenter(view);

            presenter.UpdateWorkspace(workspace);

            var dockable = presenter.CreateView("19542B1A-36A5-494F-B6B0-CB562FA36CAA");

            Assert.That(dockable, Is.SameAs(mock));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CreateView(string)"/> method returns the existing dockable view when it already exists within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCreateDockableViewWhenViewAlreadyExists()
        {
            var mock = Substitute.For<IDockableView>();

            controller.GetView(Arg.Any<ViewID>()).Returns(mock);

            var presenter = CreatePresenter(view);

            presenter.UpdateWorkspace(workspace);

            var dockable = presenter.CreateView(ViewNames.WorkspaceExplorer);

            Assert.That(dockable, Is.SameAs(mock));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CreateView(string)"/> method returns the existing document view when it already exists within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCreateDocumentViewWhenViewAlreadyExists()
        {
            var mock = Substitute.For<IDockableView>();

            controller.GetView(Arg.Is<IDocument>(d => d.ID == new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAB"))).Returns(mock);

            var presenter = CreatePresenter(view);

            presenter.UpdateWorkspace(workspace);

            var dockable = presenter.CreateView("19542B1A-36A5-494F-B6B0-CB562FA36CAB");

            Assert.That(dockable, Is.SameAs(mock));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Exit()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestExit()
        {
            var presenter = CreatePresenter(view);

            presenter.Exit();

            view.Received(1).CloseAll();
            view.Received(1).Close();
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ChildControllers"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetChildControllers()
        {
            var presenter = CreatePresenter(view);

            Assert.That(presenter.ChildControllers, Is.Empty);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(view);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("ApplicationWindow"));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            controller.GetOutputPort<IApplicationOutputPort>().Returns(port);

            settings.Workspace.Returns(GetPath("Default.slw"));

            var presenter = new ApplicationViewPresenter(view, container.Resolve<ISessionContext>(), container.Resolve<ICommandManager>(), container.Resolve<IServiceRegistry>(), events);

            presenter.Initialise(controller);

            view.Received(0).AddMenuSeparator();
            view.Received().AddMenuSeparator(Arg.Any<string>());
            view.Received().AddMenuItem(Arg.Any<string>(), Arg.Any<string>());
            view.Received(0).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>());
            view.Received(0).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            view.Received(0).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());
            view.Received().AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>());
            view.Received().AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            view.Received().AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());

            view.Received().AddToolbarButton(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());

            port.Received(1).SetWorkspace(Arg.Is<WorkspaceDTO>(w => w.FileName == Path.Combine(AppContext.BaseDirectory, "Resources", "Default.slw")));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.NewWorkspace()"/> method works correctly.
        /// </summary>
        [Test]
        [Ignore("Not implemented")]
        public void TestNewWorkspace()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(view);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OnEvent(ActiveDocumentChangedEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOnEvent()
        {
            var workspace = Substitute.For<IWorkspace>();

            var document = Substitute.For<IDocument>();

            workspace.ActiveDocument.Returns(document);

            var dockable = Substitute.For<IDockableView>();

            controller.GetView(document).Returns(dockable);

            var presenter = CreatePresenter(view);

            presenter.OnEvent(new ActiveDocumentChangedEventArgs(workspace));

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View == dockable));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenDocument(string)"/> method works correctly.
        /// </summary>
        [Test]
        [Ignore("Not implemented")]
        public void TestOpenDocument()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenWorkspace()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenWorkspace()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            view.ShowOpenFileDialog(Arg.Any<string>(), Arg.Any<string>()).Returns(GetPath("Workspace-1.slw"));

            var presenter = CreatePresenter(view, port);

            presenter.OpenWorkspace();

            port.Received(1).SetWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenWorkspace()"/> method shows an error message when the workspace file does not exist.
        /// </summary>
        [Test]
        public void TestOpenWorkspaceShowsMessageWhenFileNotFound()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            view.ShowOpenFileDialog(Arg.Any<string>(), Arg.Any<string>()).Returns(@"C:\Workspace-1");

            var presenter = CreatePresenter(view, port);

            view.ClearReceivedCalls();

            presenter.OpenWorkspace();

            view.Received(1).ShowMessage(Arg.Any<string>(), InteractionType.Error, InteractionResponses.OK);

            port.Received(0).SetWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenWorkspace()"/> method throws an exception when the file name is an empty string.
        /// </summary>
        [Test]
        public void TestOpenWorkspaceThrowsExceptionWhenFilenameIsEmptyString()
        {
            var port = Substitute.For<IApplicationOutputPort>();

            view.ShowOpenFileDialog(Arg.Any<string>(), Arg.Any<string>()).Returns(string.Empty);

            var presenter = CreatePresenter(view, port);

            view.ClearReceivedCalls();

            presenter.OpenWorkspace();

            view.Received(1).ShowMessage(Arg.Any<string>(), InteractionType.Error, InteractionResponses.OK);

            port.Received(0).SetWorkspace(Arg.Any<WorkspaceDTO>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.SaveWorkspace()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestSaveWorkspace()
        {
            var presenter = CreatePresenter(view);

            presenter.SaveWorkspace();

            events.Received(1).Publish(Arg.Any<WorkspaceSavedEventArgs>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.SetActiveDocument(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestSetActiveDocument()
        {
            var presenter = CreatePresenter(view);

            OpenWorkspace(GetPath("Workspace-1.slw"));

            var documentID = new DocumentID("E24FA5BD-FB27-41C9-AED1-25B837A6C152");

            var document = CreateView(documentID);

            presenter.CreateView(document.ID.ToString());

            presenter.SetActiveDocument(document.ID);

            events.Received(1).Publish(Arg.Is<ActiveDocumentChangedEventArgs>(e => e.Workspace.ActiveDocument.ID.ToString() == "E24FA5BD-FB27-41C9-AED1-25B837A6C152"));
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method does not update the default file name when the workspace file name is an empty string.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenFileNameIsEmptyString()
        {
            var filename = GetPath("Default.slw");

            var workspace = new WorkspaceDtoBuilder(string.Empty).CreateWorkspace();

            settings.Workspace.Returns(filename);

            var presenter = CreatePresenter(view);
            
            presenter.SetWorkspace(workspace);

            view.Received(1).CloseAll();

            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.FileName == string.Empty));

            Assert.That(container.Resolve<ISessionContext>().Settings.Workspace, Is.EqualTo(filename));
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method updates the default file name when the workspace file name is not an empty string and differs from the default.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenFileNameIsNotEmptyString()
        {
            var filename = @"C:\Test\Workspace-1";

            var workspace = new WorkspaceDtoBuilder(filename).CreateWorkspace();

            var presenter = CreatePresenter(view);

            presenter.SetWorkspace(workspace);

            view.Received(1).CloseAll();
            view.Received(0).SetLayout(Arg.Any<string>());

            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.FileName == filename));

            Assert.That(container.Resolve<ISessionContext>().Settings.Workspace, Is.EqualTo(filename));
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method works correctly when the workspace layout is an empty string.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenLayoutIsEmptyString()
        {
            var workspace = new WorkspaceDtoBuilder(string.Empty).CreateWorkspace();

            var presenter = CreatePresenter(view);

            presenter.SetWorkspace(workspace);

            view.Received(0).SetLayout(Arg.Any<string>());

            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.Layout == string.Empty));
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method works correctly when the workspace layout is not an empty string.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenLayoutIsNotEmptyString()
        {
            var workspace = new WorkspaceDtoBuilder(string.Empty).SetLayout(WorkspaceDtoBuilder.EMPTY_LAYOUT).CreateWorkspace();

            var presenter = CreatePresenter(view);

            presenter.SetWorkspace(workspace);

            view.Received(1).SetLayout(Arg.Is(WorkspaceDtoBuilder.EMPTY_LAYOUT));

            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.Layout == WorkspaceDtoBuilder.EMPTY_LAYOUT));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Show(IView)"/> method works correctly for a dialog view.
        /// </summary>
        [Test]
        public void TestShowDialogView()
        {
            var dialog = Substitute.For<IDialogView>();

            var presenter = CreatePresenter(view);

            presenter.Show(dialog);

            view.Received(1).Show(dialog);

            events.Received(0).Publish(Arg.Any<WorkspaceChangedEventArgs>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Show(IView)"/> method works correctly for a dockable view.
        /// </summary>
        [Test]
        public void TestShowDockableView()
        {
            var dockable = Substitute.For<IDockableView>();

            var presenter = CreatePresenter(view);

            presenter.Show(dockable);

            view.Received(1).Show(dockable);

            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowMessage(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessageWithTwoParameters()
        {
            var presenter = CreatePresenter(view);

            presenter.ShowMessage("Caption", "Message");

            controller.Received(1).ShowMessage("Caption", "Message");
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowMessage(string, string, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessageWithThreeParameters()
        {
            var presenter = CreatePresenter(view);

            presenter.ShowMessage("Caption", "Message", InteractionResponses.OK);

            controller.Received(1).ShowMessage("Caption", "Message", InteractionResponses.OK);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowMessage(string, string, InteractionType, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessageWithFourParameters()
        {
            var presenter = CreatePresenter(view);

            presenter.ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);

            view.Received(1).ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowOpenFileDialog(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowOpenFileDialog()
        {
            var presenter = CreatePresenter(view);

            presenter.ShowOpenFileDialog("Title", "Filter");

            view.Received(1).ShowOpenFileDialog("Title", "Filter");
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowSaveFileDialog(string, string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSaveFileDialog()
        {
            var presenter = CreatePresenter(view);

            presenter.ShowSaveFileDialog("Title", "Filter", "Extension");

            view.Received(1).ShowSaveFileDialog("Title", "Filter", "Extension");
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.UpdateDocument(WorkspaceDTO, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateDocument()
        {
            var presenter = CreatePresenter(view);

            var dto = CreateWorkspaceDTO(OpenWorkspace(GetPath("Workspace-1.slw")));

            var docController = Substitute.For<IDocumentController>();

            dto.Projects[0].Documents[0].Name = "New Name";

            var id = new DocumentID(dto.Projects[0].Documents[0].ID);

            controller.GetController(Arg.Is<IDocument>(d => d.ID == id)).Returns(docController);

            events.ClearReceivedCalls();

            presenter.UpdateDocument(dto, id.ToString());

            docController.Received(1).UpdateDocument(Arg.Is<IDocument>(d => d.ID == id));

            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.GetDocument(id).Name == "New Name"));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var presenter = CreatePresenter(view);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View == view));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ViewClosing(CancelEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewClosing()
        {
            var presenter = CreatePresenter(view);

            var e = new CancelEventArgs();

            presenter.ViewClosing(e);

            controller.Received(1).Exit();

            Assert.That(e.Cancel, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ViewClosing(CancelEventArgs)"/> method works correctly when the dialog is closing because the <see cref="ApplicationViewPresenter.Exit()"/> method was called.
        /// </summary>
        [Test]
        public void TestViewClosingAfterExitCalled()
        {
            var presenter = CreatePresenter(view);

            presenter.Exit();

            var e = new CancelEventArgs();

            presenter.ViewClosing(e);

            Assert.That(e.Cancel, Is.False);

            controller.Received(0).Exit();

            view.Received(1).CloseAll();
            view.Received(1).Close();
        }
    }
}
