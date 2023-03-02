using System;
using UnityEngine;

namespace Player.Collision
{
    [RequireComponent(typeof(Collider))]
    public class PlayerCollider : MonoBehaviour
    {
        public Action<PlayerController> OnCollided;

        private void OnCollisionEnter(UnityEngine.Collision collision)
        {
            var player = collision.collider.GetComponent<PlayerController>();
            if (player) OnCollided?.Invoke(player);
        }
    }
}