namespace Stratosoft.Nomenclature
{
    public class NumericValue
    {
        private readonly double doubleValue;

        private readonly long longValue;

        #region Constructors

        public NumericValue(double value)
        {
            doubleValue = value;
        }

        public NumericValue(long value)
        {
            longValue = value;
        }

        #endregion

        public double Value
        {             
            get { return longValue != 0 ? longValue : doubleValue; }
        }

        #region Object Overrides

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string value;

            if (longValue != 0)
            {
                value = longValue.ToString();
            }
            else
            {
                value = doubleValue.ToString();
            }

            return value;
        } 

        #endregion
    }
}
