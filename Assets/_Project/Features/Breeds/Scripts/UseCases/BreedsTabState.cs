using System;
using _Project.Core.Scripts.Entities;
using _Project.Core.Scripts.UseCases;
using UniRx;

namespace _Project.Features.Breeds.Scripts.UseCases
{
    public class BreedsTabState: ITabState
    {
        private Subject<Unit> _enterEvent = new Subject<Unit>();
        private Subject<Unit> _exitEvent = new Subject<Unit>();
        
        public IObservable<Unit> EnterEvent => _enterEvent;
        public IObservable<Unit> ExitEvent => _exitEvent;
        
        public TabType Type =>  TabType.BREEDS;

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