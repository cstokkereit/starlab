namespace Pegasus.Symbols
{
    /// <summary>
    /// Represents a mathematical or scientific symbol.
    /// </summary>
    public class Symbol : ISymbol
    {
        private static readonly NamedCharacters characters = new NamedCharacters(); // A dictionary containing unicode characters indexed by name.

        private static readonly ISymbol empty = new EmptySymbol(); // The empty symbol.

        private readonly ISymbol superscript; // The superscript symbol. Defaults to the empty symbol.

        private readonly ISymbol subscript; // The subscript symbol. Defaults to the empty symbol.

        private readonly ISymbol prefix; // The prefix symbol. Defaults to the empty symbol.

        private readonly string text; // The symbol value. This cannot be a null or empty string.

        private readonly bool italic; // A flag that specifies the use of italic text in the string representation of this symbol.

        private readonly bool bold; // A flag that specifies the use of bold text in the string representation of this symbol.

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="prefix">The symbol to use as the prefix for this symbol.</param>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        /// <param name="subscript">The symbol to use as the subscript for this symbol.</param>
        /// <param name="superscript">The <see cref="ISymbol"/> to use as the superscript for this symbol.</param>
        /// <exception cref="ArgumentException"></exception>
        public Symbol(ISymbol? prefix, string symbol, bool bold, bool italic, ISymbol? subscript, ISymbol? superscript)
        {
            if (string.IsNullOrEmpty(symbol)) throw new ArgumentException("symbol");

            text = GetText(symbol);

            this.superscript = superscript ?? new EmptySymbol();
            this.subscript = subscript ?? new EmptySymbol();
            this.prefix = prefix ?? new EmptySymbol();
            this.italic = italic;
            this.bold = bold;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        /// <param name="subscript">The text to use as the subscript for this symbol.</param>
        /// <param name="superscript">The text to use as the superscript for this symbol.</param>
        /// <exception cref="ArgumentException"></exception>
        public Symbol(string prefix, string symbol, bool bold, bool italic, string subscript, string superscript)
        {
            if (string.IsNullOrEmpty(symbol)) throw new ArgumentException("symbol");

            text = GetText(symbol);

            this.superscript = GetSymbol(superscript);
            this.subscript = GetSymbol(subscript);
            this.prefix = GetSymbol(prefix);
            this.italic = italic;
            this.bold = bold;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for this symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for this symbol.</param>
        public Symbol(string prefix, string symbol, bool bold, bool italic, int subscript, int superscript)
            : this(prefix, symbol, bold, italic, subscript.ToString(), superscript.ToString()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        /// <param name="subscript">The text to use as the subscript for this symbol.</param>
        /// <param name="superscript">The text to use as the superscript for this symbol.</param>
        public Symbol(string symbol, bool bold, bool italic, string subscript, string superscript)
            : this(string.Empty, symbol, bold, italic, subscript, superscript) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for this symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for this symbol.</param>
        public Symbol(string symbol, bool bold, bool italic, int subscript, int superscript)
            : this(string.Empty, symbol, bold, italic, subscript.ToString(), superscript.ToString()) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        public Symbol(string prefix, string symbol, bool bold, bool italic)
            : this(prefix, symbol, bold, italic, string.Empty, string.Empty) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        public Symbol(string symbol, bool bold, bool italic)
            : this(string.Empty, symbol, bold, italic) { }

        /// <summary>
        /// Initialises a new instance of the <see cref="Symbol"/> class.
        /// </summary>
        /// <param name="symbol">The symbol value.</param>
        public Symbol(string symbol)
            : this(symbol, false, false) { }

        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="ISymbol"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="ISymbol"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(ISymbol? other)
        {
            var result = !ReferenceEquals(other, null);

            if (result && other is Symbol symbol)
            {
                result &= symbol.superscript.Equals(superscript);
                result &= symbol.subscript.Equals(subscript);
                result &= symbol.italic.Equals(italic);
                result &= symbol.prefix.Equals(prefix);
                result &= symbol.text.Equals(text);
                result &= symbol.bold.Equals(bold);
            }
            else
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="other">The object to compare to this instance.</param>
        /// <returns>true if other is an <see cref="ISymbol"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? other)
        {
            return other is ISymbol && Equals((ISymbol)other);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            unchecked
            {
                var hashcode = 13;
                hashcode = (hashcode * 7) + (superscript != null ? superscript.GetHashCode() : 0);
                hashcode = (hashcode * 7) + (subscript != null ? subscript.GetHashCode() : 0);
                hashcode = (hashcode * 7) + (prefix != null ? prefix.GetHashCode() : 0);
                hashcode = (hashcode * 7) + (text != null ? text.GetHashCode() : 0);
                hashcode = (hashcode * 7) + italic.GetHashCode();
                hashcode = (hashcode * 7) + bold.GetHashCode();

                return hashcode;
            }
        }

        /// <summary>
        /// Converts the value of the current <see cref="Symbol"/> object to its equivalent string representation using the specified formatter.
        /// </summary>
        /// <param name="formatter">An <see cref="IFormatter"/> object that generates the string representation.</param>
        /// <returns>A string representation of the current <see cref="Symbol"/> object as specified by the formatter.</returns>
        public string ToString(IFormatter formatter)
        {
            return formatter.Format(prefix, text, bold, italic, subscript, superscript);
        }

        /// <summary>
        /// Converts the value of the current <see cref="Symbol"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="Symbol"/> object.</returns>
        public override string ToString()
        {
            return ToString(new HtmlFormatter());
        }

        /// <summary>
        /// Returns an empty symbol.
        /// </summary>
        public static ISymbol Empty => empty;

        /// <summary>
        /// Gets the specified symbol.
        /// </summary>
        /// <param name="text">The name of the symbol.</param>
        /// <returns>The specified symbol as an <see cref="ISymbol"/>.</returns>
        private static ISymbol GetSymbol(string? text)
        {
            ISymbol symbol;

            if (!string.IsNullOrEmpty(text))
            {
                symbol = new Symbol(text);
            }
            else
            {
                symbol = new EmptySymbol();
            }

            return symbol;
        }

        /// <summary>
        /// Gets the specified symbol.
        /// </summary>
        /// <param name="text">The name of the symbol.</param>
        /// <returns>The specified symbol as a string.</returns>
        private static string GetText(string? text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                text = characters.ContainsKey(text) ? characters[text] : text;
            }
            else
            {
                text = string.Empty;
            }

            return text;
        }
    }
}
