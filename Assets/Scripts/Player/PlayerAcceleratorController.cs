using Config;
using Spawner;
using System;
using UnityEngine;

namespace Player
{
    public sealed class PlayerAcceleratorController : IObserver<MapData>
    {
        private readonly PlayerController playerController;
        private readonly MapConfig mapConfig;

        public PlayerAcceleratorController(PlayerController playerController)
        {
            this.playerController = playerController;
            mapConfig = Configuration.GetConfig<MapConfig>();
        }

        public void OnCompleted()
        {
            Debug.Log("Completed");
        }

        public void OnError(Exception error)
        {
            Debug.LogError($"{error.Message}");
        }

        public void OnNext(MapData data)
        {
            if (data.ObstaclesPassed % mapConfig.OBSTACLES_PASSED_ACCELERATOR == 0)
                playerController.Accelerate();

            if (data.ObstaclesPassed == 0)
                playerController.Reset();
        }
    }
}
