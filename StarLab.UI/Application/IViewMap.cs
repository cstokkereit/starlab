namespace StarLab.Application
{
    public interface IViewMap
    {
        event EventHandler<IView> ViewCreated;

        IView this[string id] { get; }

        int Count { get; }

        bool Contains(string id);

        void Initialise();

        void Remove(string id);
    }
}
