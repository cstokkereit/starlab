using StarLab.Presentation.Workspace.Documents;

namespace StarLab.Presentation
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ViewID"/> class.
    /// </summary>
    public class ViewIDTests
    {
        /// <summary>
        /// Test that the <see cref="ViewID()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var id = new ViewID();

            Assert.That(id, Is.Not.Null);
            Assert.That(Guid.TryParse(id.ToString(), out var _), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="ViewID(Document)"/> constructor works correctly/>.
        /// </summary>
        [Test]
        public void TestConstructionFromDocument()
        {
            var document = Substitute.For<IDocument>();
            document.ID.Returns(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));

            var id = new ViewID(document);

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
        }

        /// <summary>
        /// Test that the <see cref="ViewID(DocumentID)"/> constructor works correctly/>.
        /// </summary>
        [Test]
        public void TestConstructionFromDocumentID()
        {
            var id = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
        }

        /// <summary>
        /// Test that the <see cref="ViewID(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromString()
        {
            var id = new ViewID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
        }

        /// <summary>
        /// Test that the <see cref="ViewID.Equals(ViewID)"/> method works correctly with different view IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIds()
        {
            var id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            var id2 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AB"));

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ViewID.Equals(object)"/> method works correctly with different view IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIdsAsObject()
        {
            var id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            object id2 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AB"));

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ViewID.Equals(object)"/> method works correctly with different types.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentTypes()
        {
            var id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            var id2 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.False(id1.Equals(id2));
        }

        /// <summary>
        /// Test that the <see cref="ViewID.Equals(ViewID)"/> method works correctly with matching view IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIds()
        {
            var id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            var id2 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ViewID.Equals(object)"/> method works correctly with matching view IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIdsAsObject()
        {
            var id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            object id2 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ViewID.Equals(ViewID)"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestEqualsWithNullId()
        {
            ViewID? id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            ViewID  ? id2 = null;

            Assert.That(id1.Equals(id2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="ViewID.Equals(object)"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestEqualsWithNullIdAsObject()
        {
            ViewID? id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            object? id2 = null;

            Assert.That(id1.Equals(id2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="ViewID.GetHashCode()"/> method works correctly with different view IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForDifferentIds()
        {
            var id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            var id2 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AB"));

            Assert.That(id1.GetHashCode(), Is.Not.EqualTo(id2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="ViewID.GetHashCode()"/> method works correctly with matching view IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForMatchingIds()
        {
            var id1 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
            var id2 = new ViewID(new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));

            Assert.That(id1.GetHashCode(), Is.EqualTo(id2.GetHashCode()));
        }
    }
}
