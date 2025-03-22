using System.Collections;
using System.Diagnostics;

namespace StarLab.Configuration.Serialisation
{
    /// <summary>
    /// Supports simple iteration over a collection of view configurations.
    /// </summary>
    internal class Views : IEnumerable<View>
    {
        /// <summary>
        /// Gets or sets a <see cref="List{View}"/> containing the view configurations. 
        /// </summary>
        public List<View>? View { get; set; }

        /// <summary>
        /// Returns an <see cref="IEnumerator{View}"/> that supports simple iteration over the collection of view configurations.
        /// </summary>
        /// <returns>An <see cref="IEnumerator{View}"/> that can be used to iterate over the view configurations.</returns>
        public IEnumerator<View> GetEnumerator()
        {
            Debug.Assert(View != null);

            return View.GetEnumerator();
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator"/> that supports simple iteration over the collection of view configurations.
        /// </summary>
        /// <returns>An <see cref="IEnumerator"/> that can be used to iterate over the view configurations.</returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            Debug.Assert(View != null);

            return View.GetEnumerator();
        }
    }
}
