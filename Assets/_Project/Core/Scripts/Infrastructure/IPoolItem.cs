using System;
using UnityEngine;

namespace _Project.Core.Scripts.Infrastructure
{
    public interface IPoolItem : IDisposable
    {
        public void Hide(Transform parent);
    }
}
