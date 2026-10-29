using System;
using UniRx;

namespace _Project.Features.Weather.Scripts.Entities
{
    public interface IWeatherRefreshTimer
    {
        public void Start();
        public void Advance(float dt);
        public void Reset();
        public void RefreshStarted();
        public void RefreshFinished();
    }

    public interface IWeatherRefreshRequest
    {
        public IObservable<Unit> RefreshRequest { get; }
    }

    public class WeatherRefreshTimer : IWeatherRefreshTimer, IWeatherRefreshRequest
    {
        private readonly WeatherSettings _config;
        private readonly Subject<Unit> _refreshRequest = new Subject<Unit>();

        private float _timer;
        private bool _refreshInProgress;
        private bool _refreshMissed;

        public IObservable<Unit> RefreshRequest => _refreshRequest;

        public WeatherRefreshTimer(WeatherSettings config)
        {
            _config = config;
        }
        
        public void Start()
        {
            Reset();
            _refreshRequest.OnNext(Unit.Default);
        }

        public void Advance(float dt)
        {
            _timer += dt;

            if (_timer < _config.RefreshIntervalSeconds)
                return;

            _timer = 0f;
            
            if (_refreshInProgress)
                _refreshMissed = true;
            else
                _refreshRequest.OnNext(Unit.Default);
        }

        public void RefreshStarted()
        {
            _refreshInProgress = true;
        }

        public void RefreshFinished()
        {
            _refreshInProgress = false;

            if (!_refreshMissed)
                return;

            _refreshMissed = false;
            _timer = 0f;
            _refreshRequest.OnNext(Unit.Default);
        }

        public void Reset()
        {
            _timer = 0f;
            _refreshInProgress = false;
            _refreshMissed = false;
        }
    }
}
