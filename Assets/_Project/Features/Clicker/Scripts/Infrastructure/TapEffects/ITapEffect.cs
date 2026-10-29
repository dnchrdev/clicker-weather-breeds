using System;
using _Project.Core.Scripts.Infrastructure;
using UniRx;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure.TapEffects
{
    public interface ITapEffect : IPoolItem
    {
        public IObservable<Unit> Finished { get; }

        public void Play(Transform parent, Vector3 position);
    }
}
