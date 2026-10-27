using System.Collections.Generic;
using System.Linq;
using _Project.Core.Scripts.Entities;
using _Project.Core.Scripts.UseCases;

namespace _Project.Features.Tabs.Scripts.UseCases
{
    public class TabsStateMachine
    {
        private ITabState _currentState;
        private readonly Dictionary<TabType, ITabState> _tabs;

        public TabsStateMachine(List<ITabState> tabs)
        {
            _tabs = tabs.ToDictionary(tab => tab.Type); 
        }
        
        public void Initialize()
        {
            _currentState = _tabs.FirstOrDefault().Value;
            _currentState.Enter();
        }
        
        public void SwitchState(TabType tabType)
        {
            _currentState?.Exit();
            _currentState = Get(tabType);
            _currentState?.Enter();
        }
        
        public void Tick(float deltaTime)
        {
            _currentState?.Tick(deltaTime);
        }

        private ITabState Get(TabType type) => _tabs[type];
        
    }
}