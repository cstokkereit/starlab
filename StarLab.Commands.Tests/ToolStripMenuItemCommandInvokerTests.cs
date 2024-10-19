//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Windows.Forms;

//namespace StarLab.Commands
//{
//    /// <summary>
//    /// A class for performing unit tests on the <see cref="ToolStripMenuItemCommandInvoker"/> class.
//    /// </summary>
//    [TestClass]
//    public class ToolStripMenuItemCommandInvokerTests
//    {
//        /// <summary>
//        /// Check that the constructor works correctly. 
//        /// </summary>
//        [Test]
//        public void TestContructor()
//        {
//            var invoker = new ToolStripMenuItemCommandInvoker();

//            Assert.IsNotNull(invoker);
//        }

//        /// <summary>
//        /// Test that the AddInstance(Component, ICommand) method works correctly.
//        /// </summary>
//        [Test]
//        public void TestAddInstance()
//        {
//            var invoker = new ToolStripMenuItemCommandInvoker();

//            var command = new MockCommand(new MockReceiver<string>());

//            var button = new ToolStripMenuItem();

//            invoker.AddInstance(button, command);

//            button.PerformClick();

//            Assert.IsTrue(command.ExecuteCalled);
//        }

//        /// <summary>
//        /// Test that the Type property returns the correct type name.
//        /// </summary>
//        [Test]
//        public void TestGetType()
//        {
//            var invoker = new ToolStripMenuItemCommandInvoker();

//            Assert.AreEqual("System.Windows.Forms.ToolStripMenuItem", invoker.Type);
//        }

//        /// <summary>
//        /// Test that the RemoveInstance(Component) method works correctly.
//        /// </summary>
//        [Test]
//        public void TestRemoveInstance()
//        {
//            var invoker = new ToolStripMenuItemCommandInvoker();

//            var command = new MockCommand(new MockReceiver<string>());

//            var menu = new ToolStripMenuItem();

//            invoker.AddInstance(menu, command);

//            invoker.RemoveInstance(menu);

//            menu.PerformClick();

//            Assert.IsFalse(command.ExecuteCalled);
//        }

//        /// <summary>
//        /// Test that the UpdateEnabledState(Component, bool) method works correctly.
//        /// </summary>
//        [Test]
//        public void TestUpdateEnabledState()
//        {
//            var invoker = new ToolStripMenuItemCommandInvoker();

//            var menu = new ToolStripMenuItem();

//            Assert.IsTrue(menu.Enabled);

//            invoker.UpdateEnabledState(menu, false);

//            Assert.IsFalse(menu.Enabled);
//        }

//        /// <summary>
//        /// Test that the UpdateEnabledState(Component, bool) method does not throw an exception.
//        /// </summary>
//        [Test]
//        public void TestUpdateCheckedState()
//        {
//            var invoker = new ToolStripMenuItemCommandInvoker();

//            var menu = new ToolStripMenuItem();

//            Assert.IsFalse(menu.Checked);

//            invoker.UpdateCheckedState(menu, true);

//            Assert.IsTrue(menu.Checked);
//        }
//    }
//}
