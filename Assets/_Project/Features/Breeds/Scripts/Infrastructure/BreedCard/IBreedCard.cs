using System;
using UnityEngine;

namespace _Project.Features.Breeds.Scripts.Infrastructure.BreedCard
{
    public interface IBreedCard
    {
        public string Id { get; }
        public IObservable<string> CardClicked { get; }
        
        public void Initialize(string breedId, string text);
        public void Show(Transform parent);
        public void Hide(Transform parent);
        public void ShowLoading();
        public void HideLoading();
    }
}
