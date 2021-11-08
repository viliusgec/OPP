using Client.Observer;
using System.Windows.Forms;

namespace Client.Command
{
    class SendMessage : ICommand
    {
        public TextBox TextBox;
        private string myLastMessage;
        private readonly ServerObserver observer = new();
        public SendMessage(TextBox textBox)
        {
            TextBox = textBox;
        }

        public void Send(string message)
        {
            myLastMessage = message;
            observer.SendMessage(message);
            TextBox.AppendText("Me: " + message + "\r\n");
        }

        public void Undo()
        {
            observer.UndoMessage();
            var index = TextBox.Text.LastIndexOf("Me: ");
            if(index >= 0)
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
