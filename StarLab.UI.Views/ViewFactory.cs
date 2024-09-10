using StarLab.Presentation;
using StarLab.Presentation.Docking;
using StarLab.Presentation.Model;
using StarLab.Shared.Properties;

namespace StarLab.UI
{
    public class ViewFactory : IViewFactory
    {
        private readonly ViewTypeResolver types = new ViewTypeResolver();

        private readonly IPresenterFactory presenterFactory;

        public ViewFactory(IPresenterFactory presenterFactory)
        {
            this.presenterFactory = presenterFactory;
        }

        #region IViewFactory Members

        public IDockableView CreateView(IViewContext context)
        {
            IDockableView? view = null;

            var typeName = types.Resolve(context.View);

            var type = Type.GetType(typeName);

            if (type != null)
            {
                var content = CreateContent(context.Content);

                view = Activator.CreateInstance(type, new object[] { context, content, presenterFactory }) as IDockableView;
            }

            if (view == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return view;
        }

        public IFormView CreateView(string name)
        {
            IFormView? view = null;

            var typeName = types.Resolve(name);

            var type = Type.GetType(typeName);

            if (type != null)
            {
                view = Activator.CreateInstance(type, new object[] { presenterFactory }) as IFormView;
            }

            if (view == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return view;
        }

        #endregion

        private IControlView CreateContent(string typeName, IContent content)
        {
            IControlView? view = null;

            var type = Type.GetType(typeName);

            if (type != null)
            {
                view = Activator.CreateInstance(type, new object[] { content, presenterFactory }) as IControlView;
            }

            if (view == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return view;
        }

        private IControlView CreateContent(string typeName)
        {
            IControlView? view = null;

            var type = Type.GetType(typeName);

            if (type != null)
            {
                view = Activator.CreateInstance(type, new object[] { presenterFactory }) as IControlView;
            }

            if (view == null)
            {
                throw new Exception(string.Format(Resources.MessageCouldNotBeCreated, typeName));
            }

            return view;
        }

        private IControlView CreateContent(IContent content)
        {
            IControlView? view = null;

            var typeName = types.Resolve(content.View);

            view = CreateContent(typeName);

            if (view is ISplitView splitView)
            {
                foreach (var childContent in content.Contents)
                {
                    splitView.AddChild(CreateContent(childContent), childContent.Panel);
                }
            }

            return view;
        }
    }
}
