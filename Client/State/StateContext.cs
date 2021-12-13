namespace Client.State
{
    internal class StateContext
    {
        private State _state = null;

        public StateContext(State state)
        {
            TransitionTo(state);
        }

        public void TransitionTo(State state)
        {
            _state = state;
            _state.SetContext(this);
        }

        public State GetState()
        {
            return _state;
        }

        public void ChangeToNextState()
        {
            _state.Handle1();
        }

        public string ShowText()
        {
            return _state.Handle2();
        }

        public void SendState(string roomName)
        {
            _state.SendState(roomName);
        }
    }
}
