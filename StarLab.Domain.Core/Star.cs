namespace StarLab.Domain
{

    //  https://ned.ipac.caltech.edu/level5/Gray/Gray_contents.html



    public class Star : IStar
    {
        public Star(double apparentMagnitude, double parallax, string spectralType)
        {
            ApparentMagnitude = apparentMagnitude;

            var d = 1 / (parallax / 1000);
            AbsoluteMagnitude = 5 + ApparentMagnitude - (5 * Math.Log10(d));

            SpectralType = new SpectralType(spectralType);
        }

        public double AbsoluteMagnitude { get; private set; }

        public double ApparentMagnitude { get; private set; }

        public SpectralType SpectralType { get; private set; }
    }
}
