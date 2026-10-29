using System;

namespace _Project.Features.Weather.Scripts.Entities
{
    [Serializable]
    public class WeatherSettings
    {
        public string Url;
        public string UserAgent;
        public float RefreshIntervalSeconds;
    }
}
