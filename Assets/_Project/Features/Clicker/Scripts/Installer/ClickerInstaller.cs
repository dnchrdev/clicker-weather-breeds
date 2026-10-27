using _Project.Core.Scripts.Entities;
using _Project.Features.Clicker.Scripts.Entities;
using _Project.Features.Clicker.Scripts.Infrastructure;
using _Project.Features.Clicker.Scripts.InterfaceAdapters;
using _Project.Features.Clicker.Scripts.InterfaceAdapters.Ports;
using UnityEngine;
using Zenject;

namespace _Project.Features.Clicker.Scripts.Installer
{
    public class ClickerInstaller: MonoInstaller
    {
        [SerializeField] private ClickerView _clickerView;
        
        override public void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ClickerTab>().AsSingle();
            
            Container.BindInterfacesTo<ClickerController>().AsSingle();
            Container.BindInterfacesTo<ClickerPresenter>().AsSingle();
            
            Container.Bind<IClickerView>().To<ClickerView>().FromInstance(_clickerView).AsSingle();
            
        }
    }
}