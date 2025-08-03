namespace StarLab.Domain
{
    public interface IStar
    {
        double AbsoluteMagnitude { get; }

        double ApparentMagnitude { get; }

        SpectralType SpectralType { get; }
    }
}
