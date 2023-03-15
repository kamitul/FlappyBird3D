using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Map;
using Obstacles;

namespace Services.Assets
{
    public interface IAssetService : IService
    {
        public UniTask Download();
        public List<Tile> GetTiles();
        public List<Obstacle> GetObstacles();
    }
}
