using System;
using System.Collections.Generic;
using _Project.Features.Breeds.Scripts.Entities;
using UniRx;

namespace _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports
{
    public interface IBreedsView
    {
        public IObservable<string> CardClicked { get; }
        public IObservable<Unit> PopupCloseClicked { get; }
        public void ShowView();
        public void HideView();
        public void GenerateBreedButtons(IReadOnlyList<Breed> breeds);
        public void RemoveBreedsButtons();
        public void SetListLoading(bool isLoading);
        public void SetCardLoading(string breedId);
        public void ShowBreedPopup(string breedName, string breedDescription);
        public void HideBreedPopup();
        
    }
}
