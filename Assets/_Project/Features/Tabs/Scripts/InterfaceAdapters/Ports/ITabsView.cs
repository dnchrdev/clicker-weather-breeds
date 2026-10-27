using System;
using _Project.Core.Scripts.Entities;

namespace _Project.Features.Tabs.Scripts.InterfaceAdapters.Ports
{
    public interface ITabsView
    {
        public IObservable<TabType> TabButtonClicked { get; }

        public void SubscribeToButtons();
        public void UnsubscribeFromButtons();
    }
}