using System;
using _Project.Features.Breeds.Scripts.Entities;
using _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports;
using _Project.Features.Breeds.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Breeds.Scripts.InterfaceInstaller
{
    public class BreedsPresenter:  IInitializable, IDisposable
    {
        private readonly BreedsTabState _breedsTabState;
        private readonly BreedsCatalog _breedsCatalog;
        private readonly BreedPopupInfo _breedPopupInfo;
        private readonly BreedsLoadState _loadState;
        private readonly IBreedsView _breedsView;
        
        private readonly CompositeDisposable _disposables =  new CompositeDisposable();

        public BreedsPresenter(
            BreedsTabState breedsTabState,
            BreedsCatalog breedsCatalog,
            BreedPopupInfo breedPopupInfo,
            BreedsLoadState loadState,
            IBreedsView breedsView)
        {
            _breedsTabState = breedsTabState;
            _breedsCatalog = breedsCatalog;
            _breedPopupInfo = breedPopupInfo;
            _loadState = loadState;
            _breedsView = breedsView;
        }

        public void Initialize()
        {
            _breedsTabState.IsActive.Subscribe(active => TabReaction(active)).AddTo(_disposables);

            _breedsCatalog.Updated
                .Subscribe(breeds => _breedsView.GenerateBreedButtons(breeds))
                .AddTo(_disposables);

            _loadState.ListLoading
                .Subscribe(isLoading => _breedsView.SetListLoading(isLoading))
                .AddTo(_disposables);

            _loadState.LoadingBreedId
                .Subscribe(breedId => _breedsView.SetCardLoading(breedId))
                .AddTo(_disposables);

            _breedPopupInfo.Opened
                .Subscribe(details => _breedsView.ShowBreedPopup(details.Name, details.Description))
                .AddTo(_disposables);

            _breedPopupInfo.Closed
                .Subscribe(_ => _breedsView.HideBreedPopup())
                .AddTo(_disposables);
        }

        private void TabReaction(bool isActive)
        {
            if(isActive) 
                OnTabEnter();  
            else 
                OnTabExit();
        }
        
        private void OnTabEnter()
        {
            _breedsView.ShowView();
        }
        
        private void OnTabExit()
        {
            _breedsView.RemoveBreedsButtons();
            _breedsView.HideView();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
