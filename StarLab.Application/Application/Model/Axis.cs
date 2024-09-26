//using StarLab.Application.DataTransfer;

//namespace StarLab.Application.Model
//{
//    public class Axis
//    {
//        public Axis(AxisDTO dto)
//        {
//            if (dto != null)
//            {
//                Color = dto.Color;
//                Font = new Font(dto.Font);
//                Interval = dto.Interval;
//                IsReversed = dto.IsReversed;
//                Maximum = dto.Maximum;
//                Minimum = dto.Minimum;
//                Title = new Title(dto.Title);
//                Visible = dto.Visible;
//            }
//        }

//        public Axis()
//        {
//            //Color = Color.Black;
//            Font = new Font();
//            Interval = 1;
//            IsReversed = false;
//            Maximum = 1;
//            Minimum = 0;
//            Title = new Title();
//            Visible = false;
//        }

//        public int Color { get; private set; }

//        public Font Font { get; private set; }

//        public double Interval { get; private set; }

//        public bool IsReversed { get; private set; }

//        public double Maximum { get; private set; }

//        public double Minimum { get; private set; }

//        public Title Title { get; private set; }

//        public bool Visible { get; private set; }
//    }
//}
