using System;
using System.Collections.Generic;
using UnityEngine;

namespace FSM
{
    public abstract class StateMachine<T> : MonoBehaviour
        where T : Enum
    {
        protected State<T> state;
        public Dictionary<T, State<T>> States { get; private set; }

        protected virtual void Awake()
        {
            States = Initalize();
        }

        public abstract Dictionary<T, State<T>> Initalize();

        public T GetState() => state.ID;

        public void SetState(T state)
        {
            if(States.TryGetValue(state, out var newState))
            {
                if (this.state != null)
                {
                    this.state.Exit();
                }

                if (newState != null)
                {
                    newState.Enter();
                }

                this.state = newState;
            }
            else
            {
                Debug.LogError($"No state for type: {state}");
            }
        }
    }
}
