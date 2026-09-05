namespace StarLab.Domain.Data
{
    public class AstrometricData : IAstrometricData
    {
        private readonly double rightAscension;

        private readonly double declination;

        private readonly double parallax;

        private readonly double properMotionInDeclination;

        private readonly double properMotionInRightAscension;

        public AstrometricData(double rightAscension, double declination, double parallax, double properMotionInRightAscension, double properMotionInDeclination)
        {
            this.properMotionInRightAscension = properMotionInRightAscension;
            this.properMotionInDeclination = properMotionInDeclination;
            this.rightAscension = rightAscension;
            this.declination = declination;
            this.parallax = parallax;
        }

        public double Declination => declination;

        public double Parallax => parallax;

        public double ProperMotionInDeclination => properMotionInDeclination;

        public double ProperMotionInRightAscension => properMotionInRightAscension;

        public double RightAscension => rightAscension;
    }
}
