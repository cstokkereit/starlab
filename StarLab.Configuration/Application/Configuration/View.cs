namespace StarLab.Application.Configuration
{
    /// <summary>
    /// A POCO representation of a view configuration used for XML serialisation/deserialisation.
    /// </summary>
    internal class View
    {
        public ChildViews? ChildViews { get; set; }

        public ChildView? ChildView { get; set; }

        public string? Name { get; set; }

        public string? Type { get; set; }
    }
}
