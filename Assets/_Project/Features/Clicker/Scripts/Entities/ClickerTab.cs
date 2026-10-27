using System;
using _Project.Core.Scripts.Entities;
using UniRx;

namespace _Project.Features.Clicker.Scripts.Entities
{
    public class ClickerTab: ITabState
    {
        private Subject<Unit> _enterEvent = new Subject<Unit>();
        private Subject<Unit> _exitEvent = new Subject<Unit>();
        
        public IObservable<Unit> EnterEvent => _enterEvent;
        public IObservable<Unit> ExitEvent => _exitEvent;
        
        public TabType Type =>  TabType.CLICKER;

        public void Enter()
        {
            _enterEvent.OnNext(Unit.Default);
        }

        public void Tick(float deltaTime)
        {
            
        }

        public void Exit()
        {
            _exitEvent.OnNext(Unit.Default);
        }
    }
}