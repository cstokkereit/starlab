namespace StarLab.Domain.Data
{
    /// <summary>
    /// A static class containing constants used to identify stellar parameters.
    /// </summary>
    public static class StellarParameters
    {
        public const string AbsoluteMagnitude = "AbsoluteMagnitude";

        public const string EffectiveTemperature = "EffectiveTemperature";

        public const string Luminosity = "Luminosity";

        public const string Mass = "Mass";

        public const string MagnitudeClass = "MagnitudeClass";

        public const string Parallax = "Parallax";

        public const string SpectralClass = "SpectralClass";

        /// <summary>
        /// The name of the colour index calculated from the specified short and long wavelength filters.
        /// </summary>
        /// <param name="f1">Short wavelength filter.</param>
        /// <param name="f2">Long wavelength filter.</param>
        /// <returns>The specified colour index.</returns>
        public static string ColourIndex(string f1, string f2)
        {
            return $"ColourIndex({f1}-{f2})";
        }
    }
}
