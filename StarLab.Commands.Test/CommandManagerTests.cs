using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace StarLab.Commands
{
    /// <summary>
    /// A class for performing unit tests on the <see cref="CommandManager"/> class.
    /// </summary>
    [TestClass]
    public class CommandManagerTests
    {
        /// <summary>
        /// Test that the constructor works correctly.
        /// </summary>
        [TestMethod]
        public void TestConstructor()
        {
            var manager = new CommandManager();

            Assert.IsNotNull(manager);
        }

        /// <summary>
        ///  Test that the AddCommand(string, ICommand) method throws an exception when the command has already been added.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestAddExistingCommand()
        {
            var command1 = new MockCommand(new MockReceiver<string>());
            var command2 = new MockCommand(new MockReceiver<string>());

            var manager = new CommandManager();

            manager.AddCommand("command1", command1);
            manager.AddCommand("command1", command2);
        }

        /// <summary>
        /// Test that the GetCommand(string) method works correctly when the specified command has been added.
        /// </summary>
        [TestMethod]
        public void TestGetCommand()
        {
            var command1 = new MockCommand(new MockReceiver<string>());
            var command2 = new MockCommand(new MockReceiver<string>());
            var command3 = new MockCommand(new MockReceiver<string>());

            var manager = new CommandManager();

            manager.AddCommand("command1", command1);
            manager.AddCommand("command2", command2);
            manager.AddCommand("command3", command3);

            Assert.AreSame(command1, manager.GetCommand("command1"));
            Assert.AreSame(command2, manager.GetCommand("command2"));
            Assert.AreSame(command3, manager.GetCommand("command3"));
        }

        /// <summary>
        /// Test that the GetCommand(string) method throws an exception when the specified command has not been added.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestGetMissingCommand()
        {
            var manager = new CommandManager();

            manager.GetCommand("command1");
        }

        /// <summary>
        /// Test that the GetCommandInvoker(Component) method works correctly when the specified invoker has been registered.
        /// </summary>
        [TestMethod]
        public void TestGetCommandInvoker()
        {
            var manager = new CommandManager();
            var test = new TestInvoker();

            manager.RegisterCommandInvoker(test);

            var invoker = manager.GetCommandInvoker(new Button());

            Assert.IsNotNull(invoker);
            Assert.AreEqual("System.Windows.Forms.Button", invoker.Type);
            Assert.AreSame(test, invoker);
        }

        /// <summary>
        /// Test that the GetCommandInvoker(Component) method throws an exception when the specified command invoker has not been registered.
        /// </summary>
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void TestGetMissingCommandInvoker()
        {
            var manager = new CommandManager();
            manager.GetCommandInvoker(new Button());
        }

        /// <summary>
        /// Test that the RegisterCommandInvoker(ICommandInvoker) method works correctly.
        /// </summary>
        [TestMethod]
        public void TestRegisterCommandInvoker()
        {
            var manager = new CommandManager();
            var button = new Button();

            manager.RegisterCommandInvoker(new TestInvoker());

            var invoker = manager.GetCommandInvoker(button);

            Assert.IsNotNull(invoker);
            Assert.AreEqual("System.Windows.Forms.Button", invoker.Type);
        }

        /// <summary>
        /// Test that the GetCommand(string) method works correctly when the specified command has been added.
        /// </summary>
        [TestMethod]
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
            Assert.AreEqual("C1", c1.Text);

            Assert.IsNotNull(c2);
            Assert.AreEqual("C2", c2.Text);

            Assert.IsNotNull(c3);
            Assert.AreEqual("C3", c3.Text);

            manager.RemoveCommand("C2");

            Assert.AreEqual("C1", ((TestCommand)manager.GetCommand("C1")).Text);
            Assert.AreEqual("C3", ((TestCommand)manager.GetCommand("C3")).Text);

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
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestRemoveMissingCommand()
        {
            var manager = new CommandManager();
            manager.RemoveCommand("C1");
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
