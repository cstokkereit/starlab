namespace StarLab.Application
{
    public interface ISplitViewContent
    {
        void AttachCommands(IApplicationController controller, ISplitViewController viewController);
    }
}
