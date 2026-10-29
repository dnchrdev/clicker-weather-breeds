using System;
using _Project.Features.Weather.Scripts.Entities;
using _Project.Features.Weather.Scripts.UseCases;
using UniRx;
using Zenject;

namespace _Project.Features.Weather.Scripts.InterfaceAdapters
{
    public class WeatherController : IInitializable, IDisposable
    {
        private readonly IWeatherRefreshRequest _refreshRequest;
        private readonly LoadWeatherUseCase _loadWeatherUseCase;

        private readonly CompositeDisposable _disposables = new CompositeDisposable();

        public WeatherController(IWeatherRefreshRequest refreshRequest, LoadWeatherUseCase loadWeatherUseCase)
        {
            _refreshRequest = refreshRequest;
            _loadWeatherUseCase = loadWeatherUseCase;
        }

        public void Initialize()
        {
            _refreshRequest.RefreshRequest
                .Subscribe(_ => _loadWeatherUseCase.Load())
                .AddTo(_disposables);
        }

        public void Dispose()
        {
            _disposables.Dispose();
        }
    }
}
