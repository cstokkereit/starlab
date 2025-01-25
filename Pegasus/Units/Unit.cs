using Pegasus.Symbols;

namespace Pegasus.Units
{
    /// <summary>
    /// Represents a unit of measurement.
    /// </summary>
    public class Unit
    {
        private readonly IFormatter formatter; // The formatter that will be used to generate the string representation of the unit.

        private readonly ISymbol symbol; // The symbol that represents the unit.

        private readonly string name; // The name of the unit.

        /// <summary>
        /// Initialises a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The <see cref="ISymbol"/> that represents the unit.</param>
        /// <param name="formatter">The <see cref="IFormatter"/> that will be used to generate the string representation of the unit.</param>
        public Unit(string name, ISymbol symbol, IFormatter formatter)
        {
            this.formatter = formatter;
            this.symbol = symbol;
            this.name = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The symbol that represents the unit.</param>
        /// <param name="formatter">The <see cref="IFormatter"/> that will be used to generate the string representation of the unit.</param>
        public Unit(string name, string symbol, IFormatter formatter)
            : this(name, new Symbol(symbol), formatter) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The <see cref="ISymbol"/> that represents the unit.</param>
        public Unit(string name, ISymbol symbol)
            : this(name, symbol, new HtmlFormatter()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Unit"/> class.
        /// </summary>
        /// <param name="name">The name of the unit.</param>
        /// <param name="symbol">The symbol that represents the unit.</param>
        public Unit(string name, string symbol)
            : this(name, new Symbol(symbol)) { }

        /// <summary>
        /// Converts the value of the current <see cref="Unit"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Unit"/> object.</returns>
        public override string ToString()
        {
            return string.Empty + symbol.ToString(formatter);
        }

        /// <summary>
        /// Gets the <see cref="IFormatter"/> used to generate the string representation of the unit.
        /// </summary>
        protected IFormatter Formatter => formatter;
    }
}
