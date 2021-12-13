namespace Client.Command
{
    internal interface ICommand
    {
        public void Send(string message, string room);
        public void Undo(string room);
    }
}
