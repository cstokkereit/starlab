using StarLab.Presentation;

namespace StarLab.UI
{
    public interface IViewMap
    {
        event EventHandler<IView> DocumentCreated;

        IView this[string name] { get; }

        int Count { get; }

        bool Contains(string name);

        void Initialise(IApplicationController controller);

        void Remove(string name);
    }
}
