using ScottPlot;

namespace StarLab.UI.Workspace.Documents.Charts
{
    /// <summary>
    /// A rule that constrains the minimum and maximum values for a chart axis.
    /// </summary>
    public class LockAxisRule : IAxisRule
    {
        private readonly Axis axis; // The axis to which the rule applies

        private readonly double maximum; // The maximum value

        private readonly double minimum; // The minimum value

        /// <summary>
        /// Initialises a new instance of the <see cref="LockAxisRule"/> class.
        /// </summary>
        /// <param name="axis">Specifies the axis that is being constrained by the rule.</param>
        /// <param name="minimum">The minimum allowed value.</param>
        /// <param name="maximum">The maximum allowed value.</param>
        public LockAxisRule(Axis axis, double minimum, double maximum)
        {
            this.maximum = maximum;
            this.minimum = minimum;
            this.axis = axis;
        }

        /// <summary>
        /// Applies the rule.
        /// </summary>
        /// <param name="rp">Provides access to the plot whose axis is being constrained.</param>
        /// <param name="beforeLayout">Determines whether the rule logic executes before or after the data rectangle is calculated.</param>
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
        /// Applies the minimum and maximum value constraints to the horizontal axis provided.
        /// </summary>
        /// <param name="axis">The <see cref="IXAxis"/> to which the constraints are being applied.</param>
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
        /// Applies the minimum and maximum value constraints to the vertical axis provided.
        /// </summary>
        /// <param name="axis">The <see cref="IYAxis"/> to which the constraints are being applied.</param>
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
