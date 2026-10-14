using StarLab.Shared.Properties;

namespace StarLab.Shared
{
    /// <summary>
    /// 
    /// </summary>
    public static class OverlayNames
    {
        private static List<string> names = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        static OverlayNames()
        {
            names.Add(MagnitudeClasses);
            names.Add(NamedStars);
            names.Add(Sun);
            names.Add(VariableTypes);
        }

        public const string MagnitudeClasses = "MagnitudeClasses";

        public const string NamedStars = "NamedStars";

        public const string Sun = "Sun";

        public const string VariableTypes = "VariableTypes";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetCaption(string name)
        {
            var caption = Resources.ResourceManager.GetString(name);

            return string.IsNullOrEmpty(caption) ? name : caption;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<string> ToList()
        {
            return names;
        }

        // Brightest stars
        // Stellar types e.g. Wolf rayet, brown dwarf
        // Evolutionary tracks (could prove difficult - can they be calculated?)
        // Standard stars for spectral and magnitude classes

        // Radius (R/Rsun)
        // Mass (M/Msun)
        // Diameter (D/Dsun

        // Custom overlays - for the future
    }
}
