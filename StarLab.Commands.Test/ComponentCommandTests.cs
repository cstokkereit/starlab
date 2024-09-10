using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ComponentCommand&lt;TReceiver&gt;"/> class.
    /// </summary>
    [TestClass]
    public class ComponentCommandTests
    {
        private readonly ICommandManager manager = new CommandManager(); // The command manager used to register the command invoker used in the tests.

        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            var command = new TestCommand(manager, new MockReceiver<string>());

            Assert.IsNotNull(command);
        }

        /// <summary>
        /// Test that the AddInstance(Component) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestAddInstance()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var receiver = new MockReceiver<string>();

            var command = new TestCommand(manager, receiver);

            command.AddInstance(menu);

            menu.PerformClick();

            Assert.IsTrue(receiver.TestCalled);
        }

        /// <summary>
        /// Test that the Checked property returns the correct state.
        /// </summary>
        [TestMethod]
        public void TestGetChecked()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var receiver = new MockReceiver<string>();

            var command = new TestCommand(manager, receiver);

            command.AddInstance(menu);

            Assert.IsFalse(command.Checked);
            Assert.IsFalse(menu.Checked);
        }

        /// <summary>
        /// Test that the Checked property can be set.
        /// </summary>
        [TestMethod]
        public void TestSetChecked()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var receiver = new MockReceiver<string>();

            var command = new TestCommand(manager, receiver);

            command.AddInstance(menu);

            command.Checked = true;

            Assert.IsTrue(command.Checked);
            Assert.IsTrue(menu.Checked);
        }

        /// <summary>
        /// Test that the Enabled property returns the correct state.
        /// </summary>
        [TestMethod]
        public void TestGetEnabled()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var receiver = new MockReceiver<string>();

            var command = new TestCommand(manager, receiver);

            command.AddInstance(menu);

            Assert.IsTrue(command.Enabled);
            Assert.IsTrue(menu.Enabled);
        }

        /// <summary>
        /// Test that the Enabled property can be set.
        /// </summary>
        [TestMethod]
        public void TestSetEnabled()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var receiver = new MockReceiver<string>();

            var command = new TestCommand(manager, receiver);

            command.AddInstance(menu);

            command.Enabled = false;

            Assert.IsFalse(command.Enabled);
            Assert.IsFalse(menu.Enabled);
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="ComponentCommand&lt;TReceiver&gt;"/> class.
        /// </summary>
        private class TestInvoker : CommandInvoker<ToolStripMenuItem>
        {
            public override void AddInstance(Component component, ICommand command)
            {
                base.AddInstance(component, command);
                var menu = Cast(component);
                menu.Click += OnClick;
            }

            public override void RemoveInstance(Component component)
            {
                base.RemoveInstance(component);
                var menu = Cast(component);
                menu.Click -= OnClick;
            }

            public override void UpdateCheckedState(Component component, bool value)
            {
                var menu = Cast(component);
                menu.Checked = value;
            }

            public override void UpdateEnabledState(Component component, bool value)
            {
                var menu = Cast(component);
                menu.Enabled = value;
            }

            private void OnClick(object sender, EventArgs e)
            {
                var command = GetCommandForInstance(sender as ToolStripMenuItem);
                command.Execute();
            }
        }

        /// <summary>
        /// A test class that implements the abstract <see cref="ComponentCommand&lt;TReceiver&gt;"/> class.
        /// </summary>
        private class TestCommand : ComponentCommand<MockReceiver<string>>
        {
            public TestCommand(ICommandManager manager, MockReceiver<string> receiver)
                : base(manager, receiver) { }

            public override void Execute()
            {
                receiver.Test();
            }
        }
    }
}
