using System;

namespace FSM
{
    public class State<T>
        where T : Enum
    {
        private readonly Payload<T> payload;
        public T ID => payload.ID;

        public State(Payload<T> payload)
        {
            this.payload = payload;
        }

        public virtual void Enter()
        {
            payload.OnEntered?.Invoke();
        }

        public virtual void Exit()
        {
            payload.OnExit?.Invoke();
        }
    }
}