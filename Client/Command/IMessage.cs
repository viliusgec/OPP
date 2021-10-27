namespace Client.Command
{
    interface IMessage
    {
        public void SendMessage(string message);
        public void UndoMessage();
    }
}
