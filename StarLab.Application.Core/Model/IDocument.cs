namespace StarLab.Application.Model
{
    public interface IDocument
    { 
        public IContent Content { get; }

        public string Name { get; }

        public string Path { get; }

        public string Type { get; }

        public string View { get; }
    }
}
