using System;

namespace Client.Memento
{
    internal class Originator
    {
        public State.State _state = null;

        public Originator(State.State state)
        {
            _state = state;
        }

        public IMemento Save()
        {
            return new ConcreteMemento(_state);
        }

        public void SetState(State.State state)
        {
            _state = state;
        }

        // Restores the Originator's state from a memento object.
        public void Restore(IMemento memento)
        {
            if (!(memento is ConcreteMemento))
            {
                throw new Exception("Unknown memento class " + memento.ToString());
            }

            _state = memento.GetState();
        }
    }
}
