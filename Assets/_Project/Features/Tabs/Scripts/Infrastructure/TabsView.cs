using System;
using _Project.Core.Scripts.Entities;
using _Project.Features.Tabs.Scripts.InterfaceAdapters.Ports;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.Tabs.Scripts.Infrastructure
{
    public class TabsView : MonoBehaviour, ITabsView
    {
        [SerializeField] private Button _clickerButton;
        [SerializeField] private Button _weatherButton;
        [SerializeField] private Button _breedsButton;

        private readonly Subject<TabType> _tabButtonClicked = new Subject<TabType>();

        public IObservable<TabType> TabButtonClicked => _tabButtonClicked;

        private readonly CompositeDisposable _buttonSubscriptions = new CompositeDisposable();

        public void SubscribeToButtons()
        {
            _buttonSubscriptions.Clear();  
            Bind(_clickerButton, TabType.CLICKER);
            Bind(_weatherButton, TabType.WEATHER);
            Bind(_breedsButton, TabType.BREEDS);
        }

        private void Bind(Button button, TabType tabType)
        {
            button.OnClickAsObservable()
                .Subscribe(_ => _tabButtonClicked.OnNext(tabType))
                .AddTo(_buttonSubscriptions);
        }

        public void UnsubscribeFromButtons()
        {
            _buttonSubscriptions.Clear();
        }

        public void OnDestroy()
        {
            _buttonSubscriptions.Dispose();
            _tabButtonClicked.OnCompleted();
            _tabButtonClicked.Dispose();
        }
    }
}