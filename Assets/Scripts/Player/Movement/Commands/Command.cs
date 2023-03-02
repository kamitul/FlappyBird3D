namespace Player.Movement.Commands
{
    public abstract class Command<T>
        where T : IPayload
    {
        protected readonly T payload;

        public Command(T payload)
        {
            this.payload = payload;
        }

        public abstract void Execute();
      }

    public interface IPayload { }
}