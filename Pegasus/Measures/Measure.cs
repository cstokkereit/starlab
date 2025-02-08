using Pegasus.Properties;
using Pegasus.Units;

namespace Pegasus.Measures
{
    /// <summary>
    /// Represents a value with a specified unit of measurement.
    /// </summary>
    public partial struct Measure : IComparable, IComparable<Measure>, IEquatable<Measure>
    {
        private readonly double value; // The measure value.

        private readonly Unit units; // The unit of measurement.

        /// <summary>
        /// Initialises a new instance of the <see cref="Measure"/> class.
        /// </summary>
        /// <param name="value">The measure value.</param>
        /// <param name="units">The <see cref="Unit"/> of measurement.</param>
        public Measure(double value, Unit units)
        {
            this.value = value;
            this.units = units;
        }

        /// <summary>
        /// Gets measure value.
        /// </summary>
        public double Value => value;

        /// <summary>
        /// Gets the unit of measurement.
        /// </summary>
        public Unit Unit => units;

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates the relative order of the objects being compared.
        /// </summary>
        /// <param name="other">The <see cref="Measure"/> to compare to this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        public int CompareTo(Measure other)
        {
            var otherValue = other.GetDoubleValue();
            var value = GetDoubleValue();

            if (value > otherValue) return 1;

            if (value == otherValue) return 0;

            if (value < otherValue) return -1;

            return 1;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates the relative order of the objects being compared.
        /// </summary>
        /// <param name="other">The object to compare to this instance.</param>
        /// <returns>A value that indicates the relative order of the objects being compared.</returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(object? other)
        {
            if (other == null) return 1;

            if (other is Measure) return CompareTo((Measure)other);

            throw new ArgumentException("other", Resources.ExceptionCompareMeasures);
        }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be a <see cref="Measure"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="Measure"/> to compare to this instance.</param>
<<<<<<< HEAD
        /// <returns><see cref="true"/> if other has the same value as this instance; <see cref="false"/> otherwise.</returns>
=======
>>>>>>> 8fe7ec8dc993dae512270f500395f9cd3baaeeb7
        public bool Equals(Measure other)
        {
            return other.value == value && other.units == units;
        }

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="other">The object to compare to this instance.</param>
        /// <returns><see cref="true"/> if obj is a <see cref="Measure"/> and its value is the same as this instance; <see cref="false"/> otherwise.</returns>
        public override bool Equals(object? other)
        {
            return other is Measure && Equals((Measure)other);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(value, units);
        }

        /// <summary>
        /// Converts the value of the current <see cref="Measure"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Measure"/> object.</returns>
        public override string ToString()
        {
            return value.ToString() + " " + units.ToString();
        }

        /// <summary>
        /// Gets the value of the measure multiplied by the factor specified by the SI unit prefix if applicable.
        /// Allows measures with the same SI unit but different SI unit prefixes to compared.
        /// </summary>
        /// <returns>The value of the measure.</returns>
        private double GetDoubleValue()
        {
            var retval = value;

            if (units is SIUnit siUnit) retval *= siUnit.Multiplier;

            return retval;
        }
    }
}
