using System;
using _Project.Core.Scripts.Entities;
using _Project.Core.Scripts.UseCases;
using _Project.Features.Weather.Scripts.Entities;
using UniRx;

namespace _Project.Features.Weather.Scripts.UseCases
{
    public class WeatherTabState: ITabState
    {
        private readonly IWeatherRefreshTimer _refreshTimer;
        private readonly LoadWeatherUseCase _loadWeatherUseCase;

        private ReactiveProperty<bool> _isActive = new ReactiveProperty<bool>(false);
        
        public IObservable<bool> IsActive => _isActive;
        
        public TabType Type => TabType.WEATHER;

        public WeatherTabState(IWeatherRefreshTimer refreshTimer, LoadWeatherUseCase loadWeatherUseCase)
        {
            _refreshTimer = refreshTimer;
            _loadWeatherUseCase = loadWeatherUseCase;
        }
        
        public void Enter()
        {
            _isActive.Value = true;
            _refreshTimer.Start();
        }

        public void Tick(float dt)
        {
            _refreshTimer.Advance(dt);
        }

        public void Exit()
        {
            _isActive.Value = false;
            _refreshTimer.Reset();
            _loadWeatherUseCase.Cancel();
        }
    }
}
