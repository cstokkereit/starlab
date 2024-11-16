using StarLab.Shared.Properties;

namespace StarLab.Application
{
    public class Factory
    {
        protected object CreateInstance(string typeName, params object?[]? args)
        {
            object? view = null;

            var type = Type.GetType(typeName);

            if (type != null)
                view = Activator.CreateInstance(type, args);

            if (view == null)
                throw new Exception(string.Format(Resources.CouldNotBeCreatedMessage, typeName));

            return view;
        }
    }
}
