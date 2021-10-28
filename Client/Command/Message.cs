using Client.Observer;
using System.Windows.Forms;

namespace Client.Command
{
    class Message : IMessage
    {
        public TextBox TextBox;
        private string myLastMessage;
        private readonly ServerObserver observer = new();
        public Message(TextBox textBox)
        {
            TextBox = textBox;
        }

        public void SendMessage(string message)
        {
            myLastMessage = message;
            observer.SendMessage(message);
            TextBox.AppendText("Me: " + message + "\r\n");
        }

        public void UndoMessage()
        {
            observer.UndoMessage(myLastMessage);
            TextBox.Text = TextBox.Text.Replace("Me: " + myLastMessage + "\r\n", "");
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
