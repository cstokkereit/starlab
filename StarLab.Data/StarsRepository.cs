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
                { "SpectralType", 76 }
            };

            

            List<double> xValues = new List<double>();
            List<double> yValues = new List<double>();

            using (var parser = new CatalogueParser(new DelimitedValueFileParser(filename, '|'), map))
            {
                parser.Parse();

                while (!parser.EOF)
                {
                    //if (!string.IsNullOrEmpty(parser.GetValue("ErrorInParallax").Trim()))
                    //{
                    //    double error = parser.GetDoubleValue("ErrorInParallax");

                    //    if (error < 1.5)
                    //    {
                    //        if (!string.IsNullOrEmpty(parser.GetValue("ApparentMagnitude").Trim()) && !string.IsNullOrEmpty(parser.GetValue("Parallax").Trim()) && !string.IsNullOrEmpty(parser.GetValue("SpectralType").Trim()))
                    //        {
                    //            stars.Add(new Star(parser.GetDoubleValue("ApparentMagnitude"), parser.GetDoubleValue("Parallax"), parser.GetValue("SpectralType")));
                    //        }
                    //    }
                    //}

                    parser.Parse();
                }
            }
        }
    }
}
