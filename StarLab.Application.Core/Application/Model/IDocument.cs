namespace StarLab.Application.Model
{
    public interface IDocument
    {
        string ID { get; }

        string Name { get; set; }

        string Path { get; }
    }
}
