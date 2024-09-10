using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Title
    {
        public Title(TitleDTO dto)
        {
            //Alignment = dto.Alignment;
            Color = dto.Color;
            Font = new Font(dto.Font);
            Text = dto.Text;
        }

        public Title()
        {
            //Alignment = StringAlignment.Center;
            //Color = Color.Black;
            Font = new Font();
            Text = string.Empty;
        }

        // StringAlignment Alignment { get; private set; }

        public int Color { get; private set; }

        public Font Font { get; private set; }

        public string Text { get; private set; }
    }
}
