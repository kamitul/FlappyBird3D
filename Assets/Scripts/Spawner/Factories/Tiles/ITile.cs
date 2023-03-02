using Spawner.Factories.Obstacles;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Spawner.Factories.Tiles
{
    public interface ITile
    {
        public  Action<ITile> OnTilePassed { get; set; }
        public Action<IObstacle> OnObstaclePassed { get; set; }
        public void SetObstacles(List<IObstacle> obstacles);
        public void SetPosition(Vector3 position);
        public void HandleVisibility(Transform trnsf);
        public void Disable();
        public Vector3 GetPosition();
        public Vector3 GetExtents();
        public Vector3 GetObstacleArea();
    }
}
