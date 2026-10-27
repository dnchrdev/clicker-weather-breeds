using System;
using _Project.Features.Weather.Scripts.InterfaceInstaller.Ports;
using _Project.Features.Weather.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Weather.Scripts.InterfaceInstaller
{
    public class WeatherPresenter:  IInitializable, IDisposable
    {
        private readonly WeatherTabState _weatherTabState;
        private readonly IWeatherView _weatherView;
        
        private readonly CompositeDisposable _disposables =  new CompositeDisposable();

        public WeatherPresenter(WeatherTabState weatherTabState,  IWeatherView weatherView)
        {
            _weatherTabState = weatherTabState;
            _weatherView = weatherView;
        }

        public void Initialize()
        {
            _weatherTabState.EnterEvent.Subscribe(_ => OnTabEnter()).AddTo(_disposables);
            _weatherTabState.ExitEvent.Subscribe(_ => OnTabExit()).AddTo(_disposables);
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