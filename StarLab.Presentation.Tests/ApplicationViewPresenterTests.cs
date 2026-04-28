#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

using StarLab.Application;
using StarLab.Application.Workspace;
using StarLab.Presentation.Configuration;
using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;
using StarLab.Shared.Properties;
using StarLab.Tests;
using Stratosoft.Commands;

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

            var chart = new ChartDtoBuilder().CreateChart();

            workspace = new WorkspaceDtoBuilder(@"C:\Workspace-1")
                .AddProject("Project-1")
                .AddFolder("Workspace-1/Project-1/Folder-1")
                .AddDocument("Chart-1", "ChartView", "Document-1", "Workspace-1/Project-1/Folder-1")
                .AddChart("Chart-1", chart)
                .CreateWorkspace();
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

            presenter.CloseWorkspace();

            controller.Received(0).ShowMessage(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<InteractionResponses>());
            interactor.Received(0).Execute(Arg.Any<WorkspaceDTO>());
            commands.Received(1).GetCommand("CloseWorkspace");
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.Name == "Document-1"));
            events.Received(1).Publish(Arg.Is<WorkspaceClosedEventArgs>(e => e.Workspace.FileName == @"C:\Workspace-1"), true);
            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.FileName == ""));
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

            presenter.CloseWorkspace();

            interactor.Received(0).Execute(Arg.Any<WorkspaceDTO>());
            commands.Received(1).GetCommand("CloseWorkspace");
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.Name == "Document-1"));
            events.Received(1).Publish(Arg.Is<WorkspaceClosedEventArgs>(e => e.Workspace.FileName == @"C:\Workspace-1"), true);
            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.FileName == ""));
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

            presenter.CloseWorkspace();

            interactor.Received(1).Execute(Arg.Is<WorkspaceDTO>(e => e.FileName == @"C:\Workspace-1"));
            commands.Received(1).GetCommand("CloseWorkspace");
            controller.Received(1).CloseDocument(Arg.Is<IDocument>(d => d.Name == "Document-1"));
            events.Received(1).Publish(Arg.Is<WorkspaceClosedEventArgs>(e => e.Workspace.FileName == @"C:\Workspace-1"), true);
            events.Received(1).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.FileName == ""));
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
            controller.Received(0).CloseDocument(Arg.Is<IDocument>(d => d.Name == "Document-1"));
            events.Received(0).Publish(Arg.Is<WorkspaceClosedEventArgs>(e => e.Workspace.FileName == @"C:\Workspace-1"), true);
            events.Received(0).Publish(Arg.Is<WorkspaceChangedEventArgs>(e => e.Workspace.FileName == ""));
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
            var presenter = CreatePresenter(false);

            presenter.Initialise(controller);

            events.Received(1).Subsribe(presenter);
        }

        /// <summary>
        /// Test that the <see cref="ApplicationViewPresenter.Initialise(IApplicationController)"/> method throws an exception when already initialised.
        /// </summary>
        [Test]
        public void TestInitialiseThrowsAnExceptionWhenAlreadyInitialised()
        {
            var presenter = CreatePresenter(true);

            var e = Assert.Throws<InvalidOperationException>(() => presenter.Initialise(controller));
        }

        //void ClearActiveDocument();
        //IDockableView? CreateView(string id);
        //void SetActiveDocument(string id);
        //void ViewActivated();
        //void ViewClosing(CancelEventArgs e);
        //void CloseActiveDocument();
        //void NewWorkspace();
        //void OpenWorkspace();
        //void SaveWorkspace();
        //IEnumerable<IChildViewController> ChildControllers { get; }
        //void Initialise(IApplicationController controller);
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
