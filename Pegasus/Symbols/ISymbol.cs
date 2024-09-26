namespace Pegasus.Symbols
{
    /// <summary>
    /// Represents a mathematical or scientific symbol.
    /// </summary>
    public interface ISymbol : IEquatable<ISymbol>
    {
        /// <summary>
        /// Converts the value of this symbol to a string using the specified formatter.
        /// </summary>
        /// <param name="formatter">The <see cref="IFormatter"/> that will be used to generate the string.</param>
        /// <returns>A string representation of this symbol.</returns>
        string ToString(IFormatter formatter);
    }
}
