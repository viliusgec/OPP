using Client.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace ClientTests
{
    [TestClass]
    public class CommandTests
    {
        TextBox textBox = new();
        string roomName = "Room";
        [TestMethod]
        public void TestSendMessageToTextBox()
        {
            var a = "Me: Test\r\n";
            SendMessage sendMessage = new(textBox);
            sendMessage.Send("Test", roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoMessageWhenIndexMoreThanZero()
        {
            var a = "Me: Test\r\n\n";
            SendMessage sendMessage = new(textBox);
            sendMessage.Send("Test", roomName);
            sendMessage.Send("Test2", roomName);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoMessageWhenIndexLessThanZero()
        {
            var a = "";
            SendMessage sendMessage = new(textBox);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestSendEmoteToTextBox()
        {
            var a = "Me: ༼ つ ◕_◕ ༽つ \r\n";
            SendEmote sendMessage = new(textBox);
            sendMessage.Send("", roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoEmoteWhenIndexMoreThanZero()
        {
            var a = "Me: ༼ つ ◕_◕ ༽つ \r\n\n";
            SendEmote sendMessage = new(textBox);
            sendMessage.Send("", roomName);
            sendMessage.Send("", roomName);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoEmoteWhenIndexLessThanZero()
        {
            var a = "";
            SendEmote sendMessage = new(textBox);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }
    }
}