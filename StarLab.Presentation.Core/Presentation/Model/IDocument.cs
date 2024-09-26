namespace StarLab.Presentation.Model
{
    public interface IDocument
    {
        public event EventHandler<string>? NameChanged;

        IContent Content { get; }

        string FullName { get; }

        string ID { get; }

        string Name { get; set; }

        string Path { get; }

        string Type { get; }

        string View { get; }
    }
}
