using Pegasus.Symbols;

namespace Pegasus.Units
{
    public class SIUnit : Unit
    {
        private readonly double multiplier;

        private readonly ISymbol prefix;

        public SIUnit(string name, ISymbol symbol, SIUnitPrefixes prefix, IFormatter formatter)
            : base(name, symbol, formatter)
        {
            multiplier = Math.Pow(10, (int)prefix);
            this.prefix = GetSymbol(prefix);
        }

        public SIUnit(string name, ISymbol symbol, SIUnitPrefixes prefix)
            : this(name, symbol, prefix, new HtmlFormatter()) { }

        public SIUnit(string name, string symbol, SIUnitPrefixes prefix)
            : this(name, new Symbol(symbol), prefix) { }

        public SIUnit(string name, ISymbol symbol, IFormatter formatter)
            : base(name, symbol, formatter)
        {
            prefix = new EmptySymbol();
            multiplier = 1;
        }

        public SIUnit(string name, ISymbol symbol)
            : this(name, symbol, new HtmlFormatter()) { }

        public SIUnit(string name, string symbol)
            : this(name, new Symbol(symbol)) { }

        public double Multiplier => multiplier;

        public override string ToString()
        {
            return prefix.ToString() + base.ToString();
        }

        private ISymbol GetSymbol(SIUnitPrefixes prefix)
        {
            var symbol = string.Empty; 

            switch (prefix)
            {
                case SIUnitPrefixes.yotta:
                    symbol = "Y";
                    break;

                case SIUnitPrefixes.zetta:
                    symbol = "Z";
                    break;

                case SIUnitPrefixes.exa:
                    symbol = "E";
                    break;

                case SIUnitPrefixes.peta:
                    symbol = "P";
                    break;

                case SIUnitPrefixes.tera:
                    symbol = "T";
                    break;

                case SIUnitPrefixes.giga:
                    symbol = "G";
                    break;

                case SIUnitPrefixes.mega:
                    symbol = "M";
                    break;

                case SIUnitPrefixes.kilo:
                    symbol = "k";
                    break;

                case SIUnitPrefixes.hecto:
                    symbol = "h";
                    break;

                case SIUnitPrefixes.deca:
                    symbol = "da";
                    break;

                case SIUnitPrefixes.deci:
                    symbol = "d";
                    break;

                case SIUnitPrefixes.centi:
                    symbol = "c";
                    break;

                case SIUnitPrefixes.milli:
                    symbol = "m";
                    break;

                case SIUnitPrefixes.micro:
                    symbol = "mu";
                    break;

                case SIUnitPrefixes.nano:
                    symbol = "n";
                    break;

                case SIUnitPrefixes.pico:
                    symbol = "p";
                    break;

                case SIUnitPrefixes.femto:
                    symbol = "f";
                    break;

                case SIUnitPrefixes.atto:
                    symbol = "a";
                    break;

                case SIUnitPrefixes.zepto:
                    symbol = "z";
                    break;

                case SIUnitPrefixes.yocto:
                    symbol = "y";
                    break;
            }

            return string.IsNullOrEmpty(symbol) ? new EmptySymbol() : new Symbol(symbol);
        }
    }
}
