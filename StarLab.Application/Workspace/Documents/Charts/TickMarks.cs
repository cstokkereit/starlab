using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents.Charts
{
    public class TickMarks
    {
        public TickMarks(TickMarksDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.BackColour != null);
            Debug.Assert(dto.ForeColour != null);

            BackColour = dto.BackColour;
            ForeColour = dto.ForeColour;
            Visible = dto.Visible;
        }

        public string BackColour { get; }

        public string ForeColour { get; }

        public bool Visible { get; }
    }
}
