using System;

namespace Client.Memento
{
    internal interface IMemento
    {
        string GetName();

        State.State GetState();

        DateTime GetDate();
    }
}
