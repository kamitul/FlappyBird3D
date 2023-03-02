using System;
using System.Collections.Generic;
using System.Linq;
using Constants;
using Cysharp.Threading.Tasks;
using Map;
using Obstacles;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Services.Assets
{
    public class OnlineAssetService : AssetService, IDisposable
    {
        private List<Tile> tiles;
        private List<Obstacle> obstacles;
        private readonly List<AsyncOperationHandle> handlers = new List<AsyncOperationHandle>();

        public override async UniTask Download()
        {
            await UniTask.WhenAll(DownloadTiles(), DownloadObstacles());
        }

        public override List<Tile> GetTiles()
        {
            return tiles;
        }

        public override List<Obstacle> GetObstacles()
        {
            return obstacles;
        }

        private async UniTask DownloadTiles()
        {
            var handle = Addressables.LoadAssetsAsync<GameObject>(Constant.ADDRESSABLE_TILE_LABEL, null);
            handlers.Add(handle);

            try
            {
                var res = await handle.Task.AsUniTask();
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Successfuly loaded tiles from server!");
                    tiles = res.Select(x=>x.GetComponent<Tile>()).ToList();
                }
                else Debug.LogError("Error downloading tiles from server!");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        private async UniTask DownloadObstacles()
        {
            var handle = Addressables.LoadAssetsAsync<GameObject>(Constant.ADDRESSABLE_OBSTACLE_LABEL, null);
            handlers.Add(handle);

            try
            {
                var res = await handle.Task.AsUniTask();
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    Debug.Log("Successfuly loaded obstacles from server!");
                    obstacles = res.Select(x=>x.GetComponent<Obstacle>()).ToList();
                }
                else Debug.LogError("Error downloading obstacles from server!");
            }
            catch (Exception e)
            {
                Debug.LogError(e.Message);
            }
        }

        public void Dispose()
        {
            handlers.ForEach(x => Addressables.Release(x));
        }
    }
}
