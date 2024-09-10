namespace StarLab.Application.Model
{
    public interface IContent
    {
        List<IContent> Contents { get; }

        string Name { get; }

        string View { get; }
    }
}
