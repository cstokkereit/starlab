using System.Text.RegularExpressions;

namespace StarLab.Domain
{
    /// <summary>
    /// The spectral type of a star defined in the Morgan-Keenan (MK) classification. See https://en.wikipedia.org/wiki/Stellar_classification for details.
    /// </summary>
    public class SpectralType
    {
        private static Regex regex = new Regex(@"([OBAFGKLMNRSTY]\(?\d?\.?\d?\+?\)?[-|\/]?[OBAFGKLMNRSTY]?\d?\.?\d?)(I{0,3}V?a?b?\+?[-|\/]?I{0,3}V?a?b?\+?)(.*)", RegexOptions.Compiled);

        private static Regex regexCarbon = new Regex(@"(C-?[HJNR]?\d?[,|\.]?\d?)(I{0,3}V?a?b?I{0,3})(.*)", RegexOptions.Compiled);

        private static Regex regexSubDwarf = new Regex(@"(sd)([ABFGKMO]\d?\.?\d?)(.*)", RegexOptions.Compiled);

        private static Regex regexWhiteDwarf = new Regex(@"(D[ABCFGKMOQXZ][BOZ]?\d?\.?\d?)(.*)", RegexOptions.Compiled);

        private static Regex regexWolfRayet = new Regex(@"(W[CNOR]\d?\.?\d?)(.*)", RegexOptions.Compiled);

        private static string[] letters = ["A", "B", "C", "D", "F", "G", "K", "L", "M", "N", "O", "R", "S", "T", "W", "Y"];

        private readonly string magnitudeClass = string.Empty;

        private readonly string peculiarities = string.Empty;

        private readonly string spectralClass = string.Empty;

        private readonly string spectralType = string.Empty;

        public SpectralType(string spectralClass, string magnitudeClass, string peculiarities)
        {
            this.magnitudeClass = magnitudeClass;
            this.peculiarities = peculiarities;
            this.spectralClass = spectralClass;

            if (peculiarities.StartsWith("+ "))
            {
                peculiarities = " " + peculiarities;
            }

            if (magnitudeClass == "sd")
            {
                spectralType = magnitudeClass + spectralClass +peculiarities;
            }
            else
            {
                spectralType = spectralClass + magnitudeClass + peculiarities;
            }
        }

        public SpectralType(string spectralType)
        {
            this.spectralType = spectralType;

            //string[] parts = ["", "", ""];

            //try
            //{

            //    switch (spectralType.Substring(0, 1))
            //    {
            //        case "C":
            //            parts = ParseCarbonStar(spectralType);
            //            break;

            //        case "D":
            //            parts = ParseWhiteDwarf(spectralType);
            //            break;

            //        case "W":
            //            parts = ParseWolfRayet(spectralType);
            //            break;

            //        case "s":
            //            parts = ParseSubDwarf(spectralType);
            //            break;

            //        default:
            //            parts = Parse(spectralType);
            //            break;
            //    }
            //}
            //catch (Exception e)
            //{
            //    var x = spectralType;
            //}

            spectralClass = ""; // parts[0];
            magnitudeClass = ""; // parts[1];
            peculiarities = ""; // parts[2];
        }
        
        public string MagnitudeClass => magnitudeClass;

        public string Peculiarities => peculiarities;

        public string SpectralClass => spectralClass;

        public override string ToString() => spectralType;

        private string[] ParseWolfRayet(string spectralType)
        {
            var matches = regexWolfRayet.Matches(spectralType);

            return [matches[0].Groups[1].Value, "", matches[0].Groups[2].Value.Trim()];
        }


        private string[] Parse(string spectralType)
        {
            string[] retval = ["", "", spectralType];

            var matches = regex.Matches(spectralType);

            if (matches.Count > 0)
            {
                retval = [matches[0].Groups[1].Value, matches[0].Groups[2].Value, matches[0].Groups[3].Value.Trim()];

                if (retval[0].EndsWith("("))
                {
                    retval[0] = retval[0].Substring(0, retval[0].Length - 1);

                    if (!string.IsNullOrEmpty(retval[1]))
                    {
                        retval[1] = "(" + retval[1];
                    }
                    else
                    {
                        retval[2] = "(" + retval[2];
                    }
                }

                if (retval[1].EndsWith("+"))
                {
                    retval = HandleComponents(retval);
                }
            }

            return retval;
        }


        private string[] HandleComponents(string[] parts)
        {
            foreach (var letter in letters)
            {
                if (parts[2].StartsWith(letter))
                {
                    parts[1] = parts[1].Substring(0, parts[1].Length - 1);
                    parts[2] = "+" + parts[2];
                    break;
                }
            }
            
            return parts;
        }



        private string[] ParseCarbonStar(string spectralType)
        {
            var matches = regexCarbon.Matches(spectralType);

            return [matches[0].Groups[1].Value, matches[0].Groups[2].Value, matches[0].Groups[3].Value.Trim()];
        }

        private string[] ParseSubDwarf(string spectralType)
        {
            var matches = regexSubDwarf.Matches(spectralType);

            return [matches[0].Groups[2].Value, matches[0].Groups[1].Value, matches[0].Groups[3].Value.Trim()];
        }

        private string[] ParseWhiteDwarf(string spectralType)
        {
            var matches = regexWhiteDwarf.Matches(spectralType);

            return [matches[0].Groups[1].Value, "", matches[0].Groups[2].Value.Trim()];
        }


    }
}
