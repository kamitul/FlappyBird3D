using Player;
using Player.Collision;
using Pooling;
using Spawner.Factories.Obstacles;
using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace Obstacles
{
    public class Obstacle : VisibilityBehaviour, IObstacle, IPoolable
    {
        [SerializeField] private List<PlayerCollider> colliders;

        [field: SerializeField] public bool IsRandomizable { get; private set; }
        public bool IsTaken { get; private set; }
        public Action<IObstacle> OnPassed { get; set; }

        public void Release() => IsTaken = false;
        public void Acquire() => IsTaken = true;
        public void Initialize(Vector3 position) => transform.position = position;

        public override void Invisible()
        {
            Disable();
            OnPassed?.Invoke(this);
        }

        private void OnEnable()
        {
            colliders.ForEach(x => x.OnCollided += OnCollision);
        }

        private void OnDisable()
        {
            colliders.ForEach(x => x.OnCollided -= OnCollision);
        }

        private void OnCollision(Player.PlayerController player)
        {
            if (player.GetState() != State.Dead)
                player.SetState(State.Dead);
        }

        public void HandleVisibility(Transform trnsf)
        {
            CheckVisiblity(trnsf);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            Release();
        }
    }
}