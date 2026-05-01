#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using StarLab.Tests;
using Stratosoft.Commands;
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

        /// <summary>
        /// Registers the dependencies with the IoC container and initialises the class level variables before each test.
        /// </summary>
        public override void SetUp()
        {
            base.SetUp();

            view = Substitute.For<IApplicationView>();
            view.ID.Returns("View1");

            var chart = new ChartDtoBuilder().CreateChart();

            workspace = new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddDocument("Document1", "ChartView", "Document-1", "Workspace-1/Project-1/Folder-1")
                .AddChart("Document1", chart)
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
            commands.Received(1).GetCommand("CloseWorkspace");
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
            var interactor = Substitute.For<IUseCase<WorkspaceDTO>>();

            factory.CreateSaveWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            controller.ShowMessage(Resources.StarLab, Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.No);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            commands.ClearReceivedCalls();

            IWorkspace? workspace1 = null;
            IWorkspace? workspace2 = null;

            events.Publish(Arg.Do<WorkspaceClosedEventArgs>(e => workspace1 = e.Workspace), true);
            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => workspace2 = e.Workspace));

            presenter.CloseWorkspace();

            interactor.Received(0).Execute(Arg.Any<WorkspaceDTO>());
            commands.Received(1).GetCommand("CloseWorkspace");
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
            var interactor = Substitute.For<IUseCase<WorkspaceDTO>>();

            factory.CreateSaveWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            controller.ShowMessage(Resources.StarLab, Resources.WorkspaceClosing, InteractionResponses.YesNoCancel).Returns(InteractionResult.Yes);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);
    
            commands.ClearReceivedCalls();

            IWorkspace? workspace1 = null;
            IWorkspace? workspace2 = null;

            events.Publish(Arg.Do<WorkspaceClosedEventArgs>(e => workspace1 = e.Workspace), true);
            events.Publish(Arg.Do<WorkspaceChangedEventArgs>(e => workspace2 = e.Workspace));

            presenter.CloseWorkspace();

            interactor.Received(1).Execute(Arg.Is<WorkspaceDTO>(e => e.FileName == @"C:\Workspace-1"));
            commands.Received(1).GetCommand("CloseWorkspace");
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

            Assert.That(presenter.ID, Is.EqualTo($"{Views.Application}Controller"));
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
        /// Test that the <see cref="ApplicationViewPresenter.ID"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetID()
        {
            var presenter = CreatePresenter(false);

            Assert.That(presenter.ID, Is.EqualTo("ApplicationViewController"));
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
        /// Test that the <see cref="ApplicationViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenDocument(string)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenDocument()
        {
            var dockable = Substitute.For<IDockableView>();

            controller.GetView(Arg.Is<IDocument>(d => d.ID == "Document1")).Returns(dockable);

            var presenter = CreatePresenter(true);

            presenter.UpdateWorkspace(workspace);

            presenter.OpenDocument("Document1");

            view.Received(1).Show(Arg.Is(dockable));
            events.Received(1).Publish(Arg.Any<WorkspaceChangedEventArgs>());
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.OpenWorkspace()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestOpenWorkspace()
        {
            var interactor = Substitute.For<IUseCase<string>>();

            factory.CreateOpenWorkspaceUseCase(Arg.Any<IApplicationOutputPort>()).Returns(interactor);

            view.ShowOpenFileDialog(Arg.Any<string>(), Arg.Any<string>()).Returns(@"C:\Workspace-1");

            var presenter = CreatePresenter(true);

            presenter.OpenWorkspace();

            interactor.Received(1).Execute(@"C:\Workspace-1");
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

            presenter.SetActiveDocument("Document1");

            events.Received(1).Publish(Arg.Any<ActiveDocumentChangedEventArgs>());

            Assert.That(document, Is.Not.Null);
            Assert.That(document.ID, Is.EqualTo("Document1"));
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.ViewActivated()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestViewActivated()
        {
            var presenter = CreatePresenter(true);

            presenter.ViewActivated();

            events.Received(1).Publish(Arg.Is<ActiveViewChangedEventArgs>(e => e.View != null && e.View.ID == "View1"));
        }

        //IDockableView? CreateView(string id);
        //void ViewClosing(CancelEventArgs e);
        //void NewWorkspace();
        //IEnumerable<IChildViewController> ChildControllers { get; }
        //void Show(IView view
        //InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses);
        //string ShowOpenFileDialog(string title, string filter);
        //string ShowSaveFileDialog(string title, string filter, string extension);
        //void OpenDocument(string id);
        //void SetWorkspace(WorkspaceDTO dto);
        //void UpdateDocument(WorkspaceDTO dto, string documentId);
        //InteractionResult ShowMessage(string caption, string message, InteractionType type, InteractionResponses responses);
        //InteractionResult ShowMessage(string caption, string message, InteractionResponses responses);
        //void ShowMessage(string caption, string message);
        //void OnEvent(TEventType e);

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
