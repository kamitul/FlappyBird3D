using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Map;
using Obstacles;

namespace Services.Assets
{
    public abstract class AssetService : IService
    {
        public abstract UniTask Download();
        public abstract List<Tile> GetTiles();
        public abstract List<Obstacle> GetObstacles();
    }
}
