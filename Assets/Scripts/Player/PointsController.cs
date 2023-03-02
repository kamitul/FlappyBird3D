using Config;
using System;
using System.Collections.Generic;
using Utils;

namespace Player
{
    public class PointsController : IObservable<PlayerData>
    {
        private readonly PlayerData data;
        private readonly List<IObserver<PlayerData>> observers;
        private bool isCalculating;

        public PointsController()
        {
            observers = new List<IObserver<PlayerData>>();
            data = Configuration.GetConfig<PlayerData>();
        }

        public void Update() 
        {
            if (isCalculating) 
                AddPoint();
        }

        public void AddPoint()
        {
            data.Update();
            Notify(data);
        }

        public void Begin()
        {
            isCalculating = true;
        }

        public void Stop()
        {
            isCalculating = false;
        }

        public IDisposable Subscribe(IObserver<PlayerData> observer)
        {
            if (!observers.Contains(observer))
                observers.Add(observer);
            return new Disposer<PlayerData>(observers, observer);
        }

        private void Notify(PlayerData data)
        {
            foreach (var observer in observers)
                observer.OnNext(data);
        }
    }
}
