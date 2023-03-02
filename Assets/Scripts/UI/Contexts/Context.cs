using System.Threading;
using UnityEngine;

namespace UI.Contexts
{
    [DisallowMultipleComponent()]
    [RequireComponent(typeof(Canvas))]
    public abstract class Context : MonoBehaviour
    {
        protected UIController controller;

        public abstract ContextIdentifier Identifier { get; }
        public CancellationTokenSource CTS = new CancellationTokenSource();

        public void Initalize(UIController controller)
        {
            this.controller = controller;
        }

        public virtual void Open()
        {
            Toggle(true);
            CTS?.Dispose();
            CTS = new CancellationTokenSource();
        }

        public virtual void Close()
        {
            Toggle(false);
            CTS?.Cancel();
        }

        public void Toggle(bool flag)
        {
            gameObject.SetActive(flag);
        }

        public virtual void OnDestroy()
        {
            CTS?.Cancel();
            CTS?.Dispose();
        }
    }
}
