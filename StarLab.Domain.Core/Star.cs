namespace StarLab.Domain
{

    //  https://ned.ipac.caltech.edu/level5/Gray/Gray_contents.html



    public class Star : IStar
    {
        private readonly Dictionary<string, IDesignation> designations = new Dictionary<string, IDesignation>();

        private readonly string designation;

        private readonly string name = string.Empty;

        public Star(double apparentMagnitude, double parallax, string spectralType, double bvColourIndex)
        {
            ApparentMagnitude = apparentMagnitude;

            //var d = 1 / (parallax / 1000);
            AbsoluteMagnitude = ApparentMagnitude + 5 * (Math.Log10(parallax / 1000) + 1);

            BVColourIndex = bvColourIndex;

            SpectralType = new SpectralType(spectralType);
        }

        public double AbsoluteMagnitude { get; }

        public double ApparentMagnitude { get; }

        public double BVColourIndex { get; }

        public string Designation => designation;

        public string Name => name;

        public SpectralType SpectralType { get; }
    }
}
