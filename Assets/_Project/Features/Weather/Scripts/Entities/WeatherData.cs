namespace _Project.Features.Weather.Scripts.Entities
{
    public class WeatherData
    {
        public int Temperature { get; }
        public string TemperatureUnit { get; }
        public string IconUrl { get; }

        public WeatherData(int temperature, string temperatureUnit, string iconUrl)
        {
            Temperature = temperature;
            TemperatureUnit = temperatureUnit;
            IconUrl = iconUrl;
        }
    }
}
