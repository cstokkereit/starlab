using StarLab.Domain;

namespace StarLab
{
    public interface IStar
    {
        double AbsoluteMagnitude { get; }

        double ApparentMagnitude { get; }

        double BVColourIndex { get; }

        string Designation { get; }

        string Name { get; }

        SpectralType SpectralType { get; }
    }
}
