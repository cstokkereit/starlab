namespace StarLab.Application.Events
{
    public interface IEventAggregator
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="e"></param>
        void Publish<T>(T e);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="subscriber"></param>
        void Subsribe(object subscriber);
    }
}
