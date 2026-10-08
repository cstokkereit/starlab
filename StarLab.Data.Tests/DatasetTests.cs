#pragma warning disable CS8604 // Possible null reference argument for parameter

using StarLab.Application.Data;
using StarLab.Application.Data.Import;
using StarLab.Data.Import;

namespace StarLab.Data
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="Dataset"/> class.
    /// </summary>
    public class DatasetTests
    {
        private static IImportDefinition importDefinition;

        private readonly string filename = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "DataWithoutHeaders.csv");

        private FileBackedDataset? source;

        [OneTimeSetUp]
        public static void Initialise()
        {
            importDefinition = ImportDefinitionBuilder.GetInstance(",")
                .AddField(0, "Name", DataTypes.Text)
                .AddField(1, "SpectralClass", DataTypes.Text)
                .AddField(2, "AbsoluteMagnitude", DataTypes.Decimal)
                .AddField(3, "ColourIndex", DataTypes.Decimal)
                .Build();
        }

        [SetUp]
        public void SetUp()
        {
            source = new FileBackedDataset(filename, importDefinition);
        }

        [TearDown]
        public void TearDown()
        {
            source?.Dispose();
        }

        /// <summary>
        /// Test that the <see cref="Dataset(IForwardOnlyDataset)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var dataset = new Dataset(source);

            Assert.That(dataset, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="Dataset.BOF"/> property is true initially.
        /// </summary>
        [Test]
        public void TestBOFAtBeginningOfFile()
        {
            var dataset = new Dataset(source);

            Assert.That(dataset.BOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Dataset.BOF"/> property is false when the pointer is positioned after the beginning of the file.
        /// </summary>
        [Test]
        public void TestBOFAfterBeginningOfFile()
        {
            var dataset = new Dataset(source);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Dataset.EOF"/> property is false initially.
        /// </summary>
        [Test]
        public void TestEOFAtBeginningOfFile()
        {
            var dataset = new Dataset(source);

            Assert.That(dataset.EOF, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="Dataset.EOF"/> property is true when the pointer is positioned at the end of the file.
        /// </summary>
        [Test]
        public void TestEOFAtEndOfFile()
        {
            var dataset = new Dataset(source);

            for (int i = 0; i < 18; i++)
            {
                dataset.MoveNext();
            }

            Assert.That(dataset.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Dataset.Fields"/> property returns the correct fields.
        /// </summary>
        [Test]
        public void TestGetFields()
        {
            var dataset = new Dataset(source);

            Assert.That(dataset.Fields, Is.Not.Null);

            var fields = dataset.Fields.ToList();

            Assert.That(fields.Count, Is.EqualTo(4));

            Assert.That(fields[0].Index, Is.EqualTo(0));
            Assert.That(fields[0].Name, Is.EqualTo("Name"));
            Assert.That(fields[0].DataType, Is.EqualTo(DataTypes.Text));
            Assert.That(fields[0].Width, Is.EqualTo(-1));

            Assert.That(fields[1].Index, Is.EqualTo(1));
            Assert.That(fields[1].Name, Is.EqualTo("SpectralClass"));
            Assert.That(fields[1].DataType, Is.EqualTo(DataTypes.Text));
            Assert.That(fields[1].Width, Is.EqualTo(-1));

            Assert.That(fields[2].Index, Is.EqualTo(2));
            Assert.That(fields[2].Name, Is.EqualTo("AbsoluteMagnitude"));
            Assert.That(fields[2].DataType, Is.EqualTo(DataTypes.Decimal));
            Assert.That(fields[2].Width, Is.EqualTo(-1));

            Assert.That(fields[3].Index, Is.EqualTo(3));
            Assert.That(fields[3].Name, Is.EqualTo("ColourIndex"));
            Assert.That(fields[3].DataType, Is.EqualTo(DataTypes.Decimal));
            Assert.That(fields[3].Width, Is.EqualTo(-1));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.Rows"/> property returns the correct value.
        /// </summary>
        [Test]
        public void TestGetRows()
        {
            var dataset = new Dataset(source);

            Assert.That(dataset.Rows, Is.EqualTo(17));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(IDataField)"/> function returns the correct value for the specified compound field.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedCompoundField()
        {
            Assert.Fail();




            var dataset = new Dataset(source);

            var fields = dataset.Fields.ToList();

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>(fields[0]), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(fields[1]), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(fields[2]), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(fields[3]), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(IDataField)"/> function returns the correct value for the specified field.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedField()
        {
            var dataset = new Dataset(source);

            var fields = dataset.Fields.ToList();

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>(fields[0]), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(fields[1]), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(fields[2]), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(fields[3]), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(IDataField)"/> function throws an exception when BOF is already true.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedFieldThrowsExceptionWhenBOFTrue()
        {
            var dataset = new Dataset(source);

            var fields = dataset.Fields.ToList();

            Assert.Throws<InvalidOperationException>(() => dataset.GetValue<string>(fields[0]));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(IDataField)"/> function throws an exception when EOF is already true.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedFieldThrowsExceptionWhenEOFTrue()
        {
            var dataset = new Dataset(source);

            var fields = dataset.Fields.ToList();

            dataset.MoveLast();
            dataset.MoveNext();

            Assert.Throws<InvalidOperationException>(() => dataset.GetValue<string>(fields[0]));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(int)"/> function returns the correct value for the field with the specified index.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedIndex()
        {
            var dataset = new Dataset(source);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(int)"/> function throws an exception when BOF is already true.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedIndexThrowsExceptionWhenBOFTrue()
        {
            var dataset = new Dataset(source);

            Assert.Throws<InvalidOperationException>(() => dataset.GetValue<string>(0));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(int)"/> function throws an exception when EOF is already true.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedIndexThrowsExceptionWhenEOFTrue()
        {
            var dataset = new Dataset(source);

            dataset.MoveLast();
            dataset.MoveNext();

            Assert.Throws<InvalidOperationException>(() => dataset.GetValue<string>(0));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue(string)"/> function returns the correct value for the field with the specified name.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedName()
        {
            var dataset = new Dataset(source);

            dataset.MoveNext();

            Assert.That(dataset.GetValue<string>("Name"), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>("SpectralClass"), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>("AbsoluteMagnitude"), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>("ColourIndex"), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(string)"/> function throws an exception when BOF is already true.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedNameThrowsExceptionWhenBOFTrue()
        {
            var dataset = new Dataset(source);

            Assert.Throws<InvalidOperationException>(() => dataset.GetValue<string>("Name"));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.GetValue{T}(string)"/> function throws an exception when EOF is already true.
        /// </summary>
        [Test]
        public void TestGetValueForSpecifiedNameThrowsExceptionWhenEOFTrue()
        {
            var dataset = new Dataset(source);

            dataset.MoveLast();
            dataset.MoveNext();

            Assert.Throws<InvalidOperationException>(() => dataset.GetValue<string>("Name"));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.Move(int)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestMove()
        {
            var dataset = new Dataset(source);

            dataset.Move(6);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Betelgeuse"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("M2I"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-5.6));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(1.85));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.Move(int)"/> method throws an exception if index less than one.
        /// </summary>
        [Test]
        public void TestMoveNextThrowsExceptionWhenIndexLessThanOne()
        {
            var dataset = new Dataset(source);

            Assert.Throws<ArgumentOutOfRangeException>(() => dataset.Move(0));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.Move(int)"/> method throws an exception if index greater than the number of rows.
        /// </summary>
        [Test]
        public void TestMoveNextThrowsExceptionWhenIndexGreaterThanRows()
        {
            var dataset = new Dataset(source);

            Assert.Throws<ArgumentOutOfRangeException>(() => dataset.Move(18));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.MoveNext()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestMoveFirst()
        {
            var dataset = new Dataset(source);

            dataset.MoveFirst();

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.MoveLast()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestMoveLast()
        {
            var dataset = new Dataset(source);

            dataset.MoveLast();

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Vega"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("A0V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(0.5));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(0.00));
        }

        /// <summary>
        /// Test that the <see cref="Dataset.MoveNext()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestMoveNext()
        {
            var dataset = new Dataset(source);

            dataset.MoveNext();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));

            for (int i = 1; i < 17; i++)
            {
                dataset.MoveNext();
            }

            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Vega"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("A0V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(0.5));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(0.00));

            dataset.MoveNext();

            Assert.That(dataset.EOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Dataset.MoveNext()"/> method throws an exception if EOF is already true.
        /// </summary>
        [Test]
        public void TestMoveNextThrowsExceptionWhenEOFTrue()
        {
            var dataset = new Dataset(source);

            dataset.MoveLast();
            dataset.MoveNext();

            Assert.Throws<InvalidOperationException>(() => dataset.MoveNext());
        }

        /// <summary>
        /// Test that the <see cref="Dataset.MovePrevious()"/> method works correctly.
        /// </summary>
        [Test]
        public void TestMovePrevious()
        {
            var dataset = new Dataset(source);

            dataset.MoveLast();

            Assert.That(dataset.BOF, Is.False);
            Assert.That(dataset.EOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Vega"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("A0V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(0.5));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(0.00));

            for (int i = 1; i < 17; i++)
            {
                dataset.MovePrevious();
            }

            Assert.That(dataset.BOF, Is.False);

            Assert.That(dataset.GetValue<string>(0), Is.EqualTo("Achernar"));
            Assert.That(dataset.GetValue<string>(1), Is.EqualTo("B3V"));
            Assert.That(dataset.GetValue<double>(2), Is.EqualTo(-2.3));
            Assert.That(dataset.GetValue<double>(3), Is.EqualTo(-0.15));

            dataset.MovePrevious();

            Assert.That(dataset.BOF, Is.True);
        }

        /// <summary>
        /// Test that the <see cref="Dataset.MovePrevious()"/> method throws an exception if BOF is already true.
        /// </summary>
        [Test]
        public void TestMovePreviousThrowsExceptionWhenBOFTrue()
        {
            var dataset = new Dataset(source);

            Assert.Throws<InvalidOperationException>(() => dataset.MovePrevious());
        }

        // TODO : Add GetField(string) to the interface, create tests and implement functionality
    }
}
