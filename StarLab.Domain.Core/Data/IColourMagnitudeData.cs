namespace StarLab.Domain.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IColourMagnitudeData
    {
        double[] AbsoluteVisualMagnitude { get; }

        double[] EffectiveTemperature { get; }

        double[] Luminosity { get; }

        string MagnitudeClass { get; }

        string[] SpectralClass { get; }

        double[] ColourIndex(string name);
    }
}
