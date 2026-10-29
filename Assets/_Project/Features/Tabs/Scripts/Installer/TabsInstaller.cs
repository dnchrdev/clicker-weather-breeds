using _Project.Features.Tabs.Scripts.Infrastructure;
using _Project.Features.Tabs.Scripts.InterfaceAdapters;
using _Project.Features.Tabs.Scripts.InterfaceAdapters.Ports;
using _Project.Features.Tabs.Scripts.UseCases;
using UnityEngine;
using Zenject;

namespace _Project.Features.Tabs.Scripts.Installer
{
    public class TabsInstaller : MonoInstaller
    {
        [SerializeField] private TabsView _tabsView;
        
        public override void InstallBindings()
        {
            Container.Bind<OnTabClicked>().AsSingle();
            Container.Bind<TabsStateMachine>().AsSingle();
            
            Container.BindInterfacesTo<TabsController>().AsSingle();
            Container.BindInterfacesTo<TabsPresenter>().AsSingle();
            
            Container.Bind<ITabsView>().To<TabsView>().FromInstance(_tabsView).AsSingle();
        }
    }
}