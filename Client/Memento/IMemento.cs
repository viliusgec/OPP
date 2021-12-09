using System;

namespace Client.Memento
{
    interface IMemento
    {
        string GetName();

        State.State GetState();

        DateTime GetDate();
    }
}
