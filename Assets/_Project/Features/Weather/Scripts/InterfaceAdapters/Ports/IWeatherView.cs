namespace _Project.Features.Weather.Scripts.InterfaceAdapters.Ports
{
    public interface IWeatherView
    {
        public void UpdateWeather(int temperature, string temperatureUnit, string iconUrl);
        public void ShowView();
        public void HideView();
    }
}
