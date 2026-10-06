namespace StarLab.Domain.Entities
{
    /// <summary>
    /// TODO: Add a summary for the IStar interface. 
    /// </summary>
    public interface IStar
    {
        /// <summary>
        /// 
        /// </summary>
        double AbsoluteMagnitude { get; }

        /// <summary>
        /// 
        /// </summary>
        double ApparentMagnitude { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        double ColourIndex(string name);

        /// <summary>
        /// 
        /// </summary>
        double Declination { get; }

        /// <summary>
        /// 
        /// </summary>
        string Designation { get; }

        /// <summary>
        /// 
        /// </summary>
        int EffectiveTemperature { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        double Parallax { get; }
        
        /// <summary>
        /// 
        /// </summary>
        double RightAscension { get; }

        /// <summary>
        /// 
        /// </summary>
        string SpectralType { get; }

        /// <summary>
        /// 
        /// </summary>
        double VIColourIndex { get; }
    }
}
