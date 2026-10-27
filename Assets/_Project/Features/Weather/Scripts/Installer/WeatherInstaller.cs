using _Project.Features.Weather.Scripts.Infrastructure;
using _Project.Features.Weather.Scripts.InterfaceInstaller;
using _Project.Features.Weather.Scripts.InterfaceInstaller.Ports;
using _Project.Features.Weather.Scripts.UseCases;
using UnityEngine;
using Zenject;

namespace _Project.Features.Weather.Scripts.Installer
{
    public class WeatherInstaller: MonoInstaller
    {
        [SerializeField] private WeatherView _weatherView;
        
        public override  void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<WeatherTabState>().AsSingle();
            
            Container.BindInterfacesTo<WeatherController>().AsSingle();
            Container.BindInterfacesTo<WeatherPresenter>().AsSingle();
            
            Container.Bind<IWeatherView>().FromInstance(_weatherView).AsSingle();
        }
    }
}