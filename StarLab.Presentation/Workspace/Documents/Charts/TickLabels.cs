using StarLab.Application.Workspace.Documents.Charts;

namespace StarLab.Presentation.Workspace.Documents.Charts
{
    internal class TickLabels : ITickLabels
    {
        public TickLabels(TickLabelsDTO? dto) 
        {
            ArgumentNullException.ThrowIfNull(dto, nameof(dto));

            BackColour = string.IsNullOrEmpty(dto.BackColour) ? Constants.White : dto.BackColour;
            ForeColour = string.IsNullOrEmpty(dto.ForeColour) ? Constants.Black : dto.ForeColour;

            Font = new Font(dto.Font);
            Rotation = dto.Rotation;
            Visible = dto.Visible;
        }

        public string BackColour { get; }

        public IFont Font { get; }

        public string ForeColour { get; }

        public int Rotation { get; }

        public bool Visible { get; }
    }
}
