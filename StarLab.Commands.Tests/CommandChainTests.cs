using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="CommandChain"/> class.
    /// </summary>
    public class CommandChainTests
    {
        private readonly ICommandManager manager = new CommandManager(); // The command manager used to register the command invoker used in the tests.

        /// <summary>
        /// Test that the <see cref="CommandChain()"> constructor works.
        /// </summary>
        [Test]
        public void TestDefaultConstructor()
        {
            var command = new CommandChain();

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="CommandChain(ICommandManager)"/> constructor works correctly.
        /// </summary>
        [Test]
        public void TestManagerConstructor()
        {
            var command = new CommandChain(manager);

            Assert.That(command, Is.Not.Null);
        }

        /// <summary>
        /// Test that the <see cref="CommandChain.AddInstance(Component)"/> method works correctly.
        /// </summary>
        [Test]
        public void TestAddInstance()
        {
            manager.RegisterCommandInvoker(new TestInvoker());

            var receiver = Substitute.For<IReceiver<string>>();
            var command = new CommandChain(manager);
            var menu = new ToolStripMenuItem();

            command.AddInstance(menu);

            command.Add(new TestCommand(receiver, "This"));
            command.Add(new TestCommand(receiver, "is"));
            command.Add(new TestCommand(receiver, "a"));
            command.Add(new TestCommand(receiver, "test."));

            menu.PerformClick();

            receiver.Received(1).Test("This");
            receiver.Received(1).Test("is");
            receiver.Received(1).Test("a");
            receiver.Received(1).Test("test.");
        }

        /// <summary>
        /// Test that the <see cref="CommandChain.Execute()"/> method works correctly when the <see cref="CommandChain"/> contains multiple commands.
        /// </summary>
        [Test]
        public void TestExecuteCommandChain()
        {
            var receiver = Substitute.For<IReceiver<string>>();

            var command = new CommandChain();

            command.Add(new TestCommand(receiver, "This"));
            command.Add(new TestCommand(receiver, "is"));
            command.Add(new TestCommand(receiver, "a"));
            command.Add(new TestCommand(receiver, "test."));

            command.Execute();

            receiver.Received(1).Test("This");
            receiver.Received(1).Test("is");
            receiver.Received(1).Test("a");
            receiver.Received(1).Test("test.");
        }

        /// <summary>
        /// A derived class used to test the <see cref="CommandChain"/> class.
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
        /// A derived class used to test the <see cref="CommandChain"/> class.
        /// </summary>
        private class TestCommand : Command<IReceiver<string>>
        {
            private string text;

            public TestCommand(IReceiver<string> receiver, string text)
                : base(receiver) 
            {
                this.text = text;
            }

            public override void Execute()
            {
                receiver.Test(text);
            }
        }
    }
}
