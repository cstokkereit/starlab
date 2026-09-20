namespace StarLab.Presentation.Workspace.Documents
{
    /// <summary>
    /// A class for converting a spectral type e.g. A7 to a double value so that it can be plotted on a chart.
    /// </summary>
    internal class SpectralClassConverter
    {
        private readonly Dictionary<string, double> lookup = new Dictionary<string, double>(); // A dictionary that is used as a lookup table for the numeric equivalent of a spectral class.

        /// <summary>
        /// Initialises a new instance of the <see cref="SpectralClassConverter"/> class;
        /// </summary>
        public SpectralClassConverter()
        {
            CreateSpectralClasses("O", 0);
            CreateSpectralClasses("B", 10);
            CreateSpectralClasses("A", 20);
            CreateSpectralClasses("F", 30);
            CreateSpectralClasses("G", 40);
            CreateSpectralClasses("K", 50);
            CreateSpectralClasses("M", 60);
        }

        /// <summary>
        /// Gets the numeric equivalent for the specified spectral type.
        /// </summary>
        /// <param name="spectralClass"></param>
        /// <returns></returns>
        public double GetValue(string spectralClass)
        {
            return lookup[spectralClass];
        }

        /// <summary>
        /// Adds the entries ranging from 0 - 9 for the spectral classes O - M to the lookup table e.g. A7.5 maps to 27.5
        /// </summary>
        /// <param name="type">The spectral type, one of the values O, B, A, F, G, K and M.</param>
        /// <param name="value">The numeric equivalent of the spectral type provided (0 - 69.9).</param>
        private void CreateSpectralClasses(string type, double value)
        {
            for (int n = 0; n <= 9; n++)
            {
                for (double m = 0; m <= 9; m++)
                {
                    lookup.Add($"{type}{(n + m/10)}", value + n + m/10);
                }
            }
        }
    }
}