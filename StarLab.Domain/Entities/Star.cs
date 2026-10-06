using StarLab.Domain.Data;

namespace StarLab.Domain.Entities
{
    // https://www.pas.rochester.edu/~emamajek/EEM_dwarf_UBVIJHK_colors_Teff.txt
    // https://en.wikipedia.org/wiki/Color_index

    /// <summary>
    /// TODO: Add a summary for the Star class.
    /// </summary>
    public class Star : IStar
    {
        private readonly Dictionary<string, double> colourIndices = new Dictionary<string, double>();

        private readonly IAstrometricData astrometry;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
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

        public Star(IAstrometricData astrometry)
        {
            this.astrometry = astrometry;
        }

        public double AbsoluteMagnitude { get; }

        public double ApparentMagnitude { get; }

        public double ColourIndex(string name)
        {
            return colourIndices[name];
        }

        public double Declination => astrometry.Declination;

        public string Designation { get; }

        public int EffectiveTemperature { get; }

        public string Name { get; }

        public double Parallax => astrometry.Parallax;

        public double RightAscension => astrometry.RightAscension;

        public string SpectralType { get; }

        public double VIColourIndex { get; }

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
