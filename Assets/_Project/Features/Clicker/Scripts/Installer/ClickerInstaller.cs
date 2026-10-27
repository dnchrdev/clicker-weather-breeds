using _Project.Features.Clicker.Scripts.Infrastructure;
using _Project.Features.Clicker.Scripts.InterfaceAdapters;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Clicker.Scripts.UseCases;
using UnityEngine;
using Zenject;

namespace _Project.Features.Clicker.Scripts.Installer
{
    public class ClickerInstaller: MonoInstaller
    {
        [SerializeField] private ClickerView _clickerView;
        
        override public void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ClickerTabState>().AsSingle();
            
            Container.BindInterfacesTo<ClickerController>().AsSingle();
            Container.BindInterfacesTo<ClickerPresenter>().AsSingle();
            
            Container.Bind<IClickerView>().FromInstance(_clickerView).AsSingle();
            
        }
    }
}