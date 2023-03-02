using UnityEngine;

namespace Player.Movement.Commands
{
    public class Jump : Command<Jump.Payload>
    {
        public Jump(Payload payload) : base(payload) { }

        public override void Execute()
        {
            var position = payload.Rigidbody.position;
            var distance = payload.MaxBounds - position.y;
            payload.Rigidbody.velocity = Vector3.zero;
            payload.Rigidbody.AddForce(Vector3.up * (distance < 0 ? 0 : payload.Force), ForceMode.Impulse);
        }

        public class Payload : IPayload
        {
            public Payload(Rigidbody rigidbody, float maxBounds, float force)
            {
                Rigidbody = rigidbody;
                MaxBounds = maxBounds;
                Force = force;
            }

            public Rigidbody Rigidbody { get; private set; }
            public float MaxBounds { get; private set; }
            public float Force { get; private set; }
        }
    }
}