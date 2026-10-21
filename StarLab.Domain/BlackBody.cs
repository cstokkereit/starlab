using System.Drawing;

namespace StarLab.Domain
{
    internal class BlackBody
    {

        /// <summary>
        /// Gets an approximation the colour of a Black Body at the specified temperature. (Tanner Helland algorithm)
        /// </summary>
        /// <param name="Temperature">The temperature of the black body in Kelvin.</param>
        /// <returns></returns>
        public static Color GetColour(double temperature)
        {
            var t = temperature / 100;

            return Color.FromArgb(GetRed(t), GetGreen(t), GetBlue(t));

        }

        private static int GetGreen(double temperature)
        {
            double green;

            if (temperature <= 66)
            {
                green = 99.4708025861 * Math.Log(temperature) - 161.1195681661;
                if (green < 0) green = 0;
            }
            else
            {
                green = 288.1221695283 * Math.Pow(temperature - 60, -0.0755148492);
                if (green > 255) green = 255;
            }

            return (int)green;
        }

        private static int GetBlue(double temperature)
        {
            if (temperature <= 19) return 0;

            if (temperature > 66) return 255;

            var blue = 138.5177312231 * Math.Log(temperature - 10) - 305.0447927307;

            if (blue > 255) blue = 255;

            return (int)blue;
        }


        private static int GetRed(double temperature)
        {
            if (temperature <= 66) return 255;

            var red = 329.698727446 * Math.Pow(temperature - 60, -0.1332047592);

            if (red > 255) red = 255;

            return (int)red;
        }

    }
}
