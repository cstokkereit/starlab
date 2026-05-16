#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Configuration;
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
    public class ApplicationViewPresenterTests : PresentationTests
    {
        private IApplicationView view; // A mock of the IApplicationView interface that can be used in the unit tests.

        private WorkspaceDTO workspace; // A workspace DTO that can be used in the unit tests.

        private DocumentID documentID; // A document ID that can be used in the unit tests.

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            documentID = new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAB");

            view = Substitute.For<IApplicationView>();
            view.ID.Returns(ViewIDs.Application);

            var chart = new ChartDtoBuilder().CreateChart();

            workspace = new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAB", "ChartView", "Document-1", "Workspace-1/Project-1/Folder-1")
                .AddDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Document-2", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAB", chart)
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

            var presenter = CreatePresenter(true);

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
            var presenter = CreatePresenter(true);

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
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            var interactor = Substitute.For<IUseCase<WorkspaceDTO>>();

            factory.CreateSaveWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            presenter.SaveWorkspace();

            interactor.ClearReceivedCalls();
            commands.ClearReceivedCalls();

            IWorkspace? workspace1 = null;
            IWorkspace? workspace2 = null;

            events.Publish(Arg.Do<WorkspaceClosedEventArgs>(e => workspace1 = e.Workspace), true);
            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => workspace2 = e.Workspace));

            presenter.CloseWorkspace();

            controller.Received(0).ShowMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<InteractionResponses>());
            interactor.Received(0).Execute(Arg.Any<WorkspaceDTO>());
            command.Received(1).Enabled = false;
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.Name == "Document-1"));

            events.Received(1).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);

            Assert.That(workspace1, Is.Not.Null);
            Assert.That(workspace1.FileName, Is.EqualTo(@"C:\Workspace-1"));

            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            Assert.That(workspace2, Is.Not.Null);
            Assert.That(workspace2.FileName, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseWorkspace()"/> method works correctly when the workspace has been modified and the user selects No (discard changes).
        /// </summary>
        [Test]
        public void TestCloseWorkspaceAndDiscardChanges()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            var interactor = Substitute.For<IUseCase<WorkspaceDTO>>();

            factory.CreateSaveWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            controller.ShowMessage(Resources.StarLab, Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.No);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            IWorkspace? workspace1 = null;
            IWorkspace? workspace2 = null;

            events.Publish(Arg.Do<WorkspaceClosedEventArgs>(e => workspace1 = e.Workspace), true);
            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => workspace2 = e.Workspace));

            presenter.CloseWorkspace();

            interactor.Received(0).Execute(Arg.Any<WorkspaceDTO>());
            command.Received(1).Enabled = false;
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.Name == "Document-1"));

            events.Received(1).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);

            Assert.That(workspace1, Is.Not.Null);
            Assert.That(workspace1.FileName, Is.EqualTo(@"C:\Workspace-1"));

            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            Assert.That(workspace2, Is.Not.Null);
            Assert.That(workspace2.FileName, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseWorkspace()"/> method works correctly when the workspace has been modified and the user selects Yes (save changes).
        /// </summary>
        [Test]
        public void TestCloseWorkspaceAndSaveChanges()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            var interactor = Substitute.For<IUseCase<WorkspaceDTO>>();

            factory.CreateSaveWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            controller.ShowMessage(Resources.StarLab, Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.Yes);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);
    
            IWorkspace? workspace1 = null;
            IWorkspace? workspace2 = null;

            events.Publish(Arg.Do<WorkspaceClosedEventArgs>(e => workspace1 = e.Workspace), true);
            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => workspace2 = e.Workspace));

            presenter.CloseWorkspace();

            interactor.Received(1).Execute(Arg.Is<WorkspaceDTO>(e => e.FileName == @"C:\Workspace-1"));
            command.Received(1).Enabled = false;
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.Name == "Document-1"));

            events.Received(1).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);

            Assert.That(workspace1, Is.Not.Null);
            Assert.That(workspace1.FileName, Is.EqualTo(@"C:\Workspace-1"));

            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            Assert.That(workspace2, Is.Not.Null);
            Assert.That(workspace2.FileName, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CloseWorkspace()"/> method works correctly when the workspace has been modified and the user selects Cancel.
        /// </summary>
        [Test]
        public void TestCloseWorkspaceWhenCancelled()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO>>();

            factory.CreateSaveWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            controller.ShowMessage(Resources.StarLab, Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.Cancel);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            commands.ClearReceivedCalls();

            presenter.CloseWorkspace();

            interactor.Received(0).Execute(Arg.Any<WorkspaceDTO>());
            commands.Received(0).GetCommand("CloseWorkspace");
            controller.Received(0).CloseDocument(Arg.Any<IDocument>());
            events.Received(0).Publish(Arg.Any<WorkspaceClosedEventArgs>(), true);
            events.Received(0).Publish(Arg.Any<WorkspaceChangedEventArgs>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var presenter = new ApplicationViewPresenter(view, context, commands, services, events);

            Assert.That(presenter, Is.Not.Null);

            view.Received(1).Attach(Arg.Is(presenter));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the commands argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenCommandsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, context, null, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the context argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, null, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the events argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenEventsIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, context, commands, services, null));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the services argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenServicesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(view, context, commands, null, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter(IApplicationView, ISessionContext, ICommandManager, IServiceRegistry, IEventAggregator)"/> constructor throws an exception when the view argument is null.
        /// </summary>
        [Test]
        public void TestConstructionThrowsExceptionWhenViewIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new ApplicationViewPresenter(null, context, commands, services, events));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CreateView(string)"/> method returns a new view when it does not already exist within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCreateView()
        {
            var dockable = Substitute.For<IDockableView>();

            controller.GetView(Arg.Is(new ViewID("19542B1A-36A5-494F-B6B0-CB562FA36CAA"))).Returns(dockable);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            var view = presenter.CreateView("19542B1A-36A5-494F-B6B0-CB562FA36CAA");

            Assert.That(view, Is.SameAs(dockable));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CreateView(string)"/> method returns the existing dockable view when it already exists within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCreateDockableViewWhenViewAlreadyExists()
        {
            var dockable = Substitute.For<IDockableView>();

            controller.GetView(Arg.Any<ViewID>()).Returns(dockable);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            var view = presenter.CreateView(ViewNames.WorkspaceExplorer);

            Assert.That(view, Is.SameAs(dockable));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.CreateView(string)"/> method returns the existing document view when it already exists within the workspace hierarchy.
        /// </summary>
        [Test]
        public void TestCreateDocumentViewWhenViewAlreadyExists()
        {
            var dockable = Substitute.For<IDockableView>();

            controller.GetView(Arg.Is<IDocument>(d => d.ID == documentID)).Returns(dockable);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            var view = presenter.CreateView("19542B1A-36A5-494F-B6B0-CB562FA36CAB");

            Assert.That(view, Is.SameAs(dockable));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Exit()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestExit()
        {
            var presenter = CreatePresenter(true);

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
            var presenter = CreatePresenter(true);

            Assert.That(presenter.ChildControllers, Is.Empty);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.ID.ToString(), Is.EqualTo("ApplicationWindow"));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Initialise(IApplicationController)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestInitialise()
        {
            var interactor = Substitute.For<IUseCase<string>>();

            factory.CreateOpenWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            context.Settings.Workspace.Returns(@"C:\Workspace-1");

            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);

            view.Received(0).AddMenuSeparator();
            view.Received(7).AddMenuSeparator(Arg.Any<string>());
            view.Received(6).AddMenuItem(Arg.Any<string>(), Arg.Any<string>());
            view.Received(0).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>());
            view.Received(0).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            view.Received(0).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());
            view.Received(2).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>());
            view.Received(4).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<ICommand>());
            view.Received(4).AddMenuItem(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());

            view.Received(2).AddToolbarButton(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<Image>(), Arg.Any<ICommand>());

            interactor.Received(1).Execute(@"C:\Workspace-1");
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
            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OnEvent(ActiveDocumentChangedEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOnEvent()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("Close").Returns((ICommand)command);

            var ws = Substitute.For<IWorkspace>();
            ws.ActiveDocument.Returns(Substitute.For<IDocument>());

            var presenter = CreatePresenter(true);

            presenter.OnEvent(new ActiveDocumentChangedEventArgs(ws));

            command.Received(1).Enabled = true;
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenDocument(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenDocument()
        {
            var dockable = Substitute.For<IDockableView>();

            controller.GetView(Arg.Is<IDocument>(d => d.ID == documentID)).Returns(dockable);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            presenter.OpenDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAB");

            view.Received(1).Show(Arg.Is(dockable));
            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenWorkspace()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenWorkspace()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            var interactor = Substitute.For<IUseCase<string>>();

            factory.CreateOpenWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            view.ShowOpenFileDialog(Arg.Any<string>(), Arg.Any<string>()).Returns(@"C:\Workspace-1");

            var presenter = CreatePresenter(true);

            presenter.OpenWorkspace();

            interactor.Received(1).Execute(@"C:\Workspace-1");
            command.Received(1).Enabled = true;
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenWorkspace()"/> method throws an exception when the file name is an empty string.
        /// </summary>
        [Test]
        public void TestOpenWorkspaceThrowsExceptionWhenFilenameIsEmptyString()
        {
            view.ShowOpenFileDialog(Arg.Any<string>(), Arg.Any<string>()).Returns(string.Empty);

            var presenter = CreatePresenter(true);

            Assert.Throws<ArgumentException>(() => presenter.OpenWorkspace());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.SaveWorkspace()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestSaveWorkspace()
        {
            var interactor = Substitute.For<IUseCase<WorkspaceDTO>>();

            factory.CreateSaveWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            WorkspaceDTO? dto = null;

            interactor.Execute(Arg.Do<WorkspaceDTO>(e => dto = e));

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            presenter.SaveWorkspace();

            interactor.Received(1).Execute(Arg.Any<WorkspaceDTO>());

            Assert.That(dto, Is.Not.Null);
            Assert.That(dto.FileName, Is.EqualTo(@"C:\Workspace-1"));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.SetActiveDocument(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestSetActiveDocument()
        {
            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            IDocument? document = null;

            events.Publish(Arg.Do<ActiveDocumentChangedEventArgs>(e => document = e.Workspace.ActiveDocument));

            var viewID = new ViewID(documentID);

            var view = Substitute.For<IDocumentView>();
            view.DocumentID.Returns(documentID);

            controller.GetView(Arg.Is<ViewID>(id => id == viewID)).Returns(view);

            presenter.SetActiveDocument(viewID);

            events.Received(1).Publish(Arg.Any<ActiveDocumentChangedEventArgs>());

            Assert.That(document, Is.Not.Null);
            Assert.That(document.ID, Is.EqualTo(documentID));
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method does not update the default file name when the workspace file name is an empty string.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenFileNameIsEmptyString()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            IWorkspace? ws = null;

            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => ws = e.Workspace));

            context.Settings.Workspace.Returns(@"C:\Workspace-1");

            workspace.FileName = string.Empty;

            var presenter = CreatePresenter(true);

            presenter.SetWorkspace(workspace);

            view.Received(1).CloseAll();
            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());
            context.Settings.Received(0).Workspace = string.Empty;
            command.Received(1).Enabled = true;
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method updates the default file name when the workspace file name is not an empty string and differs from the default.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenFileNameIsNotEmptyString()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            IWorkspace? ws = null;

            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => ws = e.Workspace));

            context.Settings.Workspace.Returns(@"C:\Workspace-0");

            var presenter = CreatePresenter(true);

            presenter.SetWorkspace(workspace);

            view.Received(1).CloseAll();
            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());
            context.Settings.Received(1).Workspace = @"C:\Workspace-1";
            command.Received(1).Enabled = true;
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method works correctly when the workspace layout is an empty string.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenLayoutIsEmptyString()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            IWorkspace? ws = null;

            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => ws = e.Workspace));

            context.Settings.Workspace.Returns(@"C:\Workspace-1");

            var presenter = CreatePresenter(true);

            presenter.SetWorkspace(workspace);

            view.Received(1).CloseAll();
            view.Received(0).SetLayout(Arg.Any<string>());
            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());
            command.Received(1).Enabled = true;

            Assert.That(ws, Is.Not.Null);
            Assert.That(ws.FileName, Is.EqualTo(@"C:\Workspace-1"));
            Assert.That(ws.Layout, Is.EqualTo(string.Empty));
        }

        /// <summary>
        /// Tests that the <see cref="ApplicationViewPresenter.SetWorkspace(WorkspaceDTO)"/> method works correctly when the workspace layout is not an empty string.
        /// </summary>
        [Test]
        public void TestSetWorkspaceWhenLayoutIsNotEmptyString()
        {
            var command = Substitute.For<IComponentCommand, ICommand>();

            commands.GetCommand("CloseWorkspace").Returns((ICommand)command);

            IWorkspace? ws = null;

            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => ws = e.Workspace));

            context.Settings.Workspace.Returns(@"C:\Workspace-1");

            workspace.Layout = "<Layout></Layout>";

            var presenter = CreatePresenter(true);

            presenter.SetWorkspace(workspace);

            view.Received(1).CloseAll();
            view.Received(1).SetLayout(Arg.Any<string>());
            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());
            command.Received(1).Enabled = true;

            Assert.That(ws, Is.Not.Null);
            Assert.That(ws.FileName, Is.EqualTo(@"C:\Workspace-1"));
            Assert.That(ws.Layout, Is.EqualTo("<Layout></Layout>"));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Show(IView)"/> method works correctly for a dialog view.
        /// </summary>
        [Test]
        public void TestShowDialogView()
        {
            var dialog = Substitute.For<IDialogView>();

            var presenter = CreatePresenter(true);

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

            var presenter = CreatePresenter(true);

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
            var presenter = CreatePresenter(true);

            presenter.ShowMessage("Caption", "Message");

            controller.Received(1).ShowMessage("Caption", "Message");
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowMessage(string, string, Message)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessageWithThreeParameters()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowMessage("Caption", "Message", InteractionResponses.OK);

            controller.Received(1).ShowMessage("Caption", "Message", InteractionResponses.OK);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowMessage(string, string, InteractionType, InteractionResponses)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowMessageWithFourParameters()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);

            view.Received(1).ShowMessage("Caption", "Message", InteractionType.Question, InteractionResponses.YesNoCancel);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowOpenFileDialog(string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowOpenFileDialog()
        {
            var presenter = CreatePresenter(true);
            
            presenter.ShowOpenFileDialog("Title", "Filter");

            view.Received(1).ShowOpenFileDialog("Title", "Filter");
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ShowSaveFileDialog(string, string, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestShowSaveFileDialog()
        {
            var presenter = CreatePresenter(true);

            presenter.ShowSaveFileDialog("Title", "Filter", "Extension");

            view.Received(1).ShowSaveFileDialog("Title", "Filter", "Extension");
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.UpdateDocument(WorkspaceDTO, string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestUpdateDocument()
        {
            var documentID = new DocumentID("19542B1A-36A5-494F-B6B0-CB562FA36CAC");
            var controllerID = new ControllerID(documentID);

            var dc = Substitute.For<IDocumentController>();
            dc.DocumentID.Returns(documentID);
            dc.ID.Returns(controllerID);

            IDocument? doc = null;

            dc.UpdateDocument(Arg.Do<IDocument>(arg => doc = arg));

            controller.GetController(Arg.Any<IDocument>()).Returns(dc);

            IWorkspace? ws = null;

            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => ws = e.Workspace));

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            presenter.UpdateDocument(new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddDocument("19542B1A-36A5-494F-B6B0-CB562FA36CAC", "ChartView", "Document-1.1", "Workspace-1/Project-1/Folder-1")
                .AddChart("19542B1A-36A5-494F-B6B0-CB562FA36CAC", new ChartDtoBuilder().CreateChart())
                .CreateWorkspace(), "19542B1A-36A5-494F-B6B0-CB562FA36CAC");

            dc.Received(1).UpdateDocument(Arg.Any<IDocument>());
            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());

            Assert.That(ws, Is.Not.Null);

            var document = ws.GetDocument(documentID);

            Assert.That(document, Is.Not.Null);
            Assert.That(document.ID, Is.EqualTo(documentID));
            Assert.That(document.Name, Is.EqualTo("Document-1.1"));

            Assert.That(doc, Is.Not.Null);
            Assert.That(doc.ID, Is.EqualTo(documentID));
            Assert.That(doc.Name, Is.EqualTo("Document-1.1"));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var presenter = CreatePresenter(true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == ViewIDs.Application));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ViewClosing(CancelEventArgs)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewClosing()
        {
            var command = Substitute.For<ICommand>();

            commands.GetCommand("Exit").Returns(command);

            var presenter = CreatePresenter(true);

            var e = new CancelEventArgs();

            presenter.ViewClosing(e);

            command.Received(1).Execute();

            Assert.That(e.Cancel, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ViewClosing(CancelEventArgs)"/> method works correctly when the dialog is closing because the <see cref="ApplicationViewPresenter.Exit()"/> method was called.
        /// </summary>
        [Test]
        public void TestViewClosingAfterExitCalled()
        {
            var command = Substitute.For<ICommand>();

            commands.GetCommand("Exit").Returns(command);

            var presenter = CreatePresenter(true);

            presenter.Exit();

            var e = new CancelEventArgs();

            presenter.ViewClosing(e);

            command.Received(0).Execute();

            Assert.That(e.Cancel, Is.False);
        }

        /// <summary>
        /// A factory method that creates a new instance of the <see cref="ApplicationViewPresenter"/> class.
        /// </summary>
        /// <param name="initialise">true to initialise the presenter; false otherwise.</param>
        /// <returns>Returns the newly created <see cref="ApplicationViewPresenter"/>.</returns>
        private ApplicationViewPresenter CreatePresenter(bool initialise)
        {
            var presenter = new ApplicationViewPresenter(view, context, commands, services, events);

            if (initialise) presenter.Initialise(controller);

            return presenter;
        }
    }
}
