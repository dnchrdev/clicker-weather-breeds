using System;
using System.Collections.Generic;
using _Project.Core.Scripts.Infrastructure;
using _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases.Ports;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Features.Clicker.Scripts.Infrastructure
{
    public class RewardPopupView : MonoBehaviour, IRewardPopupView, IInitializable, IDisposable, IRaiseRewardPopup
    {
        private const int InitialPrewarmCount = 25;

        [SerializeField] private Transform _showedContainer;
        [SerializeField] private Transform _hiddenContainer;
        
        [SerializeField] private Transform _popupSpawnCenter;
        [SerializeField] private float _popupSpawnRadius = 50f;
        [SerializeField] private Transform _popupTargetTransform;
        
        private IRewardPopupFactory _factory;
        private ITapEffectView _tapEffectView;

        private GrowingPool<IRewardPopup> _pool;
        private readonly HashSet<IRewardPopup> _activePopups = new HashSet<IRewardPopup>();

        private readonly Subject<int> _rewardArrived = new Subject<int>();
        private readonly Subject<Unit> _allArrived = new Subject<Unit>();
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public IObservable<int> RewardArrived => _rewardArrived;
        public IObservable<Unit> AllArrived => _allArrived;

        [Inject]
        public void Construct(
            IRewardPopupFactory factory,
            ITapEffectView tapEffectView)
        {
            _factory = factory;
            _tapEffectView = tapEffectView;
        }

        public void Initialize()
        {
            _pool = new GrowingPool<IRewardPopup>(_factory, _hiddenContainer, InitialPrewarmCount);
            _pool.Created
                .Subscribe(OnPopupCreated)
                .AddTo(_disposables);
            _pool.Prewarm();
        }

        public void ShowRewardPopup(int reward)
        {
            if (!_pool.TryGet(out var popup))
                return;

            _activePopups.Add(popup);

            Vector2 offset = UnityEngine.Random.insideUnitCircle * _popupSpawnRadius;
            Vector3 position = _popupSpawnCenter.position + new Vector3(offset.x, offset.y, 0f);

            popup.Show(_showedContainer, position);
            popup.Play(reward, _popupTargetTransform);
            
            _tapEffectView.PlayTapEffect(position);
        }
        
        private void OnPopupCreated(IRewardPopup popup)
        {
            popup.Arrived
                .Subscribe(reward => _rewardArrived.OnNext(reward))
                .AddTo(_disposables);

            popup.Finished
                .Subscribe(_ => ReturnToPool(popup))
                .AddTo(_disposables);
        }

        private void ReturnToPool(IRewardPopup popup)
        {
            _pool.Return(popup);

            if (_activePopups.Remove(popup) && _activePopups.Count == 0)
                _allArrived.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _disposables.Dispose();

            _pool?.Dispose();
            _activePopups.Clear();

            _rewardArrived.OnCompleted();
            _allArrived.OnCompleted();
        }
    }
}
