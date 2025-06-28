using StarLab.Shared.Properties;

namespace StarLab.Application.Workspace
{
    /// <summary>
    /// The exception that is thrown when an item with the specified type and name already exists.
    /// </summary>
    public class NameExistsException : Exception
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="NameExistsException"/> class.
        /// </summary>
        /// <param name="type">The item type.</param>
        /// <param name="name">The item name.</param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public NameExistsException(ItemTypes type, string name)
            : base(string.Format(Constants.NameExistsMessage, type.ToString().ToLower(), name))
        {
            switch (type)
            {
                case ItemTypes.Document:
                    Target = Resources.Document.ToLower();
                    break;

                case ItemTypes.Folder:
                    Target = Resources.Folder.ToLower();
                    break;

                case ItemTypes.Project:
                    Target = Resources.Project.ToLower();
                    break;

                default:
                    Target = type.ToString();
                    break;
            }

            Name = name;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the target.
        /// </summary>
        public string Target { get; }
    }
}
