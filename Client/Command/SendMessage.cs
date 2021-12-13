using Client.Observer;
using System.Windows.Forms;

namespace Client.Command
{
    public class SendMessage : ICommand
    {
        public TextBox TextBox;
        private string myLastMessage;
        private readonly ServerObserver observer = new();
        public SendMessage(TextBox textBox)
        {
            TextBox = textBox;
        }

        public void Send(string message, string room)
        {
            myLastMessage = message;
            observer.SendMessage(message, room);
            TextBox.AppendText("Me: " + message + "\r\n");
        }

        public void Undo(string room)
        {
            observer.UndoMessage(room);
            int index = TextBox.Text.LastIndexOf("Me: ");
            if (index >= 0)
            {
                int endIndex = TextBox.Text.IndexOf("\r\n", index);
                TextBox.Text = TextBox.Text.Remove(index, endIndex - index + 1);
            }
        }

        public string SeeText(string text)
        {
            TextBox.Text = text;
            return TextBox.Text;
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
