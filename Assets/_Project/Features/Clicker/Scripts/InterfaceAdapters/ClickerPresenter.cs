using System;
using _Project.Features.Clicker.Scripts.Entities;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using UniRx;
using Zenject;

namespace _Project.Features.Clicker.Scripts.InterfaceAdapters
{
    public class ClickerPresenter:  IInitializable, IDisposable
    {
        private readonly ClickerTab _clickerTab;
        private readonly IClickerView _clickerView;
        
        private readonly CompositeDisposable _disposables =  new CompositeDisposable();

        public ClickerPresenter(ClickerTab clickerTab,  IClickerView clickerView)
        {
            _clickerTab = clickerTab;
            _clickerView = clickerView;
        }

        public void Initialize()
        {
            _clickerTab.EnterEvent.Subscribe(_ => OnTabEnter()).AddTo(_disposables);
            _clickerTab.ExitEvent.Subscribe(_ => OnTabExit()).AddTo(_disposables);
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