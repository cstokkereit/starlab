using System.Text;
using System.Text.RegularExpressions;

namespace StarLab.Domain
{
    /// <summary>
    /// The spectral type of a star defined in the Morgan-Keenan (MK) classification. See https://en.wikipedia.org/wiki/Stellar_classification for details.
    /// </summary>
    public class SpectralType
    {
        private static Regex regex = new Regex(@"(sd|C-[HJNR]d?|D[ABCOQXZ]*)?(W[CNOR])?([OBAFGKMS]\d[\.\d]*[-|\/]?[OBAFGKM]?[\d\.\d]?)?(?: ?)(0?)(I{0,3}V?a?b?[-|\/]?I{0,3}V?a?b?)(?: ?)(.*)", RegexOptions.Compiled);

        private readonly string magnitudeClass = string.Empty;

        private readonly string peculiarities = string.Empty;

        private readonly string spectralClass = string.Empty;

        public SpectralType(string spectralClass, string magnitudeClass, string peculiarities)
        {
            this.magnitudeClass = magnitudeClass;
            this.peculiarities = peculiarities;
            this.spectralClass = spectralClass;
        }

        public SpectralType(string spectralType)
        {
            var parts = regex.Split(spectralType);

            spectralClass = parts[1];
            magnitudeClass = parts[3];
            peculiarities = parts[4];



        }
        

        public string MagnitudeClass => magnitudeClass;

        public string Peculiarities => peculiarities;

        public string SpectralClass => spectralClass;


        public override string ToString()
        {
            var buffer = new StringBuilder();

            if (!string.IsNullOrEmpty(SpectralClass))
            {
                buffer.Append(SpectralClass);
            }

            if (!string.IsNullOrEmpty(MagnitudeClass))
            {
                buffer.Append(MagnitudeClass);
            }

            if (!string.IsNullOrEmpty(Peculiarities))
            {
                buffer.Append(Peculiarities);
            }

            return buffer.ToString();
        }
    }
}
