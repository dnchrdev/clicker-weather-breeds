using _Project.Core.Scripts.Infrastructure;
using UnityEngine;

namespace _Project.Features.Clicker.Scripts.Infrastructure.TapEffects
{
    public class TapEffectFactory : IPoolFactory<ITapEffect>
    {
        private readonly GameObject _prefab;

        public TapEffectFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public ITapEffect Create(Transform parent)
        {
            var go = Object.Instantiate(_prefab, parent);
            return go.GetComponent<ITapEffect>();
        }
    }
}
