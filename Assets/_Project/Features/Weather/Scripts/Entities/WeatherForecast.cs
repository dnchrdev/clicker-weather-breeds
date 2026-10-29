using System;
using UniRx;

namespace _Project.Features.Weather.Scripts.Entities
{
    public class WeatherForecast
    {
        private readonly Subject<WeatherData> _updated = new Subject<WeatherData>();

        public WeatherData Current { get; private set; }

        public IObservable<WeatherData> Updated => _updated;

        public bool TryUpdate(WeatherData data)
        {
            if (!IsValid(data))
                return false;

            Current = data;
            _updated.OnNext(data);

            return true;
        }

        private bool IsValid(WeatherData data)
        {
            return data != null
                   && !string.IsNullOrEmpty(data.TemperatureUnit)
                   && !string.IsNullOrEmpty(data.IconUrl);
        }
    }
}
