namespace StarLab.Application.UseCases
{
    public abstract class Interactor<T>
    {
        public abstract void Execute(T args);
    }
}
