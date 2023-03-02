using System;
using UnityEngine;

namespace Utils
{
    [DisallowMultipleComponent()]
    public class VisibilityBehaviour : MonoBehaviour
    {
        [field: SerializeField] public Bounds Bounds { get; private set; }

        public event Action OnInvisible;

        private void OnValidate()
        {
            Renderer[] renderers = GetComponentsInChildren<Renderer>();
            Bounds = renderers[0].bounds;

            for (int i = 1; i < renderers.Length; ++i)
            {
                Bounds.Encapsulate(renderers[i].bounds.min);
                Bounds.Encapsulate(renderers[i].bounds.max);
            }
        }

        public void CheckVisiblity(Transform trnsf)
        {
            if (!gameObject.activeInHierarchy) return;
            Vector3 toTarget = trnsf.position - transform.position;
            if ( toTarget.x > Bounds.extents.x) Invisible();
        }

        public virtual void Invisible() { }
    }
}
