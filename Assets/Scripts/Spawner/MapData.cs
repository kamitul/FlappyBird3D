namespace Spawner
{
    public struct MapData
    {
        public int ObstaclesPassed;

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
