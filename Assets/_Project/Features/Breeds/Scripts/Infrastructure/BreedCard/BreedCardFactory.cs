using _Project.Features.Breeds.Scripts.Infrastructure.BreedCard;
using UnityEngine;

namespace _Project.Features.Breeds.Scripts.Infrastructure
{
    public class BreedCardFactory : IBreedCardFactory
    {
        private readonly GameObject _prefab;

        public BreedCardFactory(GameObject prefab)
        {
            _prefab = prefab;
        }

        public IBreedCard Create(Transform parent)
        {
            var go = Object.Instantiate(_prefab, parent);
            return go.GetComponent<IBreedCard>();
        }
    }
}
