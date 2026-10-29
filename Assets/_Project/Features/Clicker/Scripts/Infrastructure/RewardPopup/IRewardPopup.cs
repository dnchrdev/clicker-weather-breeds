using System;
using UniRx;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup
{
    public interface IRewardPopup: IDisposable
    {
        public IObservable<int> Arrived { get; }
        public IObservable<Unit> Finished { get; }
        public void Play(int reward, Transform targetTransform);
        void Show(Transform parent, Vector3 position);
        void Hide(Transform parent);
    }
        
}