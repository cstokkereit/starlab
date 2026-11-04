namespace StarLab.Domain.Entities
{
    /// <summary>
    /// Represents a star.
    /// </summary>
    public interface IStar
    {
        /// <summary>
        /// Gets the absolute magnitude.
        /// </summary>
        double AbsoluteMagnitude { get; }

        /// <summary>
        /// Gets the apparent magnitude.
        /// </summary>
        double ApparentMagnitude { get; }

        /// <summary>
        /// Gets the specified colour index.
        /// </summary>
        /// <param name="name">The name of the colour index.</param>
        /// <returns>The colour index with the specified name.</returns>
        double ColourIndex(string name);

        /// <summary>
        /// Gets the declination.
        /// </summary>
        double Declination { get; }

        /// <summary>
        /// Gets the primary designation.
        /// </summary>
        string Designation { get; }

        /// <summary>
        /// Gets the effective temperature.
        /// </summary>
        int EffectiveTemperature { get; }

        /// <summary>
        /// Gets the common name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the trigonometric parallax.
        /// </summary>
        double Parallax { get; }

        /// <summary>
        /// Gets the right ascension.
        /// </summary>
        double RightAscension { get; }

        /// <summary>
        /// Gets the spectral type.
        /// </summary>
        string SpectralType { get; }
    }
}
