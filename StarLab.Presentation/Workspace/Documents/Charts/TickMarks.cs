using StarLab.Application.Workspace.Documents.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    internal class TickMarks : ITickMarks
    {
        public TickMarks(TickMarksDTO? dto) 
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));
        }
    }
}
