using System.ComponentModel;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="CommandManager"/> class.
    /// </summary>
    public class CommandManagerTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [Test]
        public void TestConstructor()
        {
            var manager = new CommandManager();

            Assert.That(manager, Is.Not.Null);
        }

        /// <summary>
        ///  Test that the AddCommand(string, ICommand) method throws an exception when the command has already been added.
        /// </summary>
        [Test]
        public void TestAddExistingCommand()
        {
            var command1 = new MockCommand(new MockReceiver<string>());
            var command2 = new MockCommand(new MockReceiver<string>());

            var manager = new CommandManager();

            manager.AddCommand("command1", command1);

            Assert.Throws<ArgumentException>(() => manager.AddCommand("command1", command2));
        }

        /// <summary>
        /// Test that the GetCommand(string) method works correctly when the specified command has been added.
        /// </summary>
        [Test]
        public void TestGetCommand()
        {
            var command1 = new MockCommand(new MockReceiver<string>());
            var command2 = new MockCommand(new MockReceiver<string>());
            var command3 = new MockCommand(new MockReceiver<string>());

            var manager = new CommandManager();

            manager.AddCommand("command1", command1);
            manager.AddCommand("command2", command2);
            manager.AddCommand("command3", command3);

            Assert.That(manager.GetCommand("command1"), Is.SameAs(command1));
            Assert.That(manager.GetCommand("command2"), Is.SameAs(command2));
            Assert.That(manager.GetCommand("command3"), Is.SameAs(command3));
        }

        /// <summary>
        /// Test that the GetCommand(string) method throws an exception when the specified command has not been added.
        /// </summary>
        [Test]
        public void TestGetMissingCommand()
        {
            var manager = new CommandManager();

            Assert.Throws<ArgumentException>(() => manager.GetCommand("command1"));
        }

        /// <summary>
        /// Test that the GetCommandInvoker(Component) method works correctly when the specified invoker has been registered.
        /// </summary>
        [Test]
        public void TestGetCommandInvoker()
        {
            var manager = new CommandManager();
            var test = new TestInvoker();

            manager.RegisterCommandInvoker(test);

            var invoker = manager.GetCommandInvoker(new Button());

            Assert.That(invoker, Is.Not.Null);
            Assert.That(invoker.Type, Is.EqualTo("System.Windows.Forms.Button"));
            Assert.That(invoker, Is.SameAs(test));
        }

        /// <summary>
        /// Test that the GetCommandInvoker(Component) method throws an exception when the specified command invoker has not been registered.
        /// </summary>
        [Test]
        public void TestGetMissingCommandInvoker()
        {
            var manager = new CommandManager();

            Assert.Throws<KeyNotFoundException>(() => manager.GetCommandInvoker(new Button()));
        }

        /// <summary>
        /// Test that the RegisterCommandInvoker(ICommandInvoker) method works correctly.
        /// </summary>
        [Test]
        public void TestRegisterCommandInvoker()
        {
            var manager = new CommandManager();
            var button = new Button();

            manager.RegisterCommandInvoker(new TestInvoker());

            var invoker = manager.GetCommandInvoker(button);

            Assert.That(invoker, Is.Not.Null);
            Assert.That(invoker.Type, Is.EqualTo("System.Windows.Forms.Button"));
        }

        /// <summary>
        /// Test that the GetCommand(string) method works correctly when the specified command has been added.
        /// </summary>
        [Test]
        public void TestRemoveCommand()
        {
            var manager = new CommandManager();

            manager.AddCommand("C1", new TestCommand(new TestReceiver(), "C1"));
            manager.AddCommand("C2", new TestCommand(new TestReceiver(), "C2"));
            manager.AddCommand("C3", new TestCommand(new TestReceiver(), "C3"));

            var c1 = manager.GetCommand("C1") as TestCommand;
            var c2 = manager.GetCommand("C2") as TestCommand;
            var c3 = manager.GetCommand("C3") as TestCommand;

            Assert.IsNotNull(c1);
            Assert.That(c1.Text, Is.EqualTo("C1"));

            Assert.IsNotNull(c2);
            Assert.That(c2.Text, Is.EqualTo("C2"));

            Assert.IsNotNull(c3);
            Assert.That(c3.Text, Is.EqualTo("C3"));

            manager.RemoveCommand("C2");

            Assert.That(((TestCommand)manager.GetCommand("C1")).Text, Is.EqualTo("C1"));
            Assert.That(((TestCommand)manager.GetCommand("C3")).Text, Is.EqualTo("C3"));

            var pass = false;

            try
            {
                manager.GetCommand("C2");
            }
            catch (ArgumentException e)
            {
                pass = e.Message == "A command named C2 could not be found.";
            }

            Assert.IsTrue(pass);
        }

        /// <summary>
        /// Test that the GetCommand(string) method throws an exception when the specified command has not been added.
        /// </summary>
        [Test]
        public void TestRemoveMissingCommand()
        {
            var manager = new CommandManager();

            Assert.Throws<ArgumentException>(() => manager.RemoveCommand("C1"));
        }

        /// <summary>
        /// A test class that implements the <see cref="ICommandInvoker"/> interface.
        /// </summary>
        private class TestInvoker : ICommandInvoker
        {
            private Component component = new Button();

            public string Type => component.GetType().ToString();

            public void AddInstance(Component component, ICommand command)
            {
                throw new NotImplementedException();
            }

            public void RemoveInstance(Component component)
            {
                throw new NotImplementedException();
            }

            public void UpdateCheckedState(Component component, bool value)
            {
                throw new NotImplementedException();
            }

            public void UpdateEnabledState(Component component, bool value)
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// A test class used to capture the result of executing a command.
        /// </summary>
        private class TestReceiver
        {
            public string Text { get; private set; }

            public void AppendText(string text)
            {
                Text = Text + text;
            }
        }

        /// <summary>
        /// A test class that implements the <see cref="Command&lt;TestReceiver&lt;"/> interface.
        /// </summary>
        private class TestCommand : Command<TestReceiver>
        {
            public TestCommand(TestReceiver receiver, string text)
                : base(receiver)
            {
                Text = text;
            }

            public string Text { get; private set; }

            public override void Execute()
            {
                receiver.AppendText(Text);
            }
        }
    }
}
