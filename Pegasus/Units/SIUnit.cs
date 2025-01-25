using Pegasus.Symbols;

namespace Pegasus.Units
{
    /// <summary>
    /// Represents one of the units of measurement defined by the National Institue of Standards and Technology (NIST).
    /// </summary>
    public class SIUnit : Unit
    {
        private readonly double multiplier; // The multiplier that applies to the unit as a result of it's prefix.

        private readonly ISymbol prefix; // The symbol that represents the prefix.

        /// <summary>
        /// Initialises a new instance of the <see cref="SIUnit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The <see cref="ISymbol"/> that represents the unit.</param>
        /// <param name="prefix">An <see cref="SIUnitPrefixes"/> that specifies the prefix.</param>
        /// <param name="formatter">The <see cref="IFormatter"/> that will be used to generate the string representation of the unit.</param>
        public SIUnit(string name, ISymbol symbol, SIUnitPrefixes prefix, IFormatter formatter)
            : base(name, symbol, formatter)
        {
            multiplier = Math.Pow(10, (int)prefix);
            this.prefix = GetSymbol(prefix);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SIUnit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The <see cref="ISymbol"/> that represents the unit.</param>
        /// <param name="prefix">An <see cref="SIUnitPrefixes"/> that specifies the prefix.</param>
        public SIUnit(string name, ISymbol symbol, SIUnitPrefixes prefix)
            : this(name, symbol, prefix, new HtmlFormatter()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="SIUnit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The symbol that represents the unit.</param>
        /// <param name="prefix">An <see cref="SIUnitPrefixes"/> that specifies the prefix.</param>
        public SIUnit(string name, string symbol, SIUnitPrefixes prefix)
            : this(name, new Symbol(symbol), prefix) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="SIUnit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The <see cref="ISymbol"/> that represents the unit.</param>
        /// <param name="formatter">The <see cref="IFormatter"/> that will be used to generate the string representation of the unit.</param>
        public SIUnit(string name, ISymbol symbol, IFormatter formatter)
            : base(name, symbol, formatter)
        {
            prefix = new EmptySymbol();
            multiplier = 1;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SIUnit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The <see cref="ISymbol"/> that represents the unit.</param>
        public SIUnit(string name, ISymbol symbol)
            : this(name, symbol, new HtmlFormatter()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="SIUnit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The symbol that represents the unit.</param>
        public SIUnit(string name, string symbol)
            : this(name, new Symbol(symbol)) { }

        /// <summary>
        /// Gets the multiplier.
        /// </summary>
        public double Multiplier => multiplier;

        /// <summary>
        /// Converts the value of the current <see cref="SIUnit"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="SIUnit"/> object.</returns>
        public override string ToString()
        {
            return prefix.ToString() + base.ToString();
        }

        /// <summary>
        /// Gets the <see cref="ISymbol"/> that represents the prefix.
        /// </summary>
        /// <param name="prefix">An <see cref="SIUnitPrefixes"> that specifies the prefix.</param>
        /// <returns>An <see cref="ISymbol"/> that represents the prefix.</returns>
        private ISymbol GetSymbol(SIUnitPrefixes prefix)
        {
            var symbol = string.Empty;

            switch (prefix)
            {
                case SIUnitPrefixes.Yotta:
                    symbol = "Y";
                    break;

                case SIUnitPrefixes.Zetta:
                    symbol = "Z";
                    break;

                case SIUnitPrefixes.Exa:
                    symbol = "E";
                    break;

                case SIUnitPrefixes.Peta:
                    symbol = "P";
                    break;

                case SIUnitPrefixes.Tera:
                    symbol = "T";
                    break;

                case SIUnitPrefixes.Giga:
                    symbol = "G";
                    break;

                case SIUnitPrefixes.Mega:
                    symbol = "M";
                    break;

                case SIUnitPrefixes.Kilo:
                    symbol = "k";
                    break;

                case SIUnitPrefixes.Hecto:
                    symbol = "h";
                    break;

                case SIUnitPrefixes.Deca:
                    symbol = "da";
                    break;

                case SIUnitPrefixes.Deci:
                    symbol = "d";
                    break;

                case SIUnitPrefixes.Centi:
                    symbol = "c";
                    break;

                case SIUnitPrefixes.Milli:
                    symbol = "m";
                    break;

                case SIUnitPrefixes.Micro:
                    symbol = "mu";
                    break;

                case SIUnitPrefixes.Nano:
                    symbol = "n";
                    break;

                case SIUnitPrefixes.Pico:
                    symbol = "p";
                    break;

                case SIUnitPrefixes.Femto:
                    symbol = "f";
                    break;

                case SIUnitPrefixes.Atto:
                    symbol = "a";
                    break;

                case SIUnitPrefixes.Zepto:
                    symbol = "z";
                    break;

                case SIUnitPrefixes.Yocto:
                    symbol = "y";
                    break;
            }

            return string.IsNullOrEmpty(symbol) ? new EmptySymbol() : new Symbol(symbol);
        }
    }
}
