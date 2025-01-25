namespace StarLab.Application.Configuration
{
    /// <summary>
    /// A POCO representation of a child view configuration used for XML serialisation/deserialisation.
    /// </summary>
    internal class ChildView
    {
        public string? Name { get; set; }

        public string? Panel { get; set; }

        public string? Presenter { get; set; }

        public string? View { get; set; }
    }
}
