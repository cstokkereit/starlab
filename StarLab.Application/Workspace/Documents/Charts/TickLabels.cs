using System.Diagnostics;

namespace StarLab.Application.Workspace.Documents.Charts
{
    internal class TickLabels
    {
        public TickLabels(TickLabelsDTO? dto)
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Debug.Assert(dto.Font != null);

            Font = new Font(dto.Font);
            Rotation = dto.Rotation;
        }

        public Font Font { get; }

        public int Rotation { get; }
    }
}
