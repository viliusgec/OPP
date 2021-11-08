namespace Client.Command
{
    interface ICommand
    {
        public void Send(string message);
        public void Undo();
    }
}
