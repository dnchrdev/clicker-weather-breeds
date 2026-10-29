using System;
using _Project.Core.Scripts.Entities;
using _Project.Core.Scripts.UseCases;
using UniRx;

namespace _Project.Features.Weather.Scripts.UseCases
{
    public class WeatherTabState: ITabState
    {
        private ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>(false);
        
        public IObservable<bool> IsActive => _isActive;
        
        public TabType Type => TabType.WEATHER;
        
        public void Enter()
        {
            _isActive.Value = true;
        }

        public void Tick(float dt)
        {
            
        }

        public void Exit()
        {
            _isActive.Value = false;
        }
    }
}