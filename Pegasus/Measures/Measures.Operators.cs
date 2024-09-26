namespace Pegasus.Measures
{
    public partial struct Measure
    {
        public static implicit operator double(Measure measure) => measure.GetDoubleValue();
    }
}
