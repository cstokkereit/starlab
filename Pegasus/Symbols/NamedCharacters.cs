namespace Pegasus.Symbols
{
    /// <summary>
    /// A dictionary containing unicode characters indexed by name.
    /// </summary>
    public class NamedCharacters : Dictionary<string, string>
    {
        private static readonly string[] greek = new[] { "Alpha", "Beta", "Gamma", "Delta", "Epsilon", "Zeta", "Eta", "Theta", "Iota", "Kappa", "Lambda", "Mu", "Nu", "Xi", "Omicron", "Pi", "Rho", "Sigma", "Tau", "Upsilon", "Phi", "Chi", "Psi", "Omega" }; // An array containing the names of the Greek letters.

        /// <summary>
        /// Initialises a new instance of the <see cref="NamedCharacters"/> class.
        /// </summary>
        public NamedCharacters()
        {
            Add("degree", char.ConvertFromUtf32(176));

            AddGreekCharacters();
        }

        /// <summary>
        /// Adds the Greek characters to the dictionary.
        /// </summary>
        private void AddGreekCharacters()
        {
            for (int n = 0; n < greek.Length; n++)
            {
                if (n > 16)
                {
                    Add(greek[n].ToLower(), char.ConvertFromUtf32(n + 946));
                    Add(greek[n], char.ConvertFromUtf32(n + 914));
                }
                else
                {
                    Add(greek[n].ToLower(), char.ConvertFromUtf32(n + 945));
                    Add(greek[n], char.ConvertFromUtf32(n + 913));
                }
            }
        }
    }
}
