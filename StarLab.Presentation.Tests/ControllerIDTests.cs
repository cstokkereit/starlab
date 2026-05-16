using StarLab.Presentation.Workspace;
using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ControllerID"/> class.
    /// </summary>
    public class ControllerIDTests
    {
        /// <summary>
        /// Test that the <see cref="ControllerID(ViewID)"/> constructor works correctly for a view of type <see cref="IApplicationView"/>.
        /// </summary>
        [Test]
        public void TestConstructionFromApplicationView()
        {
            var view = Substitute.For<IApplicationView>();
            view.ID.Returns(new ViewID("Application"));

            var id = new ControllerID(view);

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("ApplicationWindow"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(ViewID)"/> constructor works correctly for a view of type <see cref="IChildView"/>.
        /// </summary>
        [Test]
        public void TestConstructionFromChildView()
        {
            var view = Substitute.For<IChildView>();
            view.ID.Returns(new ViewID("OptionsView"));

            var id = new ControllerID(view);

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("OptionsView"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(ViewID)"/> constructor works correctly for a view of type <see cref="IDialogView"/>.
        /// </summary>
        [Test]
        public void TestConstructionFromDialogView()
        {
            var view = Substitute.For<IDialogView>();
            view.ID.Returns(new ViewID("Options"));

            var id = new ControllerID(view);

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("OptionsDialog"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(DocumentID)"/> constructor works correctly/>.
        /// </summary>
        [Test]
        public void TestConstructionFromDocumentID()
        {
            var id = new ControllerID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(ViewID)"/> constructor works correctly for a view of type <see cref="IDocumentView"/>.
        /// </summary>
        [Test]
        public void TestConstructionFromDocumentView()
        {
            var documentID = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            var view = Substitute.For<IDocumentView>();
            view.ID.Returns(new ViewID(documentID));
            view.DocumentID.Returns(documentID);
            
            var id = new ControllerID(view);

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromString()
        {
            var id = new ControllerID("ApplicationController");

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("ApplicationController"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(IView)"/> constructor works correctly for a view of type <see cref="IDockableView"/>
        /// </summary>
        [Test]
        public void TestConstructionFromToolView()
        {
            var view = Substitute.For<IDockableView>();
            view.ID.Returns(new ViewID("WorkspaceExplorer"));

            var id = new ControllerID(view);

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("WorkspaceExplorerTool"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(ViewID)"/> constructor throws an exception for an unexpected view type.
        /// </summary>
        [Test]
        public void TestConstructionFromUnexpectedViewType()
        {
            var view = Substitute.For<IView>();
            view.ID.Returns(new ViewID("View"));

            Assert.Throws<ArgumentException>(() => new ControllerID(view));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID(ViewID)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromViewID()
        {
            var id = new ControllerID(ViewIDs.WorkspaceExplorer);

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("WorkspaceExplorer"));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.Equals(ControllerID)"/> method works correctly with different controller IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIds()
        {
            var id1 = new ControllerID("ControllerA");
            var id2 = new ControllerID("ControllerB");

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.Equals(object)"/> method works correctly with different controller IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIdsAsObject()
        {
            var id1 = new ControllerID("ControllerA");
            object id2 = new ControllerID("ControllerB");

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.Equals(object)"/> method works correctly with different types.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentTypes()
        {
            var id1 = new ControllerID("ControllerA");
            var id2 = new ViewID("ControllerA");

            Assert.False(id1.Equals(id2));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.Equals(ControllerID)"/> method works correctly with matching controller IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIds()
        {
            var id1 = new ControllerID("ControllerA");
            var id2 = new ControllerID("ControllerA");

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.Equals(object)"/> method works correctly with matching controller IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIdsAsObject()
        {
            var id1 = new ControllerID("ControllerA");
            object id2 = new ControllerID("ControllerA");

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.Equals(ControllerID)"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestEqualsWithNullId()
        {
            ControllerID? id1 = new ControllerID("ControllerA");
            ControllerID? id2 = null;

            Assert.That(id1.Equals(id2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.Equals(object)"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestEqualsWithNullIdAsObject()
        {
            ControllerID? id1 = new ControllerID("ControllerA");
            object? id2 = null;

            Assert.That(id1.Equals(id2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.GetHashCode()"/> method works correctly with different controller IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForDifferentIds()
        {
            var id1 = new ControllerID("ControllerA");
            var id2 = new ControllerID("ControllerB");

            Assert.That(id1.GetHashCode(), Is.Not.EqualTo(id2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="ControllerID.GetHashCode()"/> method works correctly with matching controller IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForMatchingIds()
        {
            var id1 = new ControllerID("ControllerA");
            var id2 = new ControllerID("ControllerA");

            Assert.That(id1.GetHashCode(), Is.EqualTo(id2.GetHashCode()));
        }
    }
}
