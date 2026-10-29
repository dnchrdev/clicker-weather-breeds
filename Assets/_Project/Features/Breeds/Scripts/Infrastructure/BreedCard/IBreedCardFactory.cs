using UnityEngine;

namespace _Project.Features.Breeds.Scripts.Infrastructure.BreedCard
{
    public interface IBreedCardFactory
    {
        public IBreedCard Create(Transform parent);
    }
}
