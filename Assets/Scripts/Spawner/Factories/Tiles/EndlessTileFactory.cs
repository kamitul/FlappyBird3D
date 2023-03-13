using UnityEngine;
using Pooling;
using Map;

namespace Spawner.Factories.Tiles
{
    public class EndlessTileFactory : ITileFactory
    {
        private readonly Payload payload;

        private int multiplierXPosition = 0;

        public EndlessTileFactory(Payload payload)
        {
            this.payload = payload;
        }

        public ITile Create()
        {
            int random = Random.Range(0, payload.TilePools.Length);
            var obj = payload.TilePools[random].GetObject(true);
            if (obj == null) return null;

            var newPos = Vector3.zero + new Vector3((multiplierXPosition++ * obj.Bounds.extents.x * 2), 0, 0);
            obj.SetPosition(newPos);
            return obj;
        }

        public void Reset()
        {
            multiplierXPosition = 0;
        }

        public class Payload
        {
            public Payload(Pool<Tile>[] tilePools)
            {
                TilePools = tilePools;
            }

            public Pool<Tile>[] TilePools { get; private set; }   
        }
    }
}
