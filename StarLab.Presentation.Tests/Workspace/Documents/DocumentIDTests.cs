namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="DocumentID"/> class.
    /// </summary>
    public class DocumentIDTests
    {
        /// <summary>
        /// Test that the <see cref="DocumentID()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var id = new DocumentID();

            Assert.That(id, Is.Not.Null);
            Assert.That(Guid.TryParse(id.ToString(), out var _), Is.True);
        }

        /// <summary>
        /// Test that the <see cref="DocumentID(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromString()
        {
            var id = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id, Is.Not.Null);
            Assert.That(id.ToString(), Is.EqualTo("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID(string)"/> constructor throws an exception for an invalid ID.
        /// </summary>
        [Test]
        public void TestConstructionFromInvalidID()
        {
            Assert.Throws<FormatException>(() => new DocumentID("5542258B-B3B0-4A61-84FF"));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.Equals(DocumentID)"/> method works correctly with different document IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIds()
        {
            var id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AB");

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.Equals(object)"/> method works correctly with different document IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIdsAsObject()
        {
            var id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            object id2 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AB");

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.Equals(object)"/> method works correctly with different types.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentTypes()
        {
            var id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ViewID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.False(id1.Equals(id2));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.Equals(DocumentID)"/> method works correctly with matching document IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIds()
        {
            var id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.Equals(object)"/> method works correctly with matching document IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIdsAsObject()
        {
            var id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            object id2 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.Equals(DocumentID)"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestEqualsWithNullId()
        {
            DocumentID? id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            DocumentID? id2 = null;

            Assert.That(id1.Equals(id2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.Equals(object)"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestEqualsWithNullIdAsObject()
        {
            DocumentID? id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            object? id2 = null;

            Assert.That(id1.Equals(id2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.GetHashCode()"/> method works correctly with different document IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForDifferentIds()
        {
            var id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AB");

            Assert.That(id1.GetHashCode(), Is.Not.EqualTo(id2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="DocumentID.GetHashCode()"/> method works correctly with matching document IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForMatchingIds()
        {
            var id1 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new DocumentID("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1.GetHashCode(), Is.EqualTo(id2.GetHashCode()));
        }
    }
}
