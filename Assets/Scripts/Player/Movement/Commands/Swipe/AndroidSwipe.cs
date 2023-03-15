using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement.Commands.Swipe
{
    public sealed class AndroidSwipe : Command<AndroidSwipe.Payload>, ISwipe, IDisposable
    {
        private Vector2 initialPosition;
        private Vector2 delta;

        public AndroidSwipe(Payload payload) : base(payload) 
        {
            payload.TouchAction.performed += OnSwipeStarted;
            payload.TouchAction.canceled += OnSwipeEnded;
        }

        private void OnSwipeStarted(InputAction.CallbackContext obj)
        {
            initialPosition = payload.PositionAction.ReadValue<Vector2>();
        }

        private void OnSwipeEnded(InputAction.CallbackContext obj)
        {
            delta = payload.PositionAction.ReadValue<Vector2>() - initialPosition;
        }

        public override void Execute()
        {
            if (delta.x == 0) return;

            var newPos = payload.Rigidbody.position;
            payload.StartLaneIndex = Mathf.Clamp(delta.x < 0 ? ++payload.StartLaneIndex : --payload.StartLaneIndex, 0, payload.Lanes.Length - 1);
            newPos.z = payload.Lanes[payload.StartLaneIndex];
            delta = Vector2.zero;

            payload.Rigidbody.MovePosition(newPos);
        }

        public void Dispose()
        {
            payload.TouchAction.performed -= OnSwipeStarted;
            payload.TouchAction.canceled -= OnSwipeEnded;
        }

        public class Payload : IPayload
        {
            public Payload(Rigidbody rigidbody, InputAction touchAction, InputAction positionAction, float[] lanes, int startLaneIndex)
            {
                TouchAction = touchAction;
                PositionAction = positionAction;
                Rigidbody = rigidbody;
                Lanes = lanes;
                StartLaneIndex = startLaneIndex;
            }

            public InputAction TouchAction { get; private set; }
            public InputAction PositionAction { get; private set; }
            public Rigidbody Rigidbody { get; private set; }
            public float[] Lanes { get; private set; }
            public int StartLaneIndex { get; set; }
        }
    }
}
