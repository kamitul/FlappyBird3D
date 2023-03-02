using Player;
using Player.Collision;
using Pooling;
using Spawner.Factories.Obstacles;
using Spawner.Factories.Tiles;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Map
{
    public class Tile : VisibilityBehaviour, ITile, IPoolable
    {
        [SerializeField] private Vector3 obstacleArea;
        [SerializeField] private List<PlayerCollider> colliders;

        private List<IObstacle> obstacles = new List<IObstacle>();

        public bool IsTaken { get; private set; }
        public Action<ITile> OnTilePassed { get; set; }
        public Action<IObstacle> OnObstaclePassed { get; set; }

        public Vector3 GetPosition() => transform.position;
        public Vector3 GetExtents() => Bounds.extents;
        public Vector3 GetObstacleArea() => obstacleArea;
        public void Release() => IsTaken = false;
        public void Acquire() => IsTaken = true;

        private void OnEnable()
        {
            colliders.ForEach(x => x.OnCollided += OnCollision);
        }

        private void OnDisable()
        {
            colliders.ForEach(x => x.OnCollided -= OnCollision);
            obstacles.ForEach(x => x.OnPassed -= OnObstacleGained);
        }

        public void SetObstacles(List<IObstacle> obs)
        {
            obstacles.Clear();
            obstacles.ForEach(x => x.OnPassed -= OnObstacleGained);

            obstacles = obs;
            obstacles.ForEach(x => x.OnPassed += OnObstacleGained);
        }

        public void SetPosition(Vector3 newPos)
        {
            transform.position = newPos;
        }

        private void OnObstacleGained(IObstacle obj)
        {
            OnObstaclePassed?.Invoke(obj);
        }

        private void OnCollision(Player.PlayerController player)
        {
            if (player.GetState() != State.Dead)
                player.SetState(State.Dead);
        }

        public override void Invisible()
        {
            Disable();
            OnTilePassed?.Invoke(this);
        }

        public void HandleVisibility(Transform trnsf)
        {
            CheckVisiblity(trnsf);
            obstacles.ForEach(x => x.HandleVisibility(trnsf));
        }

        public void Disable()
        {
            obstacles.ForEach(x => x.Disable());
            obstacles.Clear();

            gameObject.SetActive(false);
            Release();
        }
    }
}