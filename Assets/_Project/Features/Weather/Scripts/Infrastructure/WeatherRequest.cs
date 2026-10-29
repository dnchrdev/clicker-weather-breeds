using System;
using System.Collections.Generic;
using System.Text;
using _Project.Features.Weather.Scripts.Entities;
using _Project.Network.Scripts.Entities;
using UnityEngine;

namespace _Project.Features.Weather.Scripts.Infrastructure
{
    public class WeatherRequest : IRequest<WeatherData>
    {
        private readonly WeatherSettings _config;
        private readonly Dictionary<string, string> _headers;

        public string Url => _config.Url;
        public IReadOnlyDictionary<string, string> Headers => _headers;

        public WeatherRequest(WeatherSettings config)
        {
            _config = config;

            _headers = new Dictionary<string, string>
            {
                { "User-Agent", config.UserAgent },
                { "Accept", "application/geo+json" }
            };
        }

        public WeatherData Parse(byte[] raw)
        {
            var response = JsonUtility.FromJson<ForecastResponse>(Encoding.UTF8.GetString(raw));
            var periods = response?.properties?.periods;

            if (periods == null || periods.Length == 0)
                throw new FormatException("No periods found");

            var today = periods[0];

            return new WeatherData(today.temperature, today.temperatureUnit, today.icon);
        }

        [Serializable]
        private class ForecastResponse
        {
            public ForecastProperties properties;
        }

        [Serializable]
        private class ForecastProperties
        {
            public ForecastPeriod[] periods;
        }

        [Serializable]
        private class ForecastPeriod
        {
            public int temperature;
            public string temperatureUnit;
            public string icon;
        }
    }
}
