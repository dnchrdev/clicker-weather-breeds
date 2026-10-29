using System;
using _Project.Features.Clicker.Scripts.Entities;
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
        private readonly IEnergy _energy;
        private readonly Wallet _wallet;
        
        private readonly CompositeDisposable _disposables =  new CompositeDisposable();

        public ClickerPresenter(ClickerTabState clickerTabState, IClickerView clickerView, IEnergy energy, Wallet wallet)
        {
            _clickerTabState = clickerTabState;
            _clickerView = clickerView;
            _energy = energy;
            _wallet = wallet;
        }

        public void Initialize()
        {
            _clickerTabState.IsActive
                .Subscribe(active => TabReaction(active))
                .AddTo(_disposables);
            
            _energy.Amount
                .DistinctUntilChanged()
                .Subscribe(amount => _clickerView.UpdateEnergy(amount))
                .AddTo(_disposables);
            
            _energy.Ratio
                .DistinctUntilChanged()
                .Subscribe(ratio => _clickerView.UpdateEnergyFill(ratio))
                .AddTo(_disposables);
            
            _wallet.OuterAmount
                .DistinctUntilChanged()
                .Subscribe(amount => _clickerView.UpdateWallet(amount))
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