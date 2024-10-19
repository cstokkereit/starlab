//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using System.Windows.Forms;

//namespace StarLab.Commands
//{
//    /// <summary>
//    /// A class for performing unit tests on the <see cref="ToolStripButtonCommandInvoker"/> class.
//    /// </summary>
//    [TestClass]
//    public class ToolStripButtonCommandInvokerTests
//    {
//        /// <summary>
//        /// Check that the constructor works correctly. 
//        /// </summary>
//        [Test]
//        public void TestContructor()
//        {
//            var invoker = new ToolStripButtonCommandInvoker();

//            Assert.IsNotNull(invoker);
//        }

//        /// <summary>
//        /// Test that the AddInstance(Component, ICommand) method works correctly.
//        /// </summary>
//        [Test]
//        public void TestAddInstance()
//        {
//            var invoker = new ToolStripButtonCommandInvoker();

//            var command = new MockCommand(new MockReceiver<string>());

//            var button = new ToolStripButton();

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
//            var invoker = new ToolStripButtonCommandInvoker();

//            Assert.AreEqual("System.Windows.Forms.ToolStripButton", invoker.Type);
//        }

//        /// <summary>
//        /// Test that the RemoveInstance(Component) method works correctly.
//        /// </summary>
//        [Test]
//        public void TestRemoveInstance()
//        {
//            var invoker = new ToolStripButtonCommandInvoker();

//            var command = new MockCommand(new MockReceiver<string>());

//            var button = new ToolStripButton();

//            invoker.AddInstance(button, command);

//            invoker.RemoveInstance(button);

//            button.PerformClick();

//            Assert.IsFalse(command.ExecuteCalled);
//        }

//        /// <summary>
//        /// Test that the UpdateEnabledState(Component, bool) method works correctly.
//        /// </summary>
//        [Test]
//        public void TestUpdateEnabledState()
//        {
//            var invoker = new ToolStripButtonCommandInvoker();

//            var button = new ToolStripButton();

//            Assert.IsTrue(button.Enabled);

//            invoker.UpdateEnabledState(button, false);

//            Assert.IsFalse(button.Enabled);
//        }

//        /// <summary>
//        /// Test that the UpdateEnabledState(Component, bool) method does not throw an exception.
//        /// </summary>
//        [Test]
//        public void TestUpdateCheckedState()
//        {
//            var invoker = new ToolStripButtonCommandInvoker();

//            var button = new ToolStripButton();

//            Assert.IsFalse(button.Checked);

//            invoker.UpdateCheckedState(button, true);

//            Assert.IsTrue(button.Checked);
//        }
//    }
//}
