using System;
using _Project.Core.Scripts.Infrastructure;
using UniRx;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure.RewardPopup
{
    public interface IRewardPopup : IPoolItem
    {
        public IObservable<int> Arrived { get; }
        public IObservable<Unit> Finished { get; }
        public void Play(int reward, Transform targetTransform);
        void Show(Transform parent, Vector3 position);
    }
}
