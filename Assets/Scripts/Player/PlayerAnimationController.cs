using UnityEngine;

namespace Player
{
    [DisallowMultipleComponent()]
    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationController : MonoBehaviour
    {
        private Animator animator;

        private void Awake()
        {
            animator = GetComponent<Animator>();
        }

        public void SetFly()
        {
            animator.SetTrigger("isFlying");
        }

        public void SetDie()
        {
            animator.SetTrigger("isDead");
        }
    }
}
