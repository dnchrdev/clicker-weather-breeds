using System;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters
{
    public class ClickerPresenter:  IInitializable, IDisposable
    {
        private readonly ClickerTabState _clickerTabState;
        private readonly IClickerView _clickerView;
        
        private readonly CompositeDisposable _disposables =  new CompositeDisposable();

        public ClickerPresenter(ClickerTabState clickerTabState,  IClickerView clickerView)
        {
            _clickerTabState = clickerTabState;
            _clickerView = clickerView;
        }

        public void Initialize()
        {
            _clickerTabState.IsActive.Subscribe(active => TabReaction(active)).AddTo(_disposables);
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
            _clickerView.ShowView();
            _clickerView.SubscribeToButtons();
        }
        
        private void OnTabExit()
        {
            _clickerView.UnsubscribeToButtons();
            _clickerView.HideView();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}