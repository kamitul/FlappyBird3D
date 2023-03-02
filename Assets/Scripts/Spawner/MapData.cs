namespace Spawner
{
    public class MapData
    {
        public int ObstaclesPassed { get; private set; }

        public MapData(int obstaclesPassed)
        {
            ObstaclesPassed = obstaclesPassed;
        }

        public void Increment()
        {
            ObstaclesPassed++;
        }

        public void Reset()
        {
            ObstaclesPassed = 0;
        }
    }
}
