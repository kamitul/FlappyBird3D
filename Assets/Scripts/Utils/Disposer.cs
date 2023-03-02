using System;
using System.Collections.Generic;

namespace Utils
{
    public class Disposer<T> : IDisposable
    {
        private readonly List<IObserver<T>> observers;
        private readonly IObserver<T> observer;

        public Disposer(List<IObserver<T>> observers, IObserver<T> observer)
        {
            this.observers = observers;
            this.observer = observer;
        }

        public void Dispose()
        {
            if (observer != null && observers.Contains(observer))
                observers.Remove(observer);
        }
    }
}
