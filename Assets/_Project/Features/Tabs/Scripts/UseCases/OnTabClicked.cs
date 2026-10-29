using _Project.Core.Scripts.Entities;

namespace _Project.Features.Tabs.Scripts.UseCases
{
    public class OnTabClicked
    {
        private readonly TabsStateMachine  _tabsStateMachine;
        
        public void TabButtonClicked(TabType tabType)
        {
            _tabsStateMachine.SwitchState(tabType);
        }
    }
}