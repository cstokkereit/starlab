using System.Collections;
using System.Diagnostics;

namespace StarLab.Configuration.Serialisation
{
    /// <summary>
    /// Supports simple iteration over a collection of child view configurations.
    /// </summary>
    internal class ChildViews : IEnumerable<ChildView>
    {
        /// <summary>
        /// Gets or sets a <see cref="List{ChildView}"/> containing the child view configurations. 
        /// </summary>
        public List<ChildView>? ChildView { get; set; }

        /// <summary>
        /// Returns an <see cref="IEnumerator{ChildView}"/> that supports simple iteration over the collection of child view configurations.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{ChildView}"/> that can be used to iterate over the child view configurations.</returns>
        public IEnumerator<ChildView> GetEnumerator()
        {
            Debug.Assert(ChildView != null);

            return ChildView.GetEnumerator();
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator"/> that supports simple iteration over the collection of child view configurations.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> that can be used to iterate over the child view configurations.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            Debug.Assert(ChildView != null);

            return ChildView.GetEnumerator();
        }
    }
}
