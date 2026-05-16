using System.Diagnostics;

namespace StarLab.Application
{
    /// <summary>
    /// A strongly typed ID.
    /// </summary>
    /// <typeparam name="T">The type to which this ID applies.</typeparam>
    [DebuggerDisplay("{value}")]
    public class ID<T> : IEquatable<ID<T>>
    {
        private readonly string value; // The string value of the ID.

        /// <summary>
        /// Initializes a new instance of the <see cref="ID{T}"/> class.
        /// </summary>
        /// <param name="value">The string value to assign to the ID.</param>
        public ID(string value)
        {
            this.value = value ?? throw new ArgumentNullException(nameof(value));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ID{T}"/> class.
        /// </summary>
        /// <param name="guid">The GUID to assign to the ID.</param>
        public ID(Guid guid)
            : this(guid.ToString().ToUpper()) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ID{T}"/> class.
        /// </summary>
        public ID()
            : this(Guid.NewGuid()) { }

        /// <summary>
        /// Determines whether the specified object is equal to the current instance.
        /// </summary>
        /// <param name="obj">The object to compare with the current instance.</param>
        /// <returns>true if the specified object is equal to the current instance; false otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return (obj is ID<T> id) && Equals(id);
        }

        /// <summary>
        /// Determines whether the specified <see cref="ID{T}"> is equal to the current instance.
        /// </summary>
        /// <param name="other">The <see cref="ID{T}"> to compare with the current instance.</param>
        /// <returns>true if the specified <see cref="ID{T}"> is equal to the current instance; false otherwise.</returns>
        public bool Equals(ID<T>? other)
        {
            if (ReferenceEquals(other, null)) return false;

            if (ReferenceEquals(this, other)) return true;

            return value == other.value;
        }

        /// <summary>
        /// Generates a hash code for the current instance.
        /// </summary>
        /// <returns>The hash code for the current instance.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(typeof(T), value.GetHashCode());
        }

        /// <summary>
        /// Generates a string representation of the current instance.
        /// </summary>
        /// <returns>A string representation of the current instance.</returns>
        public override string ToString()
        {
            return value;
        }

        /// <summary>
        /// Determines whether two specified <see cref="ID{T}"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="ID{T}"/> instance to compare.</param>
        /// <param name="right">The second <see cref="ID{T}"/> instance to compare.</param>
        /// <returns>true if the value of left is equal to the value of right; false otherwise.</returns>
        public static bool operator ==(ID<T>? left, ID<T>? right)
        {
            if (ReferenceEquals(left, right)) return true;

            if (ReferenceEquals(left, null)) return ReferenceEquals(right, null);

            return left.Equals(right);
        }

        /// <summary>
        /// Determines whether two specified <see cref="ID{T}"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="ID{T}"/> instance to compare.</param>
        /// <param name="right">The second <see cref="ID{T}"/> instance to compare.</param>
        /// <returns>true if the value of left is not equal to the value of right; false otherwise.</returns>
        public static bool operator !=(ID<T>? left, ID<T>? right) => !(left == right);
    }
}
