using System;
using System.Collections.Generic;
using _Project.Core.Scripts.Entities;
using _Project.Core.Scripts.Infrastructure;
using _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    public class RewardPopupView : MonoBehaviour, IRewardPopupView, IInitializable, IDisposable
    {
        private const int InitialPrewarmCount = 50;

        [SerializeField] private Transform _showedContainer;
        [SerializeField] private Transform _hiddenContainer;
        
        [SerializeField] private Transform _popupSpawnCenter;
        [SerializeField] private float _popupSpawnRadius = 50f;
        [SerializeField] private Transform _popupTargetTransform;
        
        private IRewardPopupFactory _factory;

        private readonly ObjectPool<IRewardPopup> _pool = new ObjectPool<IRewardPopup>();
        private readonly List<IRewardPopup> _allPopups = new List<IRewardPopup>();
        private readonly HashSet<IRewardPopup> _activePopups = new HashSet<IRewardPopup>();

        private readonly Subject<int> _rewardArrived = new Subject<int>();
        private readonly Subject<Unit> _allSettled = new Subject<Unit>();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        private int _growCount = InitialPrewarmCount;

        public IObservable<int> RewardArrived => _rewardArrived;
        public IObservable<Unit> AllSettled => _allSettled;

        [Inject]
        public void Construct(
            IRewardPopupFactory factory)
        {
            _factory = factory;
        }

        public void Initialize()
        {
            Grow();
        }

        public void SpawnRewardPopup(int reward)
        {
            if (!_pool.TryGet(out var popup))
            {
                Grow();

                if (!_pool.TryGet(out popup))
                    return;
            }

            _activePopups.Add(popup);

            Vector2 offset = UnityEngine.Random.insideUnitCircle * _popupSpawnRadius;
            Vector3 position = _popupSpawnCenter.position + new Vector3(offset.x, offset.y, 0f);

            popup.Show(_showedContainer, position);
            popup.Play(reward, _popupTargetTransform);
        }

        private void Grow()
        {
            for (int i = 0; i < _growCount; i++)
            {
                var popup = CreatePopup();
                _allPopups.Add(popup);
                _pool.Return(popup);
            }

            _growCount *= 2;
        }

        private IRewardPopup CreatePopup()
        {
            var popup = _factory.Create(_hiddenContainer);

            popup.Hide(_hiddenContainer);

            popup.Arrived
                .Subscribe(reward => _rewardArrived.OnNext(reward))
                .AddTo(_disposables);

            popup.Finished
                .Subscribe(_ => ReturnToPool(popup))
                .AddTo(_disposables);

            return popup;
        }

        private void ReturnToPool(IRewardPopup popup)
        {
            popup.Hide(_hiddenContainer);
            _pool.Return(popup);

            if (_activePopups.Remove(popup) && _activePopups.Count == 0)
                _allSettled.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _disposables.Dispose();

            for (int i = _allPopups.Count - 1; i >= 0; i--)
                _allPopups[i].Dispose();

            _allPopups.Clear();
            _activePopups.Clear();
            _pool.Clear();

            _rewardArrived.OnCompleted();
            _allSettled.OnCompleted();
        }
    }
}
