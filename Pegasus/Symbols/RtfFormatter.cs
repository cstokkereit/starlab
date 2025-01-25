using System.Text;

namespace Pegasus.Symbols
{
    /// <summary>
    /// A formatter that generates the RTF representation of a specified symbol.
    /// </summary>
    public class RtfFormatter : IFormatter
    {
        private const string BOLD = "\\b{0}\\b0";
        private const string ITALIC = "\\i{0}\\i0";
        private const string SUBSCRIPT = "\\sub {0}";
        private const string SUPERSCRIPT = "\\super {0}";

        /// <summary>
        /// Generates a string representation of the specified symbol.
        /// </summary>
        /// <param name="prefix">The <see cref="ISymbol"/> to use as the prefix for this symbol.</param>
        /// <param name="symbol">The symbol value.</param>
        /// <param name="bold">A flag that specifies the use of bold text in the string representation of this symbol.</param>
        /// <param name="italic">A flag that specifies the use of italic text in the string representation of this symbol.</param>
        /// <param name="subscript">The <see cref="ISymbol"/> to use as the subscript for this symbol.</param>
        /// <param name="superscript">The <see cref="ISymbol"/> to use as the superscript for this symbol.</param>
        /// <returns>An RTF string representation of the specified symbol.</returns>
        public string Format(ISymbol prefix, string symbol, bool bold, bool italic, ISymbol subscript, ISymbol superscript)
        {
            var buffer = new StringBuilder();

            buffer.Append(prefix.ToString(this));

            if (bold) symbol = string.Format(BOLD, symbol);
            if (italic) symbol = string.Format(ITALIC, symbol);
            buffer.Append(symbol);

            buffer.Append(Format(SUBSCRIPT, subscript));
            buffer.Append(Format(SUPERSCRIPT, superscript));

            return buffer.ToString();
        }

        /// <summary>
        /// Generates a string representation of the specified symbol.
        /// </summary>
        /// <param name="format">The required format.</param>
        /// <param name="symbol">The <see cref="ISymbol"/> being formatted.</param>
        /// <returns>An RTF string representation of the specified symbol.</returns>
        private string Format(string format, ISymbol symbol)
        {
            var text = symbol.ToString(this);

            if (!string.IsNullOrEmpty(text)) text = "{" + string.Format(format, text) + "}";

            return text;
        }
    }
}