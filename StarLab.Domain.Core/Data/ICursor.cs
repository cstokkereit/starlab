namespace StarLab.Data
{
    /// <summary>
    /// TODO
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICursor<T>
    {
        T Current { get; }

        void MoveNext();

        void MovePrevious();
    }
}
