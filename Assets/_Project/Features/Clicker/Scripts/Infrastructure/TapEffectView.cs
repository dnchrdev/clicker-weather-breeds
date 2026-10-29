using System;
using _Project.Core.Scripts.Infrastructure;
using _Project.Features.Clicker.Scripts.Infrastructure.TapEffects;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    public class TapEffectView : MonoBehaviour, ITapEffectView, IInitializable, IDisposable
    {
        private const int InitialPrewarmCount = 10;

        [SerializeField] private Transform _showedContainer;
        [SerializeField] private Transform _hiddenContainer;

        private IPoolFactory<ITapEffect> _factory;
        private GrowingPool<ITapEffect> _pool;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        [Inject]
        public void Construct(IPoolFactory<ITapEffect> factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            _pool = new GrowingPool<ITapEffect>(_factory, _hiddenContainer, InitialPrewarmCount);
            _pool.Created
                .Subscribe(OnEffectCreated)
                .AddTo(_disposables);
            _pool.Prewarm();
        }

        public void PlayTapEffect(Vector3 position)
        {
            if (!_pool.TryGet(out var effect))
                return;

            effect.Play(_showedContainer, position);
        }

        private void OnEffectCreated(ITapEffect effect)
        {
            effect.Finished
                .Subscribe(_ => _pool.Return(effect))
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
            _pool?.Dispose();
        }
    }
}
