using Pegasus.Symbols;

namespace Pegasus.Units
{
    public class Unit
    {
        private readonly IFormatter formatter;

        private readonly ISymbol symbol;

        private readonly string name;

        public Unit(string name, ISymbol symbol, IFormatter formatter)
        {
            this.formatter = formatter;
            this.symbol = symbol;
            this.name = name;
        }

        public Unit(string name, string symbol, IFormatter formatter)
            : this(name, new Symbol(symbol), formatter) { }

        public Unit(string name, ISymbol symbol)
            : this(name, symbol, new HtmlFormatter()) { }

        public Unit(string name, string symbol)
            : this(name, new Symbol(symbol)) { }

        public override string ToString()
        {
            return string.Empty + symbol.ToString(formatter);
        }

        protected IFormatter Formatter => formatter;
    }
}
