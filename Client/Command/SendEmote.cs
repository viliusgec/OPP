using System;
using Client.Observer;
using System.Windows.Forms;

namespace Client.Command
{
    public class SendEmote : ICommand
    {
        public TextBox TextBox;
        private readonly ServerObserver observer = new();
        public SendEmote(TextBox textBox)
        {
            TextBox = textBox;
        }

        public void Send(string message, string room)
        {
            observer.SendMessage("༼ つ ◕_◕ ༽つ",room);
            TextBox.AppendText("Me: ༼ つ ◕_◕ ༽つ \r\n");
        }

        public void Undo(string room)
        {
            observer.UndoMessage(room);
            var index = TextBox.Text.LastIndexOf("Me: ");
            if (index >= 0)
            {
                var endIndex = TextBox.Text.IndexOf("\r\n", index);
                TextBox.Text = TextBox.Text.Remove(index, endIndex - index + 1);
            }
        }

        public void RecieveMessage()
        {
            observer.ReceiveMessage(TextBox);
        }

        public void ReceiveUndoMessage()
        {
            observer.ReceiveUndoMessage(TextBox);
        }
    }
}
