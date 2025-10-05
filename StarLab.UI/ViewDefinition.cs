using StarLab.Presentation;

namespace StarLab.UI
{
    /// <summary>
    /// A class that holds the parameter values used to create and configure a view or child view.
    /// </summary>
    public class ViewDefinition : IViewDefinition
    {
        private readonly List<IViewDefinition> definitions = new List<IViewDefinition>(); // A list containing the child view configurations.

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewDefinition"/> class. 
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <param name="type">The view type.</param>
        public ViewDefinition(string name, ViewTypes type)
        { 
            switch (type)
            {
                case ViewTypes.Application:
                    TypeName = "StarLab.UI.Workspace.ApplicationView, StarLab.UI";
                    break;

                case ViewTypes.Dialog:
                    TypeName = "StarLab.UI.DialogView, StarLab.UI";
                    break;

                case ViewTypes.Document:
                    TypeName = "StarLab.UI.Workspace.Documents.DocumentView, StarLab.UI";
                    break;

                case ViewTypes.MessageBox:
                    TypeName = "StarLab.UI.MessageBoxView, StarLab.UI";
                    break;

                case ViewTypes.Tool:
                    TypeName = "StarLab.UI.ToolView, StarLab.UI";
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(type));
            }

            ViewType = type;
            Name = name;
        }

        /// <summary>
        /// Initialises a new instance of the <see cref="ViewDefinition"/> class. 
        /// </summary>
        /// <param name="name">The view name.</param>
        /// <param name="type">The view type.</param>
        private ViewDefinition(string name, string type)
        {
            ViewType = ViewTypes.Content;
            TypeName = type;
            Name = name;
        }

        /// <summary>
        /// Gets an <see cref="IReadOnlyList{IViewDefinition}"/> containing the child view definitions.
        /// </summary>
        public IReadOnlyList<IViewDefinition> ChildViewDefinitions => definitions;

        /// <summary>
        /// Gets the view name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the name of the class that implements the view.
        /// </summary>
        public string TypeName { get; }

        /// <summary>
        /// Gets the view type.
        /// </summary>
        public ViewTypes ViewType { get; }

        /// <summary>
        /// A fluent interface method that adds the definition of a child view to the view definition.
        /// </summary>
        /// <param name="name">The child view name.</param>
        /// <param name="type">The child view type.</param>
        /// <returns>The <see cref="ViewDefinition"/> being configured.</returns>
        public ViewDefinition AddChild(string name, string type)
        {
            definitions.Add(new ViewDefinition($"{Name}::{name}", type));

            return this;
        }

        /// <summary>
        /// A fluent interface method that adds the definition of a child view to the view definition.
        /// </summary>
        /// <param name="type">The child view type.</param>
        /// <returns>The <see cref="ViewDefinition"/> being configured.</returns>
        public ViewDefinition AddChild(string type)
        {
            definitions.Add(new ViewDefinition(Name, type));

            return this;
        }
    }
}
