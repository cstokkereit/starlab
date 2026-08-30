using StarLab.Tests;

namespace StarLab.Presentation.Workspace
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Workspace"/> class.
    /// </summary>
    public class WorkspaceTests
    {
        private IWorkspace workspace;

        /// <summary>
        /// Creates the test database prior to running the tests.
        /// </summary>
        [OneTimeSetUp]
        public void InitialiseFixture()
        {
            var workspaceBuilder = new WorkspaceDtoBuilder(@"C:\Users\TestUser\Documents\TestWorkspace.slw");

            var projectBuilder = new ProjectDTOBuilder();

            workspaceBuilder.AddProject(projectBuilder.SetName("Project-1")
                .SetDatabase("localhost", 27017, "Database-1")
                .CreateProject());

            workspaceBuilder.AddProject(projectBuilder.SetName("Project-2")
                .SetDatabase("localhost", 27017, "Database-2")
                .CreateProject());

            workspace = new Workspace(workspaceBuilder.CreateWorkspace());
        }

        /// <summary>
        /// Test that the <see cref="IWorkspace.FileName"/> property works correctly.
        /// </summary>
        [Test]
        public void TestGetFileName()
        {
            Assert.That(workspace.FileName, Is.EqualTo(@"C:\Users\TestUser\Documents\TestWorkspace.slw"));
        }

        /// <summary>
        /// Test that the <see cref="IWorkspace.Project"/> property works correctly for the first project.
        /// </summary>
        [Test]
        public void TestGetProject1()
        {
            var project = workspace.GetProject("Workspace/Project-1");

            Assert.That(project, Is.Not.Null);

            Assert.That(project.Name, Is.EqualTo("Project-1"));

            var database = project.Database;

            Assert.That(database, Is.Not.Null);

            Assert.That(database.Host, Is.EqualTo("localhost"));
            Assert.That(database.Port, Is.EqualTo(27017));
            Assert.That(database.Name, Is.EqualTo("Database-1"));
        }

        /// <summary>
        /// Test that the <see cref="IWorkspace.Project"/> property works correctly for the second project.
        /// </summary>
        [Test]
        public void TestGetProject2()
        {
            var project = workspace.GetProject("Workspace/Project-2");

            Assert.That(project, Is.Not.Null);

            Assert.That(project.Name, Is.EqualTo("Project-2"));

            var database = project.Database;

            Assert.That(database, Is.Not.Null);

            Assert.That(database.Host, Is.EqualTo("localhost"));
            Assert.That(database.Port, Is.EqualTo(27017));
            Assert.That(database.Name, Is.EqualTo("Database-2"));
        }
    }
}
