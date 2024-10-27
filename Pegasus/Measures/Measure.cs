using Pegasus.Properties;
using Pegasus.Units;

namespace Pegasus.Measures
{
    public partial struct Measure : IComparable, IComparable<Measure>, IEquatable<Measure>
    {
        private readonly double value;

        private readonly Unit units;

        public Measure(double value, Unit units)
        {
            this.value = value;
            this.units = units;
        }

        public double Value => value;

        public Unit Unit => units;

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
        /// 
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public int CompareTo(object? other)
        {
            if (other == null) return 1;

            if (other is Measure) return CompareTo((Measure)other);

            throw new ArgumentException("other", Resources.ExceptionCompareMeasures);
        }

        public bool Equals(Measure other)
        {
            return other.value == value && other.units == units;
        }

        public override bool Equals(object? other)
        {
            if (other is null) return false;

            if (other is Measure measure) return Equals(measure);

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(value, units);
        }

        public override string ToString()
        {
            return value.ToString() + " " + units.ToString();
        }

        private double GetDoubleValue()
        {
            var retval = value;

            if (units is SIUnit siUnit)
            {
                retval *= siUnit.Multiplier;
            }

            return retval;
        }
    }
}
