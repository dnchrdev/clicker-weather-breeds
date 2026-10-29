using _Project.Features.Weather.Scripts.InterfaceInstaller.Ports;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Features.Weather.Scripts.Infrastructure
{
    public class WeatherView: MonoBehaviour, IWeatherView
    {
        [SerializeField] private Image _weatherIcon;
        [SerializeField] private TMP_Text _temperatureText;

        public void UpdateWeather()
        {
            
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