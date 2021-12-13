using System;

namespace Client.Memento
{
    internal class ConcreteMemento : IMemento
    {
        private readonly State.State _state;

        private readonly DateTime _date;

        public ConcreteMemento(State.State state)
        {
            _state = state;
            _date = DateTime.Now;
        }

        public State.State GetState()
        {
            return _state;
        }

        public string GetName()
        {
            return $"{_date} / ({_state.GetType().Name})...";
        }

        public DateTime GetDate()
        {
            return _date;
        }
    }
}
