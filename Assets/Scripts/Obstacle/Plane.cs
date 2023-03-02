using Player;
using UnityEngine;

namespace Obstacles
{
    public class Plane : MonoBehaviour
    {
        private void OnCollisionEnter(Collision collision)
        {
            var character = collision.collider.GetComponent<Player.PlayerController>();
            if (character) character.SetState(State.Dead);
        }
    }
}