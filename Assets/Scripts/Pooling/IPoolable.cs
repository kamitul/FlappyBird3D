using UnityEngine;

namespace Pooling
{
    public interface IPoolable
    {
        public bool IsTaken { get; }
        public void Release();
        public void Acquire();
    }
}
