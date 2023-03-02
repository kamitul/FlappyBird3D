using Spawner.Factories.Tiles;

namespace Spawner.Factories.Obstacles
{
    public interface IObstacleFactory
    {
        public IObstacle Create(ITile tile);
        public void Reset();
    }
}

