using Client.Command;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Forms;

namespace ClientTests
{
    [TestClass]
    public class CommandTests
    {
        private readonly TextBox textBox = new();
        private readonly string roomName = "Room";
        [TestMethod]
        public void TestSendMessageToTextBox()
        {
            string a = "Me: Test\r\n";
            SendMessage sendMessage = new(textBox);
            sendMessage.Send("Test", roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoMessageWhenIndexMoreThanZero()
        {
            string a = "Me: Test\r\n\n";
            SendMessage sendMessage = new(textBox);
            sendMessage.Send("Test", roomName);
            sendMessage.Send("Test2", roomName);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoMessageWhenIndexLessThanZero()
        {
            string a = "";
            SendMessage sendMessage = new(textBox);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestSendEmoteToTextBox()
        {
            string a = "Me: ༼ つ ◕_◕ ༽つ \r\n";
            SendEmote sendMessage = new(textBox);
            sendMessage.Send("", roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoEmoteWhenIndexMoreThanZero()
        {
            string a = "Me: ༼ つ ◕_◕ ༽つ \r\n\n";
            SendEmote sendMessage = new(textBox);
            sendMessage.Send("", roomName);
            sendMessage.Send("", roomName);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }

        [TestMethod]
        public void TestUndoEmoteWhenIndexLessThanZero()
        {
            string a = "";
            SendEmote sendMessage = new(textBox);
            sendMessage.Undo(roomName);
            Assert.AreEqual(a, textBox.Text);
        }
    }
}