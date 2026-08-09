namespace StarLab.Domain.Entities
{
    public interface IStar
    {
        double AbsoluteMagnitude { get; }

        double ApparentMagnitude { get; }

        double BVColourIndex { get; }

        string Designation { get; }

        string Name { get; }

        ISpectralType SpectralType { get; }
    }
}
