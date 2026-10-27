using System;
using _Project.Features.Tabs.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Tabs.Scripts.UseCases;
using UniRx;
using UnityEngine;
using Zenject;

namespace _Project.Features.Tabs.Scripts.InterfaceAdapters
{
    public class TabsController: IInitializable, ITickable, IDisposable
    {
        private readonly TabsStateMachine _tabsStateMachine;
        private readonly ITabsView _tabView;
        
        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public TabsController(TabsStateMachine tabsStateMachine, ITabsView tabView)
        {
            _tabsStateMachine = tabsStateMachine;
            _tabView = tabView;
        }
        
        public void Initialize()
        {
            _tabsStateMachine.Initialize();
            
            _tabView.TabButtonClicked
                .DistinctUntilChanged()
                .Subscribe(type => _tabsStateMachine.SwitchState(type))
                .AddTo(_disposables);
        }
        
        public void Tick()
        {
            _tabsStateMachine?.Tick(Time.deltaTime);
        }

        public void Dispose()
        {
            _disposables.Clear();
            _disposables.Dispose();
        }
    }
}