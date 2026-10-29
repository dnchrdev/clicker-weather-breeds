using UnityEngine;

namespace _Project.Core.Scripts.Infrastructure
{
    public interface IPoolFactory<T>
    {
        public T Create(Transform parent);
    }
}
