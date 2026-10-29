using _Project.Features.Weather.Scripts.Entities;
using _Project.Features.Weather.Scripts.Infrastructure;
using _Project.Features.Weather.Scripts.InterfaceAdapters;
using _Project.Features.Weather.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Weather.Scripts.UseCases;
using _Project.Network.Scripts.Entities;
using UnityEngine;
using Zenject;

namespace _Project.Features.Weather.Scripts.Installer
{
    public class WeatherInstaller: MonoInstaller
    {
        [SerializeField] private WeatherView _weatherView;
        [SerializeField] private WeatherSettings _weatherSettings = new WeatherSettings();
        
        public override  void InstallBindings()
        {
            Container.Bind<WeatherSettings>().FromInstance(_weatherSettings).AsSingle();
            
            Container.BindInterfacesTo<WeatherRefreshTimer>().AsSingle();
            Container.Bind<WeatherForecast>().AsSingle();
            
            Container.Bind<LoadWeatherUseCase>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeatherTabState>().AsSingle();
            
            Container.BindInterfacesTo<WeatherController>().AsSingle();
            Container.BindInterfacesTo<WeatherPresenter>().AsSingle();
            
            Container.Bind<IRequest<WeatherData>>().To<WeatherRequest>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeatherIconStorage>().AsSingle();
            
            Container.Bind<IWeatherView>().FromInstance(_weatherView).AsSingle();
        }
    }
}
