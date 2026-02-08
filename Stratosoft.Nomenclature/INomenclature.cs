namespace Stratosoft.Nomenclature
{
    /// <summary>
    /// Represents a nomenclature.
    /// </summary>
    public interface INomenclature
    {
        /// <summary>
        /// Gets the nomenclature description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the nomenclature ID.
        /// </summary>
        Guid ID { get; }

        /// <summary>
        /// Gets the nomenclature name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets an <see cref="IEnumerable{ITerm}"> containing the terms associated with this nomenclature.
        /// </summary>
        IEnumerable<ITerm> Terms { get; }

        /// <summary>
        /// Adds the <see cref="ITerm"/> provided to the nomenclature.
        /// </summary>
        /// <param name="term">The <see cref="ITerm"/> being added.</param>
        /// <exception cref="ArgumentException"></exception>
        void Add(ITerm term);

        /// <summary>
        /// Gets the specified <see cref="Term"/>.
        /// </summary>
        /// <param name="name">The name of the required <see cref="Term"/>.</param>
        /// <returns>The specified <see cref="Term"/>.</returns>
        ITerm GetTerm(string name);

        /// <summary>
        /// Removes the <see cref="ITerm"/> with the specified name from the nomenclature.
        /// </summary>
        /// <param name="name">The name of the <see cref="ITerm"/> being removed.</param>
        void Remove(string name);

        /// <summary>
        /// Removes the specified <see cref="ITerm"/> from the nomenclature.
        /// </summary>
        /// <param name="term">The <see cref="ITerm"/> being removed.</param>
        void Remove(ITerm term);
    }
}
