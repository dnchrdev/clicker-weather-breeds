using System;
using _Project.Features.Weather.Scripts.Entities;
using _Project.Features.Weather.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Weather.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Weather.Scripts.InterfaceAdapters
{
    public class WeatherPresenter:  IInitializable, IDisposable
    {
        private readonly WeatherTabState _weatherTabState;
        private readonly WeatherForecast _weatherForecast;
        private readonly IWeatherView _weatherView;
        
        private readonly CompositeDisposable _disposables =  new CompositeDisposable();

        public WeatherPresenter(WeatherTabState weatherTabState, WeatherForecast weatherForecast, IWeatherView weatherView)
        {
            _weatherTabState = weatherTabState;
            _weatherForecast = weatherForecast;
            _weatherView = weatherView;
        }

        public void Initialize()
        {
            _weatherTabState.IsActive.
                Subscribe(active => TabReaction(active))
                .AddTo(_disposables);

            _weatherForecast.Updated
                .Subscribe(data => _weatherView.UpdateWeather(data.Temperature, data.TemperatureUnit, data.IconUrl))
                .AddTo(_disposables);
        }

        private void TabReaction(bool isActive)
        {
            if(isActive) 
                OnTabEnter();  
            else 
                OnTabExit();
        }
        
        private void OnTabEnter()
        {
            _weatherView.ShowView();
        }
        
        private void OnTabExit()
        {
            _weatherView.HideView();
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
