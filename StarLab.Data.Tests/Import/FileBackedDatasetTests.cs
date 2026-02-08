using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="FileBackedDataset"/> class.
    /// </summary>
    public class FileBackedDatasetTests
    {
        private readonly string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Data.csv");

        /// <summary>
        /// Test that the <see cref="FileBackedDataset(string, IImportDefinition)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            // Arrange
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            // Act
            var dataset = new FileBackedDataset(filename, importDefinition);

            // Assert
            Assert.That(dataset, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.BOF"/> property is true when a file is first opened.
        /// </summary>
        [Test]
        public void TestBOFAtBeginningOfFile()
        {
            // Arrange
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            // Act
            var dataset = new FileBackedDataset(filename, importDefinition);

            // Assert
            Assert.That(dataset.BOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.BOF"/> property is false when the pointer is positioned after the beginning of the file.
        /// </summary>
        [Test]
        public void TestBOFAfterBeginningOfFile()
        {
            // Arrange
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(filename, importDefinition);

            // Act
            dataset.MoveFirst();

            // Assert
            Assert.That(dataset.BOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.BOF"/> property is false when the pointer is positioned at the end of the file.
        /// </summary>
        [Test]
        public void TestBOFAtEndOfFile()
        {
            // Arrange
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(filename, importDefinition);

            // Act
            dataset.MoveLast();
            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.EOF"/> property is false when a file is frst opened.
        /// </summary>
        [Test]
        public void TestEOFAtBeginningOfFile()
        {
            // Arrange
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(filename, importDefinition);

            // Assert
            Assert.That(dataset.EOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.EOF"/> property is false when the pointer is positioned before the end of the file.
        /// </summary>
        [Test]
        public void TestEOFBeforeEndOfFile()
        {
            // Arrange
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(filename, importDefinition);

            // Act
            dataset.MoveLast();

            // Assert
            Assert.That(dataset.EOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.EOF"/> property is true when the pointer is positioned at the end of the file.
        /// </summary>
        [Test]
        public void TestEOFAtEndOfFile()
        {
            // Arrange
            var importDefinition = ImportDefinitionBuilder.GetInstance(",").Build();

            var dataset = new FileBackedDataset(filename, importDefinition);

            // Act
            dataset.MoveLast();
            dataset.MoveNext();

            // Assert
            Assert.That(dataset.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.Fields"/> property TODO.
        /// </summary>
        [Test]
        public void TestGetFields()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue(IDataField)"/> function TODO.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedField()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue(int)"/> function TODO.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedIndex()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.GetValue(string)"/> function TODO.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedName()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.Move(int)"/> method TODO.
        /// </summary>
        [Test]
        public void TestMove()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.MoveFirst()"/> method TODO.
        /// </summary>
        [Test]
        public void TestMoveFirst()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.MoveLast()"/> method TODO.
        /// </summary>
        [Test]
        public void TestMoveLast()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.MoveNext()"/> method TODO.
        /// </summary>
        [Test]
        public void TestMoveNext()
        {
            Assert.Fail();
        }

        /// <summary>
        /// Test that the <see cref="FileBackedDataset.MovePrevious()"/> method TODO.
        /// </summary>
        [Test]
        public void TestMovePrevious()
        {
            Assert.Fail();
        }
    }
}
