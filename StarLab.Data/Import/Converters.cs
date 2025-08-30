using StarLab.Application.Data.Import;

namespace StarLab.Data.Import
{
    /// <summary>
    /// A static factory that creates instances of <see cref="IConverter"/> that can be used to convert a <see cref="string"/> value to a specified data type.
    /// </summary>
    internal static class Converters
    {
        private static Dictionary<DataTypes, IConverter> converters = new Dictionary<DataTypes, IConverter>(); // A dictionary containing the available converters indexed by the output DataType.

        /// <summary>
        /// A static constructor that loads the available converters into a <see cref="Dictionary{DataTypes, IConverter}"/>
        /// </summary>
        static Converters()
        {
            converters.Add(DataTypes.Decimal, new DecimalConverter());
            converters.Add(DataTypes.Integer, new IntegerConverter());
            converters.Add(DataTypes.Text, new TextConverter());
        }

        /// <summary>
        /// Gets an <see cref="IConverter"/> that can be used to convert a string value to the specified data type.
        /// </summary>
        /// <param name="type">A <see cref="DataTypes"/> value that specifies the required conversion.</param>
        /// <returns>An <see cref="IConverter"/> that can be used to convert a string value to the specified data type.</returns>
        public static IConverter GetConverter(DataTypes type)
        {
            return converters[type];
        }
    }

    /// <summary>
    /// A converter that converts the <see cref="string"/> value provided to its double-precision floating-point equivalent.
    /// </summary>
    internal class DecimalConverter : IConverter
    {
        /// <summary>
        /// Converts the <see cref="string"/> value provided to its double-precision floating-point equivalent.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to be converted.</param>
        /// <returns>An <see cref="object"/> that holds the double-precision floating-point value that is equivalent to the numeric value provided.</returns>
        public object Convert(string value)
        {
            return double.Parse(value);
        }
    }

    /// <summary>
    /// A converter that converts the <see cref="string"/> value provided to its 64-bit signed integer equivalent.
    /// </summary>
    internal class IntegerConverter : IConverter
    {
        /// <summary>
        /// Converts the <see cref="string"/> value provided to its 64-bit signed integer equivalent.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to be converted.</param>
        /// <returns>An <see cref="object"/> that holds the 64-bit signed integer value that is equivalent to the numeric value provided.</returns>
        public object Convert(string value)
        {
            return long.Parse(value);
        }
    }

    /// <summary>
    /// A default converter that simply returns the <see cref="string"/> value provided.
    /// </summary>
    internal class TextConverter : IConverter
    {
        /// <summary>
        /// This is a pass through function that simply returns the <see cref="string"/> value provided.
        /// </summary>
        /// <param name="value">The <see cref="string"/> value to be converted.</param>
        /// <returns>An <see cref="object"/> that holds the <see cref="string"/> value provided.</returns>
        public object Convert(string value)
        {
            return value;
        }
    }
}
