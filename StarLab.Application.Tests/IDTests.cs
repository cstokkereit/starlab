namespace StarLab.Application
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ID{T}"/> class.
    /// </summary>
    public class IDTests
    {
        /// <summary>
        /// Test that the <see cref="ID{T}()"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstruction()
        {
            var id = new ID<ITestType1>();

            Assert.That(id, Is.Not.Null);

            Assert.That(!string.IsNullOrEmpty(id.ToString()));
            Assert.That(Guid.TryParse(id.ToString(), out Guid _));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}(string)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructionFromString()
        {
            var id = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id, Is.Not.Null);

            Assert.That(id.ToString(), Is.EqualTo("5542258B-B3B0-4A61-84FF-916F8EFE38AA"));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.Equals(ID{T})"/> method works correctly with different IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIds()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AB");

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.Equals(object)"/> method works correctly with different IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentIdsAsObject()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            object id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AB");

            Assert.That(id1, Is.Not.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.Equals(ID{T})"/> method works correctly with different types.
        /// </summary>
        [Test]
        public void TestEqualsWithDifferentTypes()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType2>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1.Equals(id2), Is.False);
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.Equals(ID{T})"/> method works correctly with matching IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIds()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.Equals(object)"/> method works correctly with matching IDs.
        /// </summary>
        [Test]
        public void TestEqualsWithMatchingIdsAsObject()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            object id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1, Is.EqualTo(id2));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.Equals(ID{T})"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestEqualsWithNullId()
        {
            ID<ITestType1>? id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            ID<ITestType1>? id2 = null;

            Assert.False(id1.Equals(id2));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.GetHashCode()"/> method works correctly with different IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForDifferentIds()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AB");

            Assert.That(id1.GetHashCode(), Is.Not.EqualTo(id2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.GetHashCode()"/> method works correctly with different types.
        /// </summary>
        [Test]
        public void TestGetHashCodeForDifferentTypes()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType2>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1.GetHashCode(), Is.Not.EqualTo(id2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.GetHashCode()"/> method works correctly with matching IDs.
        /// </summary>
        [Test]
        public void TestGetHashCodeForMatchingIds()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.That(id1.GetHashCode(), Is.EqualTo(id2.GetHashCode()));
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.operator ==(ID{T}, ID{T})"/> method works correctly with matching IDs.
        /// </summary>
        [Test]
        public void TestOperatorEqualsWithMatchingIds()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");

            Assert.True(id1 == id2);
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.operator ==(ID{T}, ID{T})"/> method works correctly with null IDs.
        /// </summary>
        [Test]
        public void TestOperatorEqualsWithNullIds()
        {
            ID<ITestType1>? id1 = null;
            ID<ITestType1>? id2 = null;

            Assert.True(id1 == id2);
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.operator ==(ID{T}, ID{T})"/> method works correctly with differnet IDs.
        /// </summary>
        [Test]
        public void TestOperatorNotEqualsWithDifferentIds()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            var id2 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AB");

            Assert.True(id1 != id2);
        }

        /// <summary>
        /// Test that the <see cref="ID{T}.operator ==(ID{T}, ID{T})"/> method works correctly with a null ID.
        /// </summary>
        [Test]
        public void TestOperatorNotEqualsWithNullId()
        {
            var id1 = new ID<ITestType1>("5542258B-B3B0-4A61-84FF-916F8EFE38AA");
            ID<ITestType1>? id2 = null;

            Assert.True(id1 != id2);
            Assert.True(id2 != id1);
        }

        /// <summary>
        /// A simple interface for testing the <see cref="ID{T}"/> class.
        /// </summary>
        private interface ITestType1 { }

        /// <summary>
        /// A simple interface for testing the <see cref="ID{T}"/> class.
        /// </summary>
        private interface ITestType2 { }
    }
}
