using System.Text;

namespace Pegasus.Symbols
{
    /// <summary>
    /// Represents the product of two or more mathematical or scientific symbols.
    /// </summary>
    public class SymbolProduct : ISymbol
    {
        private readonly IList<ISymbol> symbols; // A list containing the symbols that comprise this symbol product.

        private readonly ISymbol separator; // The symbol used to separate the symbols in the string representation of this symbol product.

        /// <summary>
        /// Initialises a new instance of the <see cref="SymbolProduct"/> class.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise this symbol product.</param>
        /// <param name="separator">The symbol to be used as a separator in the string representation of this symbol product.</param>
        public SymbolProduct(IEnumerable<ISymbol> symbols, ISymbol separator)
        {
            this.symbols = new List<ISymbol>(symbols);
            this.separator = separator;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SymbolProduct"/> class.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise this symbol product.</param>
        /// <param name="separator">The symbol to be used as a separator in the string representation of this symbol product.</param>
        public SymbolProduct(IEnumerable<ISymbol> symbols, string separator)
            : this(symbols, new Symbol(separator)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="SymbolProduct"/> class.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise this symbol product.</param>
        public SymbolProduct(IEnumerable<ISymbol> symbols)
            : this(symbols, " ") { }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="ISymbol"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="ISymbol"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(ISymbol? other)
        {
            var result = !ReferenceEquals(other, null);

            if (result && other is SymbolProduct product)
            {
                result &= product.symbols.Count == symbols.Count;

                if (result)
                {
                    for (int i = 0; i < symbols.Count; i++)
                    {
                        result &= product.symbols[i].Equals(symbols[i]);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Converts the value of the current <see cref="SymbolProduct"/> object to its equivalent string representation using the specified formatter.
        /// </summary>
        /// <param name="formatter">An <see cref="IFormatter"/> object that generates the string representation.</param>
        /// <returns>A string representation of the current <see cref="SymbolProduct"/> object as specified by the formatter.</returns>
        public string ToString(IFormatter formatter)
        {
            var buffer = new StringBuilder();
            var first = true;

            foreach (var symbol in symbols)
            {
                if (!first) buffer.Append(separator);
                buffer.Append(symbol.ToString(formatter));
                first = false;
            }

            return buffer.ToString();
        }

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>true if obj is a <see cref="SymbolProduct"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is SymbolProduct && Equals((SymbolProduct)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = 23;

                for (int i = 0; i < symbols.Count; i++)
                {
                    hashcode = (hashcode * 3) + (symbols[i] != null ? symbols[i].GetHashCode() : 0);
                }

                return hashcode;
            }
        }

        /// <summary>
        /// Converts the value of the current <see cref="SymbolProduct"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="SymbolProduct"/> object.</returns>
        public override string ToString()
        {
            return ToString(new HtmlFormatter());
        }

        /// <summary>
        /// Gets the number of symbols in this <see cref="SymbolProduct"/>
        /// </summary>
        public int Count
        {
            get { return symbols.Count; }
        }
    }
}
