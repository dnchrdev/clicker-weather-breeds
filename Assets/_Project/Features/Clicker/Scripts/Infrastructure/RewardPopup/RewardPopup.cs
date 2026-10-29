using System;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup
{
    public class RewardPopup : MonoBehaviour, IRewardPopup
    {
        [SerializeField] private TMP_Text _rewardText;
        
        [Header("Animation")]
        [SerializeField] private float _appearDuration = 0.2f;
        [SerializeField] private float _holdDuration = 0.4f;
        [SerializeField] private float _flyDuration = 0.5f;
        [SerializeField] private float _disappearDuration = 0.15f;
        
        private Subject<Unit> _finished = new Subject<Unit>();
        private Subject<int> _arrived = new Subject<int>();
        private int _reward;
        private TweenCallback _onArrived;
        private TweenCallback _onFinished;
        private Tween _toWalletTranslationTween;

        public IObservable<Unit> Finished => _finished;
        public IObservable<int> Arrived => _arrived;

        private void Awake()
        {
            _onArrived = () => _arrived.OnNext(_reward);
            _onFinished = () => _finished.OnNext(Unit.Default);
        }

        public void Show(Transform parent, Vector3 position)
        {
            gameObject.transform.SetParent(parent);
            gameObject.transform.position = position;
        }

        private Sequence _sequence;

        public void Play(int reward, Transform targetPosition)
        {
            _reward = reward;
            _rewardText.SetText("+{0}", reward);

            _sequence?.Kill();

            transform.localScale = Vector3.zero;

            _sequence = DOTween.Sequence()
                .Append(transform.DOScale(1f, _appearDuration).SetEase(Ease.OutCubic))
                .AppendInterval(_holdDuration)
                .Append(transform.DOMove(targetPosition.position, _flyDuration).SetEase(Ease.InQuad))
                .AppendCallback(_onArrived)
                .Append(transform.DOScale(0f, _disappearDuration).SetEase(Ease.Linear))
                .OnComplete(_onFinished)
                .SetLink(gameObject);
        }

        public void Hide(Transform parent)
        {
            _sequence?.Kill();
            _sequence = null;

            transform.SetParent(parent);
            transform.localScale = Vector3.one;
        }

        void IDisposable.Dispose()
        {
            _sequence?.Kill();
            _finished.OnCompleted();

            // При выгрузке сцены Unity уже уничтожил объект раньше, чем вызвался Dispose.
            if (this != null)
                Destroy(gameObject);
        }
    }
}