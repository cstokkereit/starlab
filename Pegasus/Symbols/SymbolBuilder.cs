namespace Pegasus.Symbols
{
    /// <summary>
    /// A factory class for creating mathematical and scientific symbols.
    /// </summary>
    public static class SymbolBuilder
    {
        #region Constructors
        
        /// <summary>
        /// Static constructor.
        /// </summary>
        static SymbolBuilder()
        {
            Cross = new Symbol("×");
            Dot = new Symbol("·");
        } 

        #endregion

        /// <summary>
        /// Gets a cross symbol for use in products.
        /// </summary>
        public static ISymbol Cross { get; private set; }

        /// <summary>
        /// Gets a dot symbol for use in products.
        /// </summary>
        public static ISymbol Dot { get; private set; }

        /// <summary>
        /// Creates a symbol product from the specified symbols.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise the symbol product.</param>
        /// <param name="separator">The symbol to be used as a separator in the string representation of the symbol product.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol product.</returns>
        public static ISymbol CreateProduct(IEnumerable<ISymbol> symbols, ISymbol separator)
        {
            return new SymbolProduct(symbols, separator);
        }

        /// <summary>
        /// Creates a symbol product from the specified symbols.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise the symbol product.</param>
        /// <param name="separator">The symbol to be used as a separator in the string representation of the symbol product.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol product.</returns>
        public static ISymbol CreateProduct(IEnumerable<string> symbols, ISymbol separator)
        {
            return CreateProduct(symbols.Select(symbol => new Symbol(symbol)).ToList<ISymbol>(), separator);
        }

        /// <summary>
        /// Creates a symbol product from the specified symbols.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise the symbol product.</param>
        /// <param name="separator">The symbol to be used as a separator in the string representation of the symbol product.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol product.</returns>
        public static ISymbol CreateProduct(IEnumerable<string> symbols, string separator)
        {
            return CreateProduct(symbols, new Symbol(separator));
        }

        /// <summary>
        /// Creates a symbol product from the specified symbols.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise the symbol product.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol product.</returns>
        public static ISymbol CreateProduct(IEnumerable<ISymbol> symbols)
        {
            return new SymbolProduct(symbols);
        }

        /// <summary>
        /// Creates a symbol product from the specified symbols.
        /// </summary>
        /// <param name="symbols">A collection containing the symbols that comprise the symbol product.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol product.</returns>
        public static ISymbol CreateProduct(IEnumerable<string> symbols)
        {
            return new SymbolProduct(symbols.Select(symbol => new Symbol(symbol)).ToList<ISymbol>());
        }

        /// <summary>
        /// Creates a symbol product from the specified symbols.
        /// </summary>
        /// <param name="symbols">An array containing the symbols that comprise the symbol product.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol product.</returns>
        public static ISymbol CreateProduct(params ISymbol[] symbols)
        {
            return new SymbolProduct(symbols);
        }

        /// <summary>
        /// Creates a symbol product from the specified symbols.
        /// </summary>
        /// <param name="symbols">A string array containing the symbols that comprise the symbol product.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol product.</returns>
        public static ISymbol CreateProduct(params string[] symbols)
        {
            return new SymbolProduct(symbols.Select(symbol => new Symbol(symbol)).ToList<ISymbol>());
        }

        /// <summary>
        /// Creates a symbol quotient from the specified values.
        /// </summary>
        /// <param name="numerator">The numeric value to be used as the numerator of the symbol quotient.</param>
        /// <param name="denominator">The numeric value to be used as the denominator of the symbol quotient.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol quotient.</returns>
        public static ISymbol CreateQuotient(int numerator, int denominator)
        {
            return new SymbolQuotient(new Symbol(numerator.ToString()), new Symbol(denominator.ToString()));
        }

        /// <summary>
        /// Creates a symbol quotient from the specified symbols.
        /// </summary>
        /// <param name="numerator">The string value to be used as the numerator of the symbol quotient.</param>
        /// <param name="denominator">The string value to be used as the denominator of the symbol quotient.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol quotient.</returns>
        public static ISymbol CreateQuotient(string numerator, string denominator)
        {
            return new SymbolQuotient(new Symbol(numerator), new Symbol(denominator));
        }

        /// <summary>
        /// Creates a symbol quotient from the specified symbols.
        /// </summary>
        /// <param name="numerator">A collection containing the symbols that comprise the numerator of the symbol quotient.</param>
        /// <param name="denominator">A collection containing the symbols that comprise the denominator of the symbol quotient.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol quotient.</returns>
        public static ISymbol CreateQuotient(IEnumerable<ISymbol> numerator, IEnumerable<ISymbol> denominator)
        {
            return new SymbolQuotient(new SymbolProduct(numerator), new SymbolProduct(denominator));
        }

        /// <summary>
        /// Creates a symbol quotient from the specified symbols.
        /// </summary>
        /// <param name="numerator">A collection containing the symbols that comprise the numerator of the symbol quotient.</param>
        /// <param name="denominator">The symbol to be used as the denominator of the symbol quotient.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol quotient.</returns>
        public static ISymbol CreateQuotient(IEnumerable<ISymbol> numerator, ISymbol denominator)
        {
            return new SymbolQuotient(new SymbolProduct(numerator), denominator);
        }

        /// <summary>
        /// Creates a symbol quotient from the specified symbols.
        /// </summary>
        /// <param name="numerator">The symbol to be used as the numerator of the symbol quotient.</param>
        /// <param name="denominator">A collection containing the symbols that comprise the denominator of the symbol quotient.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol quotient.</returns>
        public static ISymbol CreateQuotient(ISymbol numerator, IEnumerable<ISymbol> denominator)
        {
            return new SymbolQuotient(numerator, new SymbolProduct(denominator));
        }

        /// <summary>
        /// Creates a symbol quotient from the specified symbols.
        /// </summary>
        /// <param name="numerator">The symbol to be used as the numerator of the symbol quotient.</param>
        /// <param name="denominator">The symbol to be used as the denominator of the symbol quotient.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol quotient.</returns>
        public static ISymbol CreateQuotient(ISymbol numerator, ISymbol denominator)
        {
            return new SymbolQuotient(numerator, denominator);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbol(string symbol)
        {
            return new Symbol(symbol);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbol(string symbol, bool bold, bool italic)
        {
            return new Symbol(symbol, bold, italic);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The symbol to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefix(ISymbol prefix, string symbol, bool bold, bool italic)
        {
            return new Symbol(prefix, symbol, bold, italic, Symbol.Empty, Symbol.Empty);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefix(string prefix, string symbol, bool bold, bool italic)
        {
            return new Symbol(prefix, symbol, bold, italic);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The symbol to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The symbol to use as the subscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixAndSubscript(ISymbol prefix, string symbol, bool bold, bool italic, ISymbol subscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript, Symbol.Empty);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The text to use as the subscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixAndSubscript(string prefix, string symbol, bool bold, bool italic, string subscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript, string.Empty);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixAndSubscript(string prefix, string symbol, bool bold, bool italic, int subscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript.ToString(), string.Empty);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The symbol to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The symbol to use as the subscript for the symbol.</param>
        /// <param name="superscript">The symbol to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixSubscriptAndSuperscript(ISymbol prefix, string symbol, bool bold, bool italic, ISymbol subscript, ISymbol superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The text to use as the subscript for the symbol.</param>
        /// <param name="superscript">The text to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixSubscriptAndSuperscript(string prefix, string symbol, bool bold, bool italic, string subscript, string superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The text to use as the subscript for the symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixSubscriptAndSuperscript(string prefix, string symbol, bool bold, bool italic, string subscript, int superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript, superscript.ToString());
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for the symbol.</param>
        /// <param name="superscript">The text to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixSubscriptAndSuperscript(string prefix, string symbol, bool bold, bool italic, int subscript, string superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript.ToString(), superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for the symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixSubscriptAndSuperscript(string prefix, string symbol, bool bold, bool italic, int subscript, int superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, subscript, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="superscript">The text to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixAndSuperscript(ISymbol prefix, string symbol, bool bold, bool italic, ISymbol superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, Symbol.Empty, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="superscript">The text to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixAndSuperscript(string prefix, string symbol, bool bold, bool italic, string superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, string.Empty, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="prefix">The text to use as the prefix for this symbol.</param>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithPrefixAndSuperscript(string prefix, string symbol, bool bold, bool italic, int superscript)
        {
            return new Symbol(prefix, symbol, bold, italic, string.Empty, superscript.ToString());
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The symbol to use as the subscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscript(string symbol, bool bold, bool italic, ISymbol subscript)
        {
            return new Symbol(null, symbol, bold, italic, subscript, null);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The text to use as the subscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscript(string symbol, bool bold, bool italic, string subscript)
        {
            return new Symbol(symbol, bold, italic, subscript, string.Empty);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscript(string symbol, bool bold, bool italic, int subscript)
        {
            return new Symbol(symbol, bold, italic, subscript.ToString(), string.Empty);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The symbol to use as the subscript for the symbol.</param>
        /// <param name="superscript">The symbol to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscriptAndSuperscript(string symbol, bool bold, bool italic, ISymbol subscript, ISymbol superscript)
        {
            return new Symbol(null, symbol, bold, italic, subscript, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The text to use as the subscript for the symbol.</param>
        /// <param name="superscript">The text to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscriptAndSuperscript(string symbol, bool bold, bool italic, string subscript, string superscript)
        {
            return new Symbol(symbol, bold, italic, subscript, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The text to use as the subscript for the symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscriptAndSuperscript(string symbol, bool bold, bool italic, string subscript, int superscript)
        {
            return new Symbol(symbol, bold, italic, subscript, superscript.ToString());
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for the symbol.</param>
        /// <param name="superscript">The text to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscriptAndSuperscript(string symbol, bool bold, bool italic, int subscript, string superscript)
        {
            return new Symbol(symbol, bold, italic, subscript.ToString(), superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="subscript">The numeric value to use as the subscript for the symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSubscriptAndSuperscript(string symbol, bool bold, bool italic, int subscript, int superscript)
        {
            return new Symbol(symbol, bold, italic, subscript, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="superscript">The symbol to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSuperscript(string symbol, bool bold, bool italic, ISymbol superscript)
        {
            return new Symbol(null, symbol, bold, italic, null, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="superscript">The text to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSuperscript(string symbol, bool bold, bool italic, string superscript)
        {
            return new Symbol(symbol,bold, italic , string.Empty, superscript);
        }

        /// <summary>
        /// Creates the specified symbol.
        /// </summary>
        /// <param name="symbol">A string that specifies the symbol, this could be either the name of the symbol or the symbol itself e.g. 'theta' or 'θ'.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of the symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of the symbol.</param>
        /// <param name="superscript">The numeric value to use as the superscript for the symbol.</param>
        /// <returns>The <see cref="ISymbol"/> representation of the specified symbol.</returns>
        public static ISymbol CreateSymbolWithSuperscript(string symbol, bool bold, bool italic, int superscript)
        {
            return new Symbol(symbol, bold, italic, string.Empty, superscript.ToString());
        }
    }
}
