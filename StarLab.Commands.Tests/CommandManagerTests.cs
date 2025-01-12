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
            var command1 = Substitute.For<ICommand>();
            var command2 = Substitute.For<ICommand>();

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
            var command1 = Substitute.For<ICommand>();
            var command2 = Substitute.For<ICommand>();
            var command3 = Substitute.For<ICommand>();

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

            var invoker1 = Substitute.For<ICommandInvoker>();
            invoker1.Type.Returns(typeof(Button).ToString());
            manager.RegisterCommandInvoker(invoker1);

            var invoker2 = Substitute.For<ICommandInvoker>();
            invoker2.Type.Returns(typeof(ToolStripMenuItem).ToString());
            manager.RegisterCommandInvoker(invoker2);

            var invoker3 = manager.GetCommandInvoker(new ToolStripMenuItem());

            Assert.That(invoker3, Is.Not.Null);
            Assert.That(invoker3, Is.SameAs(invoker2));
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

            var invoker1 = Substitute.For<ICommandInvoker>();
            invoker1.Type.Returns(typeof(Button).ToString());

            manager.RegisterCommandInvoker(invoker1);

            var invoker2 = manager.GetCommandInvoker(new Button());

            Assert.That(invoker2, Is.Not.Null);
            Assert.That(invoker2, Is.SameAs(invoker1));
        }

        /// <summary>
        /// Test that the RemoveCommand(string) method works correctly.
        /// </summary>
        [Test]
        public void TestRemoveCommand()
        {
            var manager = new CommandManager();

            var command1 = Substitute.For<ICommand>();
            var command2 = Substitute.For<ICommand>();
            var command3 = Substitute.For<ICommand>();

            manager.AddCommand("C1", command1);
            manager.AddCommand("C2", command2);
            manager.AddCommand("C3", command3);

            manager.RemoveCommand("C2");

            Assert.That(manager.GetCommand("C1"), Is.SameAs(command1));
            Assert.That(manager.GetCommand("C3"), Is.SameAs(command3));

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
    }
}
