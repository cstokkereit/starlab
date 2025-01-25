using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="ComponentCommand{TReceiver}"/> class.
    /// </summary>
    public class ComponentCommandTests
    {
        private readonly ICommandManager manager = new CommandManager(); // The command manager used to register the command invoker used in the tests.

        /// <summary>
        /// Test that the <see cref="ComponentCommand{IReceiver}(ICommandManager, IReceiver)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var command = new TestCommand(manager, Substitute.For<IReceiver>());

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="ComponentCommand{IReceiver}.AddInstance(Component)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddInstance()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var receiver = Substitute.For<IReceiver>();

            var command = new TestCommand(manager, receiver);

            command.AddInstance(menu);

            menu.PerformClick();

            receiver.Received().Test();
        }

        /// <summary>
        /// Test that the <see cref="ComponentCommand{IReceiver}.Checked"/> property returns the correct state.
        /// </summary>
        [Test]
        public void TestGetChecked()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var command = new TestCommand(manager, Substitute.For<IReceiver>());

            command.AddInstance(menu);

            Assert.That(command.Checked, Is.False);
            Assert.That(menu.Checked, Is.False);
        }

        /// <summary>
        /// Test that the <see cref="ComponentCommand{IReceiver}.Checked"/> property can be set.
        /// </summary>
        [Test]
        public void TestSetChecked()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var command = new TestCommand(manager, Substitute.For<IReceiver>());

            command.AddInstance(menu);

            command.Checked = true;

            Assert.That(command.Checked);
            Assert.That(menu.Checked);
        }

        /// <summary>
        /// Test that the <see cref="ComponentCommand{IReceiver}.Enabled"/> property returns the correct state.
        /// </summary>
        [Test]
        public void TestGetEnabled()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var command = new TestCommand(manager, Substitute.For<IReceiver>());

            command.AddInstance(menu);

            Assert.That(command.Enabled);
            Assert.That(menu.Enabled);
        }

        /// <summary>
        /// Test that the <see cref="ComponentCommand{IReceiver}.Enabled"/> property can be set.
        /// </summary>
        [Test]
        public void TestSetEnabled()
        {
            var menu = new ToolStripMenuItem();

            manager.RegisterCommandInvoker(new TestInvoker());

            var command = new TestCommand(manager, Substitute.For<IReceiver>());

            command.AddInstance(menu);

            command.Enabled = false;

            Assert.That(command.Enabled, Is.False);
            Assert.That(menu.Enabled, Is.False);
        }

        /// <summary>
        /// A derived class used to test the abstract <see cref="ComponentCommand{TReceiver}"/> class.
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

            private void OnClick(object? sender, EventArgs? e)
            {
                if (sender is ToolStripMenuItem item)
                {
                    var command = GetCommandForInstance(item);
                    command.Execute();
                }
            }
        }

        /// <summary>
        /// A test class that implements the abstract <see cref="ComponentCommand{TReceiver}"/> class.
        /// </summary>
        private class TestCommand : ComponentCommand<IReceiver>
        {
            public TestCommand(ICommandManager manager, IReceiver receiver)
                : base(manager, receiver) { }

            public override void Execute()
            {
                receiver.Test();
            }
        }
    }
}
