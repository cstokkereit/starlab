using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class TickLabels
    {
        public TickLabels(TickLabelsDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.BackColour != null);
            Debug.Assert(dto.ForeColour != null);
            Debug.Assert(dto.Font != null);

            BackColour = dto.BackColour;
            ForeColour = dto.ForeColour;
            Font = new Font(dto.Font);
            Rotation = dto.Rotation;
            Visible = dto.Visible;
        }

        public string BackColour { get; }

        public Font Font { get; }

        public string ForeColour { get; }

        public int Rotation { get; }

        public bool Visible { get; }
    }
}
