namespace StarLab.Presentation
{
    /// <summary>
    /// Repesents the parameter values required to create a view.
    /// </summary>
    public interface IViewDefinition
    {
        /// <summary>
        /// Gets an <see cref="IReadOnlyList{IViewDefinition}"/> containing the child view definitions.
        /// </summary>
        IReadOnlyList<IViewDefinition> ChildViews { get; }

        /// <summary>
        /// Gets the view name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the ID of the panel that will be used to display the child view.
        /// </summary>
        int Panel { get; }

        /// <summary>
        /// Gets the name of the class that implements the view.
        /// </summary>
        string TypeName { get; }

        /// <summary>
        /// Gets the view type.
        /// </summary>
        ViewTypes ViewType { get; }
    }
}
