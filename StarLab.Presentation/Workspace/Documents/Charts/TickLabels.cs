using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    internal class TickLabels : ITickLabels
    {
        public TickLabels(TickLabelsDTO? dto) 
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            Font = new Font(dto.Font);
            Rotation = dto.Rotation;
        }

        public IFont Font { get; }

        public int Rotation { get; }
    }
}
