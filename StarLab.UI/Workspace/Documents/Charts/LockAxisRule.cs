using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// TODO : Documentation
    /// </summary>
    public class LockAxisRule : IAxisRule
    {
        private readonly Axis axis; // The axis to which the rule applies

        private readonly double maximum; // The maximum value

        private readonly double minimum; // The minimum value

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        /// <param name="minimum"></param>
        /// <param name="maximum"></param>
        public LockAxisRule(Axis axis, double minimum, double maximum)
        {
            this.maximum = maximum;
            this.minimum = minimum;
            this.axis = axis;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rp"></param>
        /// <param name="beforeLayout"></param>
        public void Apply(RenderPack rp, bool beforeLayout)
        {
            switch (axis)
            {
                case Axis.Bottom:
                    Apply(rp.Plot.Axes.Bottom);
                    break;

                case Axis.Left:
                    Apply(rp.Plot.Axes.Left);
                    break;

                case Axis.Right:
                    Apply(rp.Plot.Axes.Right);
                    break;

                case Axis.Top:
                    Apply(rp.Plot.Axes.Top);
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        private void Apply(IXAxis axis)
        {
            if (axis.IsInverted())
            {
                if (axis.Min > maximum) axis.Min = maximum;
                if (axis.Max < minimum) axis.Max = minimum;
            }
            else
            {
                if (axis.Min < minimum) axis.Min = minimum;
                if (axis.Max > maximum) axis.Max = maximum;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axis"></param>
        private void Apply(IYAxis axis)
        {
            if (axis.IsInverted())
            {
                if (axis.Min > maximum) axis.Min = maximum;
                if (axis.Max < minimum) axis.Max = minimum;
            }
            else
            {
                if (axis.Min < minimum) axis.Min = minimum;
                if (axis.Max > maximum) axis.Max = maximum;
            }
        }
    }
}
