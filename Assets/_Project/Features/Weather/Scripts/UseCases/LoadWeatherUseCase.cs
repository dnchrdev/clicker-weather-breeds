using System;
using System.Threading;
using _Project.Features.Weather.Scripts.Entities;
using _Project.Features.Weather.Scripts.UseCases.Ports;
using _Project.Network.Scripts.Entities;
using Cysharp.Threading.Tasks;

namespace _Project.Features.Weather.Scripts.UseCases
{
    public class LoadWeatherUseCase
    {
        private readonly IRequestQueue _requestQueue;
        private readonly IRequest<WeatherData> _forecastRequest;
        private readonly IWeatherIconStorage _iconStorage;
        private readonly WeatherForecast _weatherForecast;
        private readonly IWeatherRefreshTimer _refreshTimer;
        
        private CancellationTokenSource _cts;

        public LoadWeatherUseCase(
            IRequestQueue requestQueue,
            IRequest<WeatherData> forecastRequest,
            IWeatherIconStorage iconStorage,
            WeatherForecast weatherForecast,
            IWeatherRefreshTimer refreshTimer)
        {
            _requestQueue = requestQueue;
            _forecastRequest = forecastRequest;
            _iconStorage = iconStorage;
            _weatherForecast = weatherForecast;
            _refreshTimer = refreshTimer;
        }
        
        public void Load()
        {
            CancelCurrent();
            _refreshTimer.RefreshStarted();

            _cts = new CancellationTokenSource();
            Refresh(_cts.Token).Forget();
        }

        public void Cancel()
        {
            CancelCurrent();
        }

        private async UniTaskVoid Refresh(CancellationToken token)
        {
            try
            {
                var bytes = await _requestQueue.Get(_forecastRequest, token);
                await LoadIcon(bytes, token);
                _weatherForecast.TryUpdate(bytes);
            }
            catch (OperationCanceledException)
            {
                return;
            }
            catch (Exception)
            {
                
            }

            _refreshTimer.RefreshFinished();
        }

        private async UniTask LoadIcon(WeatherData data, CancellationToken token)
        {
            if (string.IsNullOrEmpty(data.IconUrl) || _iconStorage.Has(data.IconUrl))
                return;

            var bytes = await _requestQueue.Get(new BytesRequest(data.IconUrl), token);

            _iconStorage.Store(data.IconUrl, bytes);
        }

        private void CancelCurrent()
        {
            if (_cts == null)
                return;

            _cts.Cancel();
            _cts.Dispose();
            _cts = null;
        }
    }
}
