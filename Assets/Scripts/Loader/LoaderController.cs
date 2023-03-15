using System;
using System.Collections.Generic;
using Utils;

namespace Loader
{
    public sealed class LoaderController : IObservable<LoaderData>
    {
        private LoaderData data;

        private readonly List<IObserver<LoaderData>> observers;

        public LoaderController()
        {
            observers = new List<IObserver<LoaderData>>();
            data = new LoaderData(0f);
        }

        public void UpdateProgress(float value)
        {
            data.Update(value);
            Notify(in data);
        }

        public IDisposable Subscribe(IObserver<LoaderData> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Disposer<LoaderData>(observers, observer);
        }

        private void Notify(in LoaderData data)
        {
            foreach (var observer in observers)
                observer.OnNext(data);
        }
    }
}
