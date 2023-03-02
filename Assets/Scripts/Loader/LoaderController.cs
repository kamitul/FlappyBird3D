using System;
using System.Collections.Generic;
using Utils;

namespace Loader
{

    public class LoaderController : IObservable<LoaderData>
    {
        private readonly List<IObserver<LoaderData>> observers;
        private readonly LoaderData data;

        public LoaderController()
        {
            observers = new List<IObserver<LoaderData>>();
            data = new LoaderData(0f);
        }

        public void UpdateProgress(float value)
        {
            data.Progress = value;
            Notify(data);
        }

        public IDisposable Subscribe(IObserver<LoaderData> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Disposer<LoaderData>(observers, observer);
        }

        private void Notify(LoaderData data)
        {
            foreach (var observer in observers)
                observer.OnNext(data);
        }
    }
}
