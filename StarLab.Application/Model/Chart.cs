using StarLab.Application.DataTransfer;

namespace StarLab.Application.Model
{
    public class Chart
    {
        public Chart(ChartDTO dto)
        {
            BackColor = dto.BackColor;
            ForeColor = dto.ForeColor;

            Type = dto.Type;

            if (dto.MajorGrid != null)
            {
                MajorGrid = new Grid(dto.MajorGrid);
            }

            if (dto.MinorGrid != null)
            {
                MinorGrid = new Grid(dto.MinorGrid);
            }

            if (dto.XAxis != null)
            {
                XAxis = new Axis(dto.XAxis);
            }

            if (dto.XAxis2 != null)
            {
                XAxis2 = new Axis(dto.XAxis2);
            }

            if (dto.YAxis != null)
            {
                YAxis = new Axis(dto.YAxis);
            }

            if (dto.YAxis2 != null)
            {
                YAxis2 = new Axis(dto.YAxis2);
            }
        }

        public Chart()
        {
            //BackColor = Color.White;
            //ForeColor = Color.Black;
            MajorGrid = new Grid();
            MinorGrid = new Grid();
            XAxis = new Axis();
            XAxis2 = new Axis();
            YAxis = new Axis();
            YAxis2 = new Axis();
        }

        public int BackColor { get; private set; }

        public int ForeColor { get; private set; }

        public Grid MajorGrid { get; private set; }

        public Grid MinorGrid { get; private set; }

        public string Type { get; private set; }

        public Axis XAxis { get; private set; }

        public Axis XAxis2 { get; private set; }

        public Axis YAxis { get; private set; }

        public Axis YAxis2 { get; private set; }
    }
}
