using UnityEngine;
using Pooling;
using Obstacles;
using Spawner.Factories.Tiles;

namespace Spawner.Factories.Obstacles
{
    public sealed class EndlessObstacleFactory : IObstacleFactory
    {
        private readonly Payload payload;

        public EndlessObstacleFactory(Payload payload)
        {
            this.payload = payload;
        }

        public IObstacle Create(ITile tile)
        {
            int random = Random.Range(0, payload.ObstaclePools.Length);
            var obj = payload.ObstaclePools[random].GetObject(true);
            if (obj == null) return null;

            Vector3 newPos = obj.IsRandomizable ? RandomizePosition(tile) : tile.GetPosition();
            obj.Initialize(newPos);
            return obj;
        }

        private Vector3 RandomizePosition(ITile tile)
        {
            var pos = tile.GetPosition();
            var ext = tile.GetObstacleArea();

            int random = Random.Range(0, payload.Lines.Length - 1);
            return new Vector3(Random.Range(pos.x - ext.x, pos.x + ext.x), 0f, payload.Lines[random]);
        }

        public void Reset()
        {
            
        }

        public class Payload
        {
            public Payload(Pool<Obstacle>[] obstaclePools, float[] lines)
            {
                ObstaclePools = obstaclePools;
                Lines = lines;
            }

            public Pool<Obstacle>[] ObstaclePools { get; private set; }
            public float[] Lines { get; private set; }
        }
    }
}
