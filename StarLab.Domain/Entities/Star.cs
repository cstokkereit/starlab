using StarLab.Domain.Data;

namespace StarLab.Domain.Entities
{
    // https://www.pas.rochester.edu/~emamajek/EEM_dwarf_UBVIJHK_colors_Teff.txt
    // https://en.wikipedia.org/wiki/Color_index

    /// <summary>
    /// Provides access to the properties of a star.
    /// </summary>
    public class Star : IStar
    {
        private readonly Dictionary<string, double> colourIndices = new Dictionary<string, double>(); // A dictionary containing the available colour indices.

        private readonly IAstrometricData astrometry; // The astrometric data for the star.

        /// <summary>
        /// Initialises a new instance of the <see cref="Star"/> class.
        /// </summary>
        /// <param name="data">The entity data for the star.</param>
        public Star(IEntityData data)
        {
            AbsoluteMagnitude = data.GetDoubleValue("AbsoluteMagnitude");
            ApparentMagnitude = data.GetDoubleValue("ApparentMagnitude");
            
            Designation = data.GetStringValue("Designation");
            Name = data.GetStringValue("Name");
            SpectralType = data.GetStringValue("SpectralType");

            astrometry = new AstrometricData(
                data.GetDoubleValue("RightAscension"),
                data.GetDoubleValue("Declination"),
                data.GetDoubleValue("Parallax"),
                data.GetDoubleValue("ProperMotionInRightAscension"),
                data.GetDoubleValue("ProperMotionInDeclination")
            );

            colourIndices.Add("U-B", data.GetDoubleValue("U-B"));
            colourIndices.Add("B-V", data.GetDoubleValue("B-V"));
            colourIndices.Add("V-R", data.GetDoubleValue("V-R"));
            colourIndices.Add("R-I", data.GetDoubleValue("R-I"));

            EffectiveTemperature = CalculateEffectiveTemperature(colourIndices["B-V"]);
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="Star"/> class.
        /// </summary>
        /// <param name="astrometry">The astrometric data for the star.</param>
        public Star(IAstrometricData astrometry)
        {
            this.astrometry = astrometry;
        }

        /// <summary>
        /// Gets the absolute magnitude.
        /// </summary>
        public double AbsoluteMagnitude { get; }

        /// <summary>
        /// Gets the apparent magnitude.
        /// </summary>
        public double ApparentMagnitude { get; }

        /// <summary>
        /// Gets the specified colour index.
        /// </summary>
        /// <param name="name">The name of the colour index.</param>
        /// <returns>The colour index with the specified name.</returns>
        public double ColourIndex(string name)
        {
            return colourIndices[name];
        }

        /// <summary>
        /// Gets the declination.
        /// </summary>
        public double Declination => astrometry.Declination;

        /// <summary>
        /// Gets the primary designation.
        /// </summary>
        public string Designation { get; }

        /// <summary>
        /// Gets the effective temperature.
        /// </summary>
        public int EffectiveTemperature { get; }

        /// <summary>
        /// Gets the common name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the trigonometric parallax.
        /// </summary>
        public double Parallax => astrometry.Parallax;

        /// <summary>
        /// Gets the right ascension.
        /// </summary>
        public double RightAscension => astrometry.RightAscension;

        /// <summary>
        /// Gets the spectral type.
        /// </summary>
        public string SpectralType { get; }

        /// <summary>
        /// Calculates the effective temperature in Kelvin using Ballesteros' formula for effective temperature based on B-V colour index
        /// </summary>
        /// <param name="colourIndex"></param>
        /// <returns></returns>
        private int CalculateEffectiveTemperature(double colourIndex)
        {
            return (int)Math.Round(4600 * (1 / (0.92 * colourIndex + 1.7) + 1 / (0.92 * colourIndex + 0.62)));
        }
    }
}
