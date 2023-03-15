using UnityEngine;
using Pooling;
using Map;
using System.Linq;
using Logic;
using Config;
using Spawner.Factories.Tiles;
using Obstacles;
using System.Collections.Generic;
using Services.Assets;
using System;
using Utils;
using Spawner.Factories.Obstacles;

namespace Spawner
{
    [DisallowMultipleComponent()]
    public sealed class MapController : MonoBehaviour, IObservable<MapData>
    {
        private ITileFactory tileFactory;
        private IObstacleFactory obstacleFactory;

        private MapData data;
        private MapConfig mapConfig;

        private List<IObserver<MapData>> observers;

        public List<ITile> SpawnedTiles { get; private set; } = new List<ITile>();

        private void Awake()
        {
            observers = new List<IObserver<MapData>>();
            data = new MapData(0);
        }

        public void Initialize()
        {
            mapConfig = Configuration.GetConfig<MapConfig>();
            var tiles = GameServices.GetService<OnlineAssetService>().GetTiles();
            var obstacles = GameServices.GetService<OnlineAssetService>().GetObstacles();

            tileFactory = new EndlessTileFactory(new EndlessTileFactory.Payload(tiles.Select(x => new Pool<Tile>(x, mapConfig.MAX_VISIBILE_TILES, transform)).ToArray()));
            obstacleFactory = new EndlessObstacleFactory(new EndlessObstacleFactory.Payload(obstacles.Select(x => new Pool<Obstacle>(x,
                mapConfig.MAX_VISIBILE_TILES * mapConfig.MAX_OBSTACLES, transform)).ToArray(), mapConfig.ZLines));
        }

        private void Subscribe(ITile tile)
        {
            tile.OnTilePassed += OnTilePassed;
            tile.OnObstaclePassed += OnObstaclePassed;
        }

        private void OnObstaclePassed(IObstacle obj)
        {
            data.Increment();
            Notify(in data);
        }

        private void OnTilePassed(ITile tile)
        {
            Unsubscribe(tile);
            SpawnTile();
        }

        private void SpawnTile()
        {
            var newTile = tileFactory.Create();
            if (newTile == null) return;

            List<IObstacle> obstacles = new List<IObstacle>();
            var count = UnityEngine.Random.Range(1, mapConfig.MAX_OBSTACLES + 1);

            for(int i = 0; i < count; ++i)
            {
                var obstacle = obstacleFactory.Create(newTile);
                if (obstacle == null) return;

                obstacles.Add(obstacle);
            }
            newTile.SetObstacles(obstacles);

            AddTile(newTile);
            Subscribe(newTile);
        }

        private void Unsubscribe(ITile tile)
        {
            tile.OnTilePassed -= OnTilePassed;
            tile.OnObstaclePassed -= OnObstaclePassed;
        }

        private void AddTile(ITile newTile)
        {
            if (!SpawnedTiles.Contains(newTile))
                SpawnedTiles.Add(newTile);
        }

        public IDisposable Subscribe(IObserver<MapData> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Disposer<MapData>(observers, observer);
        }

        private void Notify(in MapData data)
        {
            foreach (var observer in observers)
                observer.OnNext(data);
        }

        public void Reset()
        {
            data.Reset();
            Notify(data);
            TurnOff();

            tileFactory?.Reset();
            for (int i = 0; i < mapConfig.MAX_VISIBILE_TILES; ++i)
            {
                SpawnTile();
            }
        }

        private void TurnOff()
        {
            foreach(var tile in SpawnedTiles)
            {
                tile.Disable();
                Unsubscribe(tile);
            }
        }
    }
}
