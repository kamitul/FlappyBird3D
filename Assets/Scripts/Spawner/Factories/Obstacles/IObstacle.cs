using System;
using UnityEngine;

namespace Spawner.Factories.Obstacles
{
    public interface IObstacle
    {
        public bool IsRandomizable { get;  }
        public Action<IObstacle> OnPassed { get; set; }
        public void Initialize(Vector3 position);
        public void HandleVisibility(Transform trnsf);
        public void Disable();
    }
}