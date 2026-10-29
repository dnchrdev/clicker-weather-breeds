using System;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace _Project.Core.Scripts.Infrastructure
{
    public sealed class GrowingPool<T> : IDisposable where T : class, IPoolItem
    {
        private readonly IPoolFactory<T> _factory;
        private readonly Transform _hiddenContainer;
        private readonly ObjectPool<T> _free = new ObjectPool<T>();
        private readonly List<T> _all = new List<T>();
        private readonly Subject<T> _created = new Subject<T>();

        private int _growCount;

        public IObservable<T> Created => _created;

        public GrowingPool(IPoolFactory<T> factory, Transform hiddenContainer, int initialCount)
        {
            _factory = factory;
            _hiddenContainer = hiddenContainer;
            _growCount = Math.Max(1, initialCount);
        }

        public void Prewarm()
        {
            Grow();
        }

        public bool TryGet(out T item)
        {
            if (_free.TryGet(out item))
                return true;

            Grow();

            return _free.TryGet(out item);
        }

        public void Return(T item)
        {
            item.Hide(_hiddenContainer);
            _free.Return(item);
        }

        private void Grow()
        {
            for (int i = 0; i < _growCount; i++)
            {
                var item = _factory.Create(_hiddenContainer);

                item.Hide(_hiddenContainer);
                _all.Add(item);

                _created.OnNext(item);
                _free.Return(item);
            }

            _growCount *= 2;
        }

        public void Dispose()
        {
            _created.OnCompleted();

            for (int i = _all.Count - 1; i >= 0; i--)
                _all[i].Dispose();

            _all.Clear();
            _free.Clear();
        }
    }
}
