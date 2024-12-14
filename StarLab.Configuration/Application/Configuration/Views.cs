using System.Collections;
using System.Diagnostics;

namespace StarLab.Application.Configuration
{
    internal class Views : IEnumerable<View>
    {
        public List<View>? View { get; set; }

        public IEnumerator<View> GetEnumerator()
        {
            Debug.Assert(View != null);

            return View.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Debug.Assert(View != null);

            return View.GetEnumerator();
        }
    }
}
