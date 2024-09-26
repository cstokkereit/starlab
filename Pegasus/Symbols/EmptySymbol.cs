namespace Pegasus.Symbols
{
    /// <summary>
    /// Represents an empty symbol.
    /// </summary>
    internal class EmptySymbol : ISymbol
    {
        #region ISymbol Members
        
        /// <summary>
        /// Determines whether this instance and a specified object, which must also be an <see cref="ISymbol"/> object, have the same value.
        /// </summary>
        /// <param name="other">The <see cref="ISymbol"/> to compare to this instance.</param>
        /// <returns>true if other has the same value as this instance; false otherwise.</returns>
        public bool Equals(ISymbol? other)
        {
            return other is EmptySymbol;
        }

        /// <summary>
        /// Converts the value of the current <see cref="ISymbol"/> object to its equivalent string representation using the specified formatter.
        /// </summary>
        /// <param name="formatter">An <see cref="IFormatter"/> object that generates the string representation.</param>
        /// <returns>A string representation of the current <see cref="ISymbol"/> object as specified by the formatter.</returns>
        public string ToString(IFormatter formatter)
        {
            return string.Empty;
        } 

        #endregion

        #region Object Overrides

        /// <summary>
        /// Determines whether this instance and a specified object have the same value.
        /// </summary>
        /// <param name="obj">The object to compare to this instance.</param>
        /// <returns>true if obj is an <see cref="ISymbol"/> and its value is the same as this instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return obj is ISymbol && Equals((ISymbol)obj);
        }

        /// <summary>
        /// Returns the hash code for this instance.
        /// </summary>
        /// <returns>A 32-bit signed integer hash code.</returns>
        public override int GetHashCode()
        {
            return 0;
        }

        /// <summary>
        /// Converts the value of the current <see cref="ISymbol"/> object to its equivalent string representation.
        /// </summary>
        /// <returns>A string representation of the current <see cref="ISymbol"/> object.</returns>
        public override string ToString()
        {
            return string.Empty;
        } 

        #endregion
    }
}
