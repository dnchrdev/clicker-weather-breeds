using System;
using _Project.Features.Breeds.Scripts.InterfaceInstaller.Ports;
using _Project.Features.Breeds.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Breeds.Scripts.InterfaceInstaller
{
    public class BreedsPresenter:  IInitializable, IDisposable
    {
        private readonly BreedsTabState _breedsTabState;
        private readonly IBreedsView _breedsView;
        
        private readonly CompositeDisposable _disposables =  new CompositeDisposable();

        public BreedsPresenter(BreedsTabState breedsTabState,  IBreedsView breedsView)
        {
            _breedsTabState = breedsTabState;
            _breedsView = breedsView;
        }

        public void Initialize()
        {
            _breedsTabState.IsActive.Subscribe(active => TabReaction(active)).AddTo(_disposables);
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
            _breedsView.HideView();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}