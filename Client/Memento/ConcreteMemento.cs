using System;

namespace Client.Memento
{
    class ConcreteMemento : IMemento
    {
        private State.State _state;

        private DateTime _date;

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
