namespace Pegasus.Symbols
{
    /// <summary>
    /// Represents a formatter that generates the string representation of a specified symbol.
    /// </summary>
    public interface IFormatter
    {
        /// <summary>
        /// Generates a string representation of the specified symbol.
        /// </summary>
        /// <param name="prefix">The <see cref="ISymbol"/> to use as the prefix for this symbol.</param>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        /// <param name="subscript">The <see cref="ISymbol"/> to use as the subscript for this symbol.</param>
        /// <param name="superscript">The <see cref="ISymbol"/> to use as the superscript for this symbol.</param>
        /// <returns>A string representation of the specified symbol.</returns>
        string Format(ISymbol prefix, string symbol, bool bold, bool italic, ISymbol subscript, ISymbol superscript);
    }
}
