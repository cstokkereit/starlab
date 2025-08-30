using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stratosoft.Nomenclature.Tests
{
    public class TermBuilder
    {
        private readonly Dictionary<string, Property> properties = new();

        public TermBuilder AddProperties(IEnumerable<Property> properties)
        {
            foreach (Property property in properties)
            {
                AddProperty(property);
            }

            return this;
        }

        public TermBuilder AddProperty(Property property)
        {
            if (properties.ContainsKey(property.Name)) throw new ArgumentException("");
            properties.Add(property.Name, property);
            return this;
        }

        public TermBuilder AddProperty(string name, string description)
        {
            return AddProperty(new Property(name, description));
        }

        public TermBuilder AddProperty(string name)
        {
            return AddProperty(new Property(name));
        }

        public Term CreateTerm(string name, string description)
        {
            Term term = new(name, description);
            AddProperties(term);
            return term;
        }

        public Term CreateTerm(string name)
        {
            Term term = new(name);
            AddProperties(term);
            return term;
        }

        private void AddProperties(Term term)
        {
            foreach (Property property in properties.Values)
            {
                term.Add(property);
            }

            properties.Clear();
        }
    }
}
