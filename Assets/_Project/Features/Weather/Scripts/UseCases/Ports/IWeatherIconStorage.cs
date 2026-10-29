namespace _Project.Features.Weather.Scripts.UseCases.Ports
{
    public interface IWeatherIconStorage
    {
        public bool Has(string url);
        public void Store(string url, byte[] data);
    }
}
