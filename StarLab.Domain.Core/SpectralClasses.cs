namespace StarLab.Domain.Core
{
    /// <summary>
    /// TODO : Documentation
    /// </summary>
    public class SpectralClasses
    {
        private static readonly Dictionary<string, double> lookup = new Dictionary<string, double>(); // A dictionary that is used as a lookup table for the numeric equivalent of a spectral class.

        /// <summary>
        /// Initialises a new instance of the <see cref="SpectralClasses"/> class;
        /// </summary>
        static SpectralClasses()
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
        /// Maps a spectral class to a numeric value that can be used in charts and overlays.
        /// </summary>
        /// <param name="spectralClass">The spectral class.</param>
        /// <returns>A numeric value that represents the spectral class.</returns>
        public static double ToDouble(string spectralClass)
        {
            return lookup[spectralClass];
        }

        /// <summary>
        /// Adds the entries ranging from 0 - 9 for the spectral classes O - M to the lookup table e.g. A7.5 maps to 27.5
        /// </summary>
        /// <param name="type">The spectral type, one of the values O, B, A, F, G, K and M.</param>
        /// <param name="value">The numeric equivalent of the spectral type provided (0 - 69.9).</param>
        private static void CreateSpectralClasses(string type, double value)
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