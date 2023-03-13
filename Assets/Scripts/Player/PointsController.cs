using System;
using System.Collections.Generic;
using Utils;

namespace Player
{
    public class PointsController : IObservable<PlayerData>
    {
        private PlayerData data;
        private bool isCalculating;

        private readonly List<IObserver<PlayerData>> observers;

        public PointsController()
        {
            observers = new List<IObserver<PlayerData>>();
        }

        public void Update() 
        {
            if (isCalculating) 
                AddPoint();
        }

        public void AddPoint()
        {
            data.Update();
            Notify(in data);
        }

        public void Begin()
        {
            isCalculating = true;
        }

        public void Stop()
        {
            isCalculating = false;
        }

        public void Reset()
        {
            data.Reset();
        }

        public IDisposable Subscribe(IObserver<PlayerData> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Disposer<PlayerData>(observers, observer);
        }

        private void Notify(in PlayerData data)
        {
            foreach (var observer in observers)
                observer.OnNext(data);
        }
    }
}
