using System;
using _Project.Core.Scripts.Entities;
using UniRx;

namespace _Project.Features.Tabs.Scripts.InterfaceAdapters.Ports
{
    public interface ITabsView
    {
        public IObservable<TabType> TabButtonClicked { get; }

        public void SubscribeToButtons();
        public void UnsubscribeFromButtons();
    }
}