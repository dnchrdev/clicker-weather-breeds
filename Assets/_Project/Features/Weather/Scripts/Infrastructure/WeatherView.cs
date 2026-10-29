using _Project.Features.Weather.Scripts.InterfaceAdapters.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace _Project.Features.Weather.Scripts.Infrastructure
{
    public class WeatherView: MonoBehaviour, IWeatherView
    {
        [SerializeField] private Image _weatherIcon;
        [SerializeField] private TMP_Text _temperatureText;

        private WeatherIconStorage _iconStorage;

        [Inject]
        public void Construct(WeatherIconStorage iconStorage)
        {
            _iconStorage = iconStorage;
        }

        public void UpdateWeather(int temperature, string temperatureUnit, string iconUrl)
        {
            if (!_iconStorage.TryGet(iconUrl, out var icon))
                return;

            _weatherIcon.sprite = icon;
            _temperatureText.text = $"{temperature}{temperatureUnit}";
            
            Debug.Log($"[Weather] UPDATED: {temperature}{temperatureUnit}, ICON: {iconUrl}");
        }
        
        public void ShowView()
        {
            gameObject.SetActive(true);
        }

        public void HideView()
        {
            gameObject.SetActive(false);
        }
    }
}
