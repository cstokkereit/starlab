using System.Text;

namespace Pegasus.Symbols
{
    /// <summary>
    /// Represents the quotient of two mathematical or scientific symbols or products thereof.
    /// </summary>
    public class SymbolQuotient : ISymbol
    {
        private readonly ISymbol denominator; // The denominator of this symbol quotient.

        private readonly ISymbol numerator; // The numerator of this symbol quotient.

        #region Constructors

        /// <summary>
        /// Initialises a new instance of the <see cref="SymbolQuotient"/> class.
        /// </summary>
        /// <param name="numerator">The symbol to be used as the numerator of this symbol quotient.</param>
        /// <param name="denominator">The symbol to be used as the denominator of this symbol quotient.</param>
        /// <exception cref="ArgumentNullException">TODO</exception>
        public SymbolQuotient(ISymbol numerator, ISymbol denominator)
        {
            this.denominator = denominator ?? throw new ArgumentNullException("denominator");
            this.numerator = numerator ?? throw new ArgumentNullException("numerator");
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="SymbolQuotient"/> class.
        /// </summary>
        /// <param name="numerator">A collection containing the symbols that comprise the numerator of this symbol quotient.</param>
        /// <param name="denominator">A collection containing the symbols that comprise the denominator of this symbol quotient.</param>
        /// <param name="separator">The symbol to be used as a separator in the string representations of the numerator and denominator of this symbol quotient.</param>
        public SymbolQuotient(IEnumerable<ISymbol> numerator, IEnumerable<ISymbol> denominator, ISymbol separator)
            : this(new SymbolProduct(numerator, separator), new SymbolProduct(denominator, separator)) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="SymbolQuotient"/> class.
        /// </summary>
        /// <param name="numerator">A collection containing the symbols that comprise the numerator of this symbol quotient.</param>
        /// <param name="denominator">A collection containing the symbols that comprise the denominator of this symbol quotient.</param>
        public SymbolQuotient(IEnumerable<ISymbol> numerator, IEnumerable<ISymbol> denominator)
            : this(new SymbolProduct(numerator), new SymbolProduct(denominator)) { }

        #endregion

        #region ISymbol Members

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="ISymbol"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="ISymbol"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(ISymbol? other)
        {
            var result = !ReferenceEquals(other, null);

            if (result && other is SymbolQuotient quotient)
            {
                result &= quotient.denominator.Equals(denominator);
                result &= quotient.numerator.Equals(numerator);
            }

            return result;
        }

        /// <summary>
        /// Converts the value of the current <see cref="SymbolQuotient"/> object to its equivalent string representation using the specified formatter.
        /// </summary>
        /// <param name="formatter">An <see cref="IFormatter"/> object that generates the string representation.</param>
        /// <returns>A string representation of the current <see cref="SymbolQuotient"/> object as specified by the formatter.</returns>
        public string ToString(IFormatter formatter)
        {
            var buffer = new StringBuilder();

            buffer.Append(FormatSymbol(numerator, formatter));
            buffer.Append("/");
            buffer.Append(FormatSymbol(denominator, formatter));

            return buffer.ToString();
        }

        #endregion

        #region Object Overrides

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>true if obj is a <see cref="SymbolQuotient"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is SymbolQuotient && Equals((SymbolQuotient)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = 31;
                hashcode = (hashcode * 5) + (denominator != null ? denominator.GetHashCode() : 0);
                hashcode = (hashcode * 5) + (numerator != null ? numerator.GetHashCode() : 0);
                return hashcode;
            }
        }

        /// <summary>
        /// Converts the value of the current <see cref="SymbolQuotient"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="SymbolQuotient"/> object.</returns>
        public override string ToString()
        {
            return ToString(new HtmlFormatter());
        }

        #endregion

        #region Private Members

        /// <summary>
        /// Generates a string representation of the specified symbol using the formatter provided.
        /// </summary>
        /// <param name="symbol">The symbol to be formatted.</param>
        /// <param name="formatter">An <see cref="IFormatter"/> object used to generate the string representation of a symbol.</param>
        /// <returns>A string representation of the specified <see cref="ISymbol"/> object.</returns>
        private string FormatSymbol(ISymbol symbol, IFormatter formatter)
        {
            var text = symbol.ToString(formatter);

            if (symbol is SymbolProduct product && product.Count > 1)
            {
                text = "(" + text + ")";
            }

            return text;
        }

        #endregion
    }
}
