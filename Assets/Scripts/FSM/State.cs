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

    public class Payload<T>
        where T : Enum
    {
        public Payload(T id, Action onEntered = null, Action onExit = null)
        {
            ID = id;
            OnEntered = onEntered;
            OnExit = onExit;
        }

        public T ID { get; private set; }
        public Action OnEntered { get; private set; }
        public Action OnExit { get; private set; }
    }
}