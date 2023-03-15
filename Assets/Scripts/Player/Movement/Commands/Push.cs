using UnityEngine;

namespace Player.Movement.Commands
{
    public sealed class Push : Command<Push.Payload>
    {
        private readonly float beginSpeed;

        public Push(Payload payload) : base(payload) 
        { 
            beginSpeed = payload.Speed; 
        }

        public override void Execute()
        {
            var newPos = payload.Rigidbody.position;
            newPos.x += payload.Speed * Time.fixedDeltaTime;
            payload.Rigidbody.MovePosition(newPos);
        }

        public void Accelerate()
        {
            payload.Speed += payload.Acceleration;
        }

        public void Reset()
        {
            payload.Speed = beginSpeed;
        }

        public class Payload : IPayload
        {
            public Payload(Rigidbody rgy, float speed, float acceleration)
            {
                Rigidbody = rgy;
                Speed = speed;
                Acceleration = acceleration;
            }

            public Rigidbody Rigidbody { get; private set; }
            public float Speed { get; set; }
            public float Acceleration { get; private set; }
        }
    }
}
