using System;

namespace FSM
{
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