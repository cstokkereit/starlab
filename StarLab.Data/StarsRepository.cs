using StarLab.Domain;
using Stratosoft.File.IO;
using System.Collections;

namespace StarLab.Data
{
    // https://en.wikipedia.org/wiki/Stellar_classification
    public class StarsRepository : IStarsRepository
    {
        private readonly List<IStar> stars = new List<IStar>();

        public IEnumerator<IStar> GetEnumerator()
        {
            return stars.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        // TODO - This is a temporary method for populating the repository
        public void Populate()
        {
            var filename = "D:\\Users\\Colin\\Documents\\Science\\Astronomy\\Catalogs\\Hipparcos\\hip_main.dat";

            var map = new Dictionary<string, int>()
            {
                { "ApparentMagnitude", 5 },
                { "Parallax", 11 },
                { "ErrorInParallax", 16 },
                { "B-V", 37 },
                { "SpectralType", 76 }
            };

            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            using (var parser = new FileParser(new DelimitedValueParser(filename, '|'), map))
            {
                parser.Parse();

                while (!parser.EOF)
                {
                    var temp = parser.GetValue("ErrorInParallax").Trim();

                    if (!string.IsNullOrEmpty(temp))
                    {
                        double error = double.Parse(temp);

                        //if (error < 5)
                        //{
                            var apparentMagnitude = parser.GetValue("ApparentMagnitude").Trim();
                            var bvColourIndex = parser.GetValue("B-V").Trim();
                            var parallax = parser.GetValue("Parallax").Trim();

                            if (!string.IsNullOrEmpty(apparentMagnitude) && !string.IsNullOrEmpty(bvColourIndex) && !string.IsNullOrEmpty(parallax))
                            {
                                var spectralType = parser.GetValue("SpectralType").Trim();

                                //if (!string.IsNullOrEmpty(spectralType))
                                //{
                                    stars.Add(new Star(double.Parse(apparentMagnitude), double.Parse(parallax), spectralType, double.Parse(bvColourIndex)));
                                //}
                            }
                        //}
                    }

                    parser.Parse();
                }
            }
        }
    }
}
