using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client.Command;
using System.Windows.Forms;

namespace ClientTests
{
    [TestClass]
    public class CommandTests
    {
        TextBox textBox = new();

        [TestMethod]
        public void TestSendMessageToTextBox()
        {
            var a = "Me: Test\r\n";
            SendMessage sendMessage = new (textBox);
            sendMessage.Send("Test");
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoMessageWhenIndexMoreThanZero()
        {
            var a = "Me: Test\r\n\n";
            SendMessage sendMessage = new(textBox);
            sendMessage.Send("Test");
            sendMessage.Send("Test2");
            sendMessage.Undo();
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoMessageWhenIndexLessThanZero()
        {
            var a = "";
            SendMessage sendMessage = new(textBox);
            sendMessage.Undo();
            Assert.AreEqual(a, textBox.Text);
        }
    }
}
