using System;
using _Project.Features.Tabs.Scripts.InterfaceAdapters.Ports;
using Zenject;

namespace _Project.Features.Tabs.Scripts.InterfaceAdapters
{
    public class TabsPresenter : IInitializable, IDisposable
    {
        private readonly ITabsView _tabsView;

        public TabsPresenter(ITabsView tabsView)
        {
            _tabsView = tabsView;
        }

        public void Initialize()
        {
            _tabsView.SubscribeToButtons();
        }

        public void Dispose()
        {
            _tabsView.UnsubscribeFromButtons();
        }
    }
}