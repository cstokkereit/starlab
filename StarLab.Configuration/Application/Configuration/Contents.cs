using System.Collections;
using System.Diagnostics;

namespace StarLab.Application.Configuration
{
    internal class Contents : IEnumerable<Content>
    {
        public List<Content>? Content { get; set; }

        public IEnumerator<Content> GetEnumerator()
        {
            Debug.Assert(Content != null);

            return Content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            Debug.Assert(Content != null);

            return Content.GetEnumerator();
        }
    }
}
