using _Project.Features.Weather.Scripts.InterfaceInstaller.Ports;
using UnityEngine;

namespace _Project.Features.Weather.Scripts.Infrastructure
{
    public class WeatherView:MonoBehaviour, IWeatherView
    {
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