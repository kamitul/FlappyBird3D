using UnityEngine;
using UnityEngine.InputSystem;

namespace Player.Movement.Commands.Swipe
{
    public sealed class KeyboardSwipe : Command<KeyboardSwipe.Payload>, ISwipe
    {
        public KeyboardSwipe(Payload payload) : base(payload) { }

        public override void Execute()
        {
            var newPos = payload.Rigidbody.position;
            var delta = payload.Direction.ReadValue<Vector2>();
            newPos.z = Mathf.Clamp(newPos.z + payload.Speed * -delta.x * Time.fixedDeltaTime, -payload.MaxBounds, payload.MaxBounds);
            payload.Rigidbody.MovePosition(newPos);
        }

        public class Payload : IPayload
        {
            public Payload(Rigidbody rigidbody, InputAction directionAction, float speed, float maxBounds)
            {
                Direction = directionAction;
                Rigidbody = rigidbody;
                Speed = speed;
                MaxBounds = maxBounds;
            }

            public InputAction Direction { get; private set; }
            public Rigidbody Rigidbody { get; private set; }
            public float Speed { get; private set; }
            public float MaxBounds { get; private set; }
        }
    }
}
