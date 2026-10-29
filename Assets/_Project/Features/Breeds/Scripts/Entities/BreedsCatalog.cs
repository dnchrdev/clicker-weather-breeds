using System;
using System.Collections.Generic;
using UniRx;

namespace _Project.Features.Breeds.Scripts.Entities
{
    public class BreedsCatalog
    {
        private readonly Subject<IReadOnlyList<Breed>> _updated = new Subject<IReadOnlyList<Breed>>();

        public IReadOnlyList<Breed> Current { get; private set; }

        public IObservable<IReadOnlyList<Breed>> Updated => _updated;

        public bool TryUpdate(IReadOnlyList<Breed> breeds)
        {
            if (!IsValid(breeds))
                return false;

            Current = breeds;
            _updated.OnNext(breeds);

            return true;
        }

        private bool IsValid(IReadOnlyList<Breed> breeds)
        {
            if (breeds == null || breeds.Count == 0)
                return false;

            for (int i = 0; i < breeds.Count; i++)
            {
                var breed = breeds[i];

                if (breed == null || string.IsNullOrEmpty(breed.Id) || string.IsNullOrEmpty(breed.Name))
                    return false;
            }

            return true;
        }
    }
}
