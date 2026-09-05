namespace StarLab.Domain.Data
{
    public interface IAstrometricData
    {
        double Declination { get; }

        double Parallax { get; }

        double ProperMotionInDeclination { get; }

        double ProperMotionInRightAscension { get; }

        double RightAscension { get; }
    }
}
